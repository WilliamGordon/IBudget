using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.console
{
    public class TransactionRaw
    {
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string account_receiver { get; set; }
        public string tracking_number { get; set; }
        public string accounting_date { get; set; }
        public string value_date { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return $"account_number:{account_number}, account_name:{account_name}, account_receiver:{account_receiver}, expense_tracking_number:{tracking_number}, accounting_date:{accounting_date}, value_date:{value_date}, amount:{amount}, currency:{currency}, expense_type:{type}, description:{description}, message:{message}";
        }
    }
}
