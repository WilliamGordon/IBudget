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
using ibudget.mvc.Models.ComplexeModel;

namespace ibudget.mvc.Controllers
{
    public class BudgetController : Controller
    {
        public IConfiguration Configuration { get; }

        public BudgetController(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
#nullable enable
        public async Task<IActionResult> DashboardBudgets(BudgetCreateModel? budget)
#nullable disable
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
#nullable enable
        public async Task<IActionResult> DashboardBudget(int id, string? month, string? year)
#nullable disable
        {
            try
            {
                ViewBag.ErrorNotification = TempData["ErrorNotification"];
                ViewBag.ErrorNotificationTransactionForCreateModal = TempData["ErrorNotificationTransactionForCreateModal"];
                ViewBag.ErrorNotificationTransactionForEditModal = TempData["ErrorNotificationTransactionForEditModal"];
                ViewBag.ErrorNotificationTransactionCategoryForEditModal = TempData["ErrorNotificationTransactionCategoryForEditModal"];
                ViewBag.ErrorNotificationCategoryForCreateModal = TempData["ErrorNotificationCategoryForCreateModal"];
                // check if controller parameters are correctly set
                if (String.IsNullOrEmpty(month) || String.IsNullOrEmpty(year))
                {
                    month = DateTime.Now.Month.ToString();
                    year = DateTime.Now.Year.ToString();
                }

                // Getting Auth0 parameters
                var UserId = User.Claims.Where(m => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").FirstOrDefault().Value;
                var AccessToken = await HttpContext.GetTokenAsync("access_token");
                // Configuration API Call with RestSharp
                var clientBudget = new RestClient($"{Configuration["Auth0:Audience"]}/api/Budget/GetBudget/" + id);
                var requestBudget = new RestRequest(Method.GET);
                requestBudget.AddHeader("authorization", $"Bearer {AccessToken}");

                // Configuration API Call with RestSharp
                var clientTransaction = new RestClient($"{Configuration["Auth0:Audience"]}/api/Transaction/GetAllTransactionsForBudget/{id}/{month}/{year}");
                var requestTransaction = new RestRequest(Method.GET);
                requestTransaction.AddHeader("authorization", $"Bearer {AccessToken}");

                // Configuration API Call with RestSharp
                var clientCategory = new RestClient($"{Configuration["Auth0:Audience"]}/api/Category/GetAllCategoriesForBudget/" + id);
                var requestCategory = new RestRequest(Method.GET);
                requestCategory.AddHeader("authorization", $"Bearer {AccessToken}");

                // Handling creating of new transaction
                TransactionCreateComplexeModel transaction = new TransactionCreateComplexeModel
                {
                    Transaction = new TransactionCreateModel
                    {
                        BudgetId = id,
                        Date = DateTime.Now,
                    },
                    Month = month,
                    Year = year,
                };
                if (TempData["transaction"] != null)
                {
                    transaction = JsonConvert.DeserializeObject<TransactionCreateComplexeModel>(TempData["transaction"].ToString());
                }

                // Handling editing transaction category
                TransactionEditComplexeModel transactionForEdit = new TransactionEditComplexeModel
                {
                    TransactionForEdit = new TransactionEditModel
                    {
                        BudgetId = id
                    },
                    Month = month,
                    Year = year,
                };
                if (TempData["transactionForEdit"] != null)
                {
                    transactionForEdit = JsonConvert.DeserializeObject<TransactionEditComplexeModel>(TempData["transactionForEdit"].ToString());
                }

                // Handling editing transaction category
                TransactionCategoryEditComplexeModel transactionCategoryForEdit = new TransactionCategoryEditComplexeModel
                {
                    TransactionCategoryForEdit = new TransactionCategoryEditModel
                    {
                        BudgetId = id
                    },
                    Month = month,
                    Year = year,
                };
                if (TempData["transactionCategoryForEdit"] != null)
                {
                    transactionCategoryForEdit = JsonConvert.DeserializeObject<TransactionCategoryEditComplexeModel>(TempData["transactionCategoryForEdit"].ToString());
                }

                // Handling creating of new category
                CategoryCreateComplexeModel category = new CategoryCreateComplexeModel
                {
                    Category = new CategoryCreateModel
                    {
                        BudgetId = id
                    },
                    Month = month,
                    Year = year,
                };
                if (TempData["category"] != null)
                {
                    category = JsonConvert.DeserializeObject<CategoryCreateComplexeModel>(TempData["category"].ToString());
                }

                // Executing API Call with RestSharp
                var responseBudget = clientBudget.Execute<BudgetViewModel>(requestBudget);
                var responseTransaction = clientTransaction.Execute<List<TransactionViewModel>>(requestTransaction);
                var responseCategory = clientCategory.Execute<List<CategoryViewModel>>(requestCategory);

                if (responseBudget.IsSuccessful && responseTransaction.IsSuccessful && responseCategory.IsSuccessful)
                {
                    foreach (var c in responseCategory.Data)
                    {
                        transaction.Transaction.Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                        transactionForEdit.TransactionForEdit.Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                        transactionCategoryForEdit.TransactionCategoryForEdit.Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    }

                    ViewData.Model = new DashboardBudgetComplexeModel
                    {
                        Budget = responseBudget.Data,
                        Transactions = responseTransaction.Data,
                        Categories = responseCategory.Data,
                        TransactionCategoryForEdit = transactionCategoryForEdit,
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
                        TransactionCategoryForEdit = transactionCategoryForEdit,
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
        public async Task<IActionResult> CreateTransaction(TransactionCreateComplexeModel transaction)
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
                request.AddJsonBody(transaction.Transaction);

                // Executing API Call with RestSharp
                var response = client.Post(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transaction.Transaction.BudgetId,
                        month = transaction.Transaction.Date.Month,
                        year = transaction.Transaction.Date.Year,
                    });
                }
                else
                {
                    TempData["ErrorNotificationTransactionForCreateModal"] = response.Content;
                    TempData["transaction"] = JsonConvert.SerializeObject(transaction);
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transaction.Transaction.BudgetId,
                        month = transaction.Month,
                        year = transaction.Year,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = transaction.Transaction.BudgetId,
                    month = transaction.Month,
                    year = transaction.Year,
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryCreateComplexeModel category)
        {
            try
            {
                if (category.Category.ParentId == null)
                {
                    category.Category.ParentId = 0;
                }
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");

                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Category");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                request.AddJsonBody(JsonConvert.SerializeObject(category.Category));

                // Executing API Call with RestSharp
                var response = client.Post(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = category.Category.BudgetId,
                        month = category.Month,
                        year = category.Year,
                    });
                }
                else
                {
                    TempData["ErrorNotificationTransactionCategoryForEditModal"] = response.Content;
                    TempData["category"] = JsonConvert.SerializeObject(category);
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = category.Category.BudgetId,
                        month = category.Month,
                        year = category.Year,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = category.Category.BudgetId,
                    month = category.Month,
                    year = category.Year,
                });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTransaction(TransactionEditComplexeModel transactionForEdit)
        {
            try
            {
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");

                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Transaction/UpdateTransaction/{transactionForEdit.TransactionForEdit.Id}");
                var request = new RestRequest(Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                request.AddJsonBody(JsonConvert.SerializeObject(transactionForEdit.TransactionForEdit));

                // Executing API Call with RestSharp
                var response = client.Put(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transactionForEdit.TransactionForEdit.BudgetId,
                        month = transactionForEdit.Month,
                        year = transactionForEdit.Year,
                    });
                }
                else
                {
                    TempData["ErrorNotificationTransactionForEditModal"] = response.Content;
                    TempData["transactionForEdit"] = JsonConvert.SerializeObject(transactionForEdit);
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transactionForEdit.TransactionForEdit.BudgetId,
                        month = transactionForEdit.Month,
                        year = transactionForEdit.Year,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = transactionForEdit.TransactionForEdit.BudgetId,
                    month = transactionForEdit.Month,
                    year = transactionForEdit.Year,
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTransactionCategory(TransactionCategoryEditComplexeModel transactionCategoryForEdit)
        {
            try
            {
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");

                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Transaction/UpdateTransactionCategory/{transactionCategoryForEdit.TransactionCategoryForEdit.Id}");
                var request = new RestRequest(Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");
                request.AddJsonBody(JsonConvert.SerializeObject(transactionCategoryForEdit.TransactionCategoryForEdit));

                // Executing API Call with RestSharp
                var response = client.Put(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transactionCategoryForEdit.TransactionCategoryForEdit.BudgetId,
                        month = transactionCategoryForEdit.Month,
                        year = transactionCategoryForEdit.Year,
                    });
                }
                else
                {
                    TempData["ErrorNotificationCategoryForCreateModal"] = response.Content;
                    TempData["transactionCategoryForEdit"] = JsonConvert.SerializeObject(transactionCategoryForEdit);
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = transactionCategoryForEdit.TransactionCategoryForEdit.BudgetId,
                        month = transactionCategoryForEdit.Month,
                        year = transactionCategoryForEdit.Year,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = transactionCategoryForEdit.TransactionCategoryForEdit.BudgetId,
                    month = transactionCategoryForEdit.Month,
                    year = transactionCategoryForEdit.Year,
                });
            }
        }

        public async Task<IActionResult> DeleteTransaction(int id, int budgetid, string month, string year)
        {
            try
            {
                // Getting Auth0 parameters
                var AccessToken = await HttpContext.GetTokenAsync("access_token");

                // Configuration API Call with RestSharp
                var client = new RestClient($"{Configuration["Auth0:Audience"]}/api/Transaction/DeleteTransaction/{id}");
                var request = new RestRequest(Method.DELETE);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json;");
                request.AddHeader("authorization", $"Bearer {AccessToken}");

                // Executing API Call with RestSharp
                var response = client.Delete(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = budgetid,
                        month = month,
                        year = year,
                    });
                }
                else
                {
                    TempData["ErrorNotification"] = response.Content;
                    return RedirectToAction(nameof(DashboardBudget), new
                    {
                        Id = budgetid,
                        month = month,
                        year = year,
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorNotification"] = ex;
                return RedirectToAction(nameof(DashboardBudget), new
                {
                    Id = budgetid,
                    month = month,
                    year = year,
                });
            }
            
        }
    }
}
