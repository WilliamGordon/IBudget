using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Transactions.Queries
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Transmitter { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
