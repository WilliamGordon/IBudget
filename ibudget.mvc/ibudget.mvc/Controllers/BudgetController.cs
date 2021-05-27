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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using ibudget.mvc.Models.EditModel;

namespace ibudget.mvc.Controllers
{
    public class BudgetController : Controller
    {
        public IConfiguration Configuration { get; }

        public BudgetController(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public async Task<IActionResult> DashboardBudgets(BudgetCreateModel? budget)
        {
            try
            {
                ViewBag.ErrorNotification = TempData["ErrorNotification"];
                ViewBag.ErrorNotificationModal = TempData["ErrorNotificationModal"];
                // Getting Auth0 parameters
                var UserId = User.Claims.Where(m => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault().Value;
                var AccessToken = await HttpContext.GetTokenAsync("access_token");
                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Budget/GetAllBudgetsForUser/{UserId}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                // Executing API Call with RestSharp
                var response = client.Execute<List<BudgetViewModel>>(request);

                // Handling creating of new Budget
                budget = new BudgetCreateModel
                {
                    UserId = UserId,
                    Name = budget == null ? "" : budget.Name    // make the value sticky in case of error
                };

                if (response.IsSuccessful)
                {
                    ViewData.Model = new DashboardBudgetsComplexeModel
                    {
                        Budgets = response.Data,
                        Budget = budget
                    };
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewBag.ErrorNotification = response.Content;
                        ViewData.Model = new DashboardBudgetsComplexeModel
                        {
                            Budgets = null,
                            Budget = budget
                        };
                    }
                    
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorNotification = ex;
                return View();
            }
        }

        // POST: BudgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBudget(BudgetCreateModel budget)
        {
            try
            {
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");
                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Budget");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                request.AddJsonBody(budget);
                // Executing API Call with RestSharp
                var response = client.Post(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudgets));
                }
                else
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        TempData["ErrorNotificationModal"] = response.Content;
                        return RedirectToAction(nameof(DashboardBudgets), budget);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudgets), budget);
            }
        }

        public async Task<IActionResult> DashboardBudget(int id)
        {
            try
            {
                ViewBag.ErrorNotification = TempData["ErrorNotification"];
                ViewBag.ErrorNotificationTransactionModal = TempData["ErrorNotificationTransactionModal"];
                ViewBag.ErrorNotificationNewCategoryModal = TempData["ErrorNotificationNewCategoryModal"];
                ViewBag.ErrorNotificationAddCategoryModal = TempData["ErrorNotificationAddCategoryModal"];
                // Getting Auth0 parameters
                var UserId = User.Claims.Where(m => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault().Value;
                var AccessToken = await HttpContext.GetTokenAsync("access_token");
                // Configuration API Call with RestSharp
                var clientBudget = new RestClient($"{Configuration["Auth0:Audience"]}/api/Budget/GetBudget/" + id);
                var requestBudget = new RestRequest(Method.GET);
                requestBudget.AddHeader("authorization", $"Bearer {AccessToken}");

                // Configuration API Call with RestSharp
                var clientTransaction = new RestClient($"{Configuration["Auth0:Audience"]}/api/Transaction/GetAllTransactionsForBudget/" + id);
                var requestTransaction = new RestRequest(Method.GET);
                requestTransaction.AddHeader("authorization", $"Bearer {AccessToken}");

                // Configuration API Call with RestSharp
                var clientCategory = new RestClient($"{Configuration["Auth0:Audience"]}/api/Category/GetAllCategoriesForBudget/" + id);
                var requestCategory = new RestRequest(Method.GET);
                requestCategory.AddHeader("authorization", $"Bearer {AccessToken}");

                // Handling creating of new transaction
                TransactionCreateModel transaction = new TransactionCreateModel
                {
                    BudgetId = id
                };
                if (TempData["transaction"] != null)
                {
                    transaction = JsonConvert.DeserializeObject< TransactionCreateModel>(TempData["transaction"].ToString());
                }

                // Handling editing transaction
                TransactionCategoryEditModel transactionForEdit = new TransactionCategoryEditModel
                {
                    BudgetId = id
                };
                if (TempData["transactionForEdit"] != null)
                {
                    transactionForEdit = JsonConvert.DeserializeObject<TransactionCategoryEditModel>(TempData["transactionForEdit"].ToString());
                }

                // Handling creating of new category
                CategoryCreateModel category = new CategoryCreateModel
                {
                    BudgetId = id
                };
                if (TempData["category"] != null)
                {
                    category = JsonConvert.DeserializeObject<CategoryCreateModel>(TempData["category"].ToString());
                }

                // Executing API Call with RestSharp
                var responseBudget = clientBudget.Execute<BudgetViewModel>(requestBudget);
                var responseTransaction = clientTransaction.Execute<List<TransactionViewModel>>(requestTransaction);
                var responseCategory = clientCategory.Execute<List<CategoryViewModel>>(requestCategory);

                if (responseBudget.IsSuccessful && responseTransaction.IsSuccessful && responseCategory.IsSuccessful)
                {
                    foreach (var c in responseCategory.Data)
                    {
                        transaction.Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                        transactionForEdit.Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    }

                    ViewData.Model = new DashboardBudgetComplexeModel
                    {
                        Budget = responseBudget.Data,
                        Transactions = responseTransaction.Data,
                        Categories = responseCategory.Data,
                        TransactionForEdit = transactionForEdit,
                        Transaction = transaction,
                        Category = category,

                    };
                }
                else
                {
                    ViewBag.ErrorNotification = responseBudget.Content + responseTransaction.Content;
                    ViewData.Model = new DashboardBudgetComplexeModel
                    {
                        Budget = null,
                        Transactions = null,
                        Categories = null,
                        TransactionForEdit = transactionForEdit,
                        Transaction = transaction,
                        Category = category
                    };
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorNotification = ex;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTransaction(TransactionCreateModel transaction)
        {
            try
            {
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");
                
                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Transaction");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                request.AddJsonBody(transaction);

                // Executing API Call with RestSharp
                var response = client.Post(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transaction.BudgetId
                    });
                }
                else
                {
                    TempData["ErrorNotificationTransactionModal"] = response.Content;
                    TempData["transaction"] = JsonConvert.SerializeObject(transaction);
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transaction.BudgetId,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = transaction.BudgetId
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryCreateModel category)
        {
            try
            {
                if (category.ParentId == null)
                {
                    category.ParentId = 0;
                }
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");

                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Category");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                request.AddJsonBody(JsonConvert.SerializeObject(category));

                // Executing API Call with RestSharp
                var response = client.Post(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = category.BudgetId
                    });
                }
                else
                {
                    TempData["ErrorNotificationNewCategoryModal"] = response.Content;
                    TempData["category"] = JsonConvert.SerializeObject(category);
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = category.BudgetId,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = category.BudgetId
                });
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTransactionCategory(TransactionCategoryEditModel transactionForEdit)
        {
            try
            {
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");

                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Transaction/UpdateTransactionCategory/{transactionForEdit.Id}");
                var request = new RestRequest(Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                request.AddJsonBody(JsonConvert.SerializeObject(transactionForEdit));

                // Executing API Call with RestSharp
                var response = client.Put(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transactionForEdit.BudgetId
                    });
                }
                else
                {
                    TempData["ErrorNotificationAddCategoryModal"] = response.Content;
                    TempData["transactionForEdit"] = JsonConvert.SerializeObject(transactionForEdit);
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transactionForEdit.BudgetId,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = transactionForEdit.BudgetId
                });
            }
        }
    }
}
