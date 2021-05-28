using ibudget.mvc.Models.CreateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.ComplexeModel
{
    public class CategoryCreateComplexeModel
    {
        public CategoryCreateModel Category { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
