using ibudget.infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace ibudget.console
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(options))
            {
                var processor = new TransactionProcessorCSV(context, @"C:\Users\willi\Downloads\transaction_ING.csv");
                processor.ProcessTransactions();
            }
        }
    }
}
