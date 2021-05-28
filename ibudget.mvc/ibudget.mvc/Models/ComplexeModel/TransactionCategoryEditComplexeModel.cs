using ibudget.mvc.Models.EditModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.ComplexeModel
{
    public class TransactionCategoryEditComplexeModel
    {
        public TransactionCategoryEditModel TransactionCategoryForEdit { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
