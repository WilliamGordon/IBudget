using ibudget.mvc.Models.CreateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.ComplexeModel
{
    public class TransactionCreateComplexeModel
    {
        public TransactionCreateModel Transaction { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
