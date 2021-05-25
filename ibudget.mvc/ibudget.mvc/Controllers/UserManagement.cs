using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserView = ibudget.mvc.Models.ViewModel.User;
using UserEdit = ibudget.mvc.Models.EditModel.User;
using Auth0.ManagementApi.Paging;
using ibudget.mvc.Models.EditModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ibudget.mvc.Controllers
{
    public class UserManagement : Controller
    {
        public IConfiguration Configuration { get; }

        public UserManagement(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var apiClient = new ManagementApiClient(Configuration["Auth0:TokenMgmt"], new Uri(Configuration["Auth0:ApiMgmt"]));
                // Get list of roles
                var roles = await apiClient.Roles.GetAllAsync(new Auth0.ManagementApi.Models.GetRolesRequest());
                List<UserView> users = new List<UserView>();
                // Get users for each roles
                foreach (var role in roles)
                {
                    foreach (var user in (PagedList<Auth0.ManagementApi.Models.AssignedUser>)await apiClient.Roles.GetUsersAsync(role.Id))
                    {
                        users.Add(new UserView
                        {
                            UserId = user.UserId,
                            Email = user.Email,
                            FullName = user.FullName,
                            Picture = user.Picture,
                            Role = role.Name,
                        });
                    }
                }
                ViewData.Model = users;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorNotification = ex.Message;
                return View();
            }
        }
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> User(string id)
        {
            var apiClient = new ManagementApiClient(Configuration["Auth0:TokenMgmt"], new Uri(Configuration["Auth0:ApiMgmt"]));
            var user = await apiClient.Users.GetAsync(id);
            var role = await apiClient.Users.GetRolesAsync(id, new Auth0.ManagementApi.Paging.PaginationInfo());
            ViewData.Model = new UserView
            {
                UserId = user.UserId,
                Email = user.Email,
                NickName = user.NickName,
                FullName = user.FullName,
                Picture = user.Picture,
                Role = role[0].Name,
                UpdatedAt = (DateTime)user.UpdatedAt,
                CreatedAt = (DateTime)user.CreatedAt,
                LastLogin = (DateTime)user.LastLogin,
                LoginsCount = user.LoginsCount,
            };
            return View();
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(string id)
        {
            var apiClient = new ManagementApiClient(Configuration["Auth0:TokenMgmt"], new Uri(Configuration["Auth0:ApiMgmt"]));
            var user = await apiClient.Users.GetAsync(id);
            var role = await apiClient.Users.GetRolesAsync(id, new Auth0.ManagementApi.Paging.PaginationInfo());
            var roles = new List<SelectListItem>();

            foreach (var r in (PagedList<Auth0.ManagementApi.Models.Role>)await apiClient.Roles.GetAllAsync(new Auth0.ManagementApi.Models.GetRolesRequest()))
            {
                roles.Add(new SelectListItem
                {
                    Value = r.Id,
                    Text = r.Name,
                    Selected = r.Id == role[0].Id
                });
            }

            ViewData.Model = new UserEdit
            {
                NickName = user.NickName,
                FullName = user.FullName,
                Role = role[0].Name,
                Roles = roles
            };
            return View();
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> Edit(UserEdit user, string id)
        {
            var apiClient = new ManagementApiClient(Configuration["Auth0:TokenMgmt"], new Uri(Configuration["Auth0:ApiMgmt"]));
            var u = await apiClient.Users.GetAsync(id);

            var response = await apiClient.Users.UpdateAsync(id, new Auth0.ManagementApi.Models.UserUpdateRequest { 
                FullName = user.FullName,
                NickName = user.NickName,
            });

            return RedirectToAction("User", "UserManagement", new { id = response.UserId });
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var apiClient = new ManagementApiClient(Configuration["Auth0:TokenMgmt"], new Uri(Configuration["Auth0:ApiMgmt"]));
            await apiClient.Users.DeleteAsync(id);
            return RedirectToAction("Index", "UserManagement");
        }
    }
}
