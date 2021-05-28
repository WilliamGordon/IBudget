using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibudget.mvc.Models.EditModel
{
    public class TransactionEditModel
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
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>();
    }
}
