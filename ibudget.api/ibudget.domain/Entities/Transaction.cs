using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
#nullable enable
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
#nullable disable
        public Budget Budget { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Transmitter { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
