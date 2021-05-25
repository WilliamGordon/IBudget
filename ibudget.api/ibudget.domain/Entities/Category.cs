using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int TransactionId { get; set; }
        public string Name { get; set; }
    }
}
