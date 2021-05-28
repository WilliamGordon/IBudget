using CsvHelper;
using CsvHelper.Configuration;
using ibudget.domain.Entities;
using ibudget.infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ibudget.console
{
    public class TransactionProcessorCSV
    {
        private StreamReader StreamReader;
        private CsvReader CsvReader;
        private ApplicationDbContext Context;
        private List<Transaction> Transactions;
        private List<TransactionRaw> TransactionRaw;
        private bool isRecordBad;

        // Should be able to handle 
        // - path to csv file on disk
        // - actual IFormFile csv from api route

        public TransactionProcessorCSV(ApplicationDbContext context, string path)
        {
            StreamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1"));
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ";",
                BadDataFound = context =>
                {
                    isRecordBad = true;
                }
            };
            CsvReader = new CsvReader(this.StreamReader, config);
            Context = context;
            TransactionRaw = new List<TransactionRaw>();
            Transactions = new List<Transaction>();
        }

        private void ProcessTransactionCSV()
        {
            while (CsvReader.Read())
            {
                TransactionRaw transactionRaw = CsvReader.GetRecord<TransactionRaw>();
                if (!isRecordBad)
                    TransactionRaw.Add(transactionRaw);
                isRecordBad = false;
            }
        }

        private void ProcessTransactionRaw()
        {
            foreach (var transaction in TransactionRaw)
            {
                Transaction newTransaction = new Transaction();
                // Try converting the TransactionForAdd to an Transaction 
                // if anything goes wrong log the error and the TransactionForAdd as a string in DB

                try
                {
                    newTransaction.Date = Convert.ToDateTime(transaction.accounting_date);
                    newTransaction.Amount = Convert.ToDecimal(transaction.amount);
                    newTransaction.Receiver = transaction.account_receiver;
                    newTransaction.Transmitter = transaction.account_number;
                    newTransaction.Description = concatenateInformation(transaction.type, transaction.description);
                    newTransaction.Message = transaction.message;
                    newTransaction.BudgetId = 1;

                    // TODO When using the self-refencing table either assign a each transaction
                    // either to an Expense or an Income depending on the amount

                    this.Transactions.Add(newTransaction);
                }
                // If an error occurs log it in the database
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private bool DoesTransactionAlreadyExist(Transaction Transaction)
        {
            // NOTE: need to handle the case where the same expense is done two time in a row,
            // should look at the traking number but it seems that is sometime inconsitant

            if (Context.Transactions.Any(o =>
                    o.Amount == Transaction.Amount &&
                    o.Date == Transaction.Date &&
                    o.Description == Transaction.Description))
                return true;

            return false;
        }

        private string concatenateInformation(string TransactionType, string description)
        {
            string information = $"Expsense Type: {TransactionType}, Description: {description}";

            // Regex to delete uneccessary whitespace
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            information = regex.Replace(information, " ");

            return information;
        }

        private void AddProcessedTransactionInDB()
        {
            foreach (Transaction Transaction in Transactions)
            {
                if (!DoesTransactionAlreadyExist(Transaction))
                {
                    Context.Transactions.Add(Transaction);
                    //Console.WriteLine(Transaction.Amount);
                }
                else
                {
                    Console.WriteLine("Transaction already exist");
                }
            }
            Context.SaveChanges();
        }

        public void ProcessTransactions()
        {
            ProcessTransactionCSV();
            ProcessTransactionRaw();

            AddProcessedTransactionInDB();
        }
    }
}
