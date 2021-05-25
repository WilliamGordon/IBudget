using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Microsoft.AspNetCore.Authentication;
using ibudget.mvc.Models.ViewModel;
using ibudget.mvc.Models.CreateModel;

namespace ibudget.mvc.Controllers
{
    public class BudgetController : Controller
    {
        // GET: BudgetController
        public async Task<IActionResult> Index(BudgetCreateModel? budget)
        {
            ViewBag.ErrorNotification = TempData["ErrorNotification"];
            var client = new RestClient("https://localhost:44358/api/Budget/GetAllBudgetsForUser/auth0%7C608e980d95657a0069252b8d");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"Bearer {await HttpContext.GetTokenAsync("access_token")}");
            List<Budget> budgets = client.Execute<List<Budget>>(request).Data;
            if (budget != null)
            {
                ViewData.Model = new BudgetsViewCreate { Budgets = budgets, Budget = new BudgetCreateModel {
                        UserId = User.Claims.Where(m => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault().Value,
                        Name = budget.Name
                    }
                };
            }
            else
            {
                ViewData.Model = new BudgetsViewCreate { 
                    Budgets = budgets, Budget = new BudgetCreateModel { 
                        UserId = User.Claims.Where(m => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault().Value
                    } 
                };
            }
            return View();
        }

        // GET: BudgetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: BudgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BudgetCreateModel budget)
        {
            try
            {
                var client = new RestClient("https://localhost:44358/api/Budget");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {await HttpContext.GetTokenAsync("access_token")}");
                request.AddJsonBody(budget);
                var response = client.Post(request);
                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorNotification"] = response.Content;
                    return RedirectToAction(nameof(Index), budget);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BudgetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BudgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BudgetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BudgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
