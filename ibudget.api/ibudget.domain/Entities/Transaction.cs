using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Budget Budget { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Transmitter { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
