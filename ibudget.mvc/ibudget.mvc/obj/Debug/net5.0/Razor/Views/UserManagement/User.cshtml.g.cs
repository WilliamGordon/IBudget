#pragma checksum "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a7ba4e93537069c1203d1f1cb9fdf6588232267a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserManagement_User), @"mvc.1.0.view", @"/Views/UserManagement/User.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\_ViewImports.cshtml"
using ibudget.mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\_ViewImports.cshtml"
using ibudget.mvc.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7ba4e93537069c1203d1f1cb9fdf6588232267a", @"/Views/UserManagement/User.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c40c84dae6b9ffd4aae1adb565df9d6c831ca76", @"/Views/_ViewImports.cshtml")]
    public class Views_UserManagement_User : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ibudget.mvc.Models.ViewModel.User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
  
    ViewData["Title"] = "User";

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n<div>\r\n    <div class=\"bg-white px-0 \">\r\n        <div class=\"row\">\r\n            <div class=\" col-md-auto \" style=\"padding-top: 3px\">\r\n                <h4><img");
            BeginWriteAttribute("src", " src=\"", 250, "\"", 270, 1);
#nullable restore
#line 11 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
WriteAttributeValue("", 256, Model.Picture, 256, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"rounded-circle img-fluid\" width=\"35\" height=\"35\" alt=\"profile-image\"></h4>\r\n            </div>\r\n            <div class=\" col-md-auto \" style=\"padding-top: 3px\">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 459, "\"", 500, 2);
            WriteAttributeValue("", 466, "/UserManagement/Edit/", 466, 21, true);
#nullable restore
#line 14 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
WriteAttributeValue("", 487, Model.UserId, 487, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-outlined btn-black text-muted bg-transparent"" data-wow-delay=""0.7s"">
                    <small>Edit User</small>
                </a>
                <i class=""mdi mdi-settings-outline""></i>
                <span class=""vl ml-3""></span>
            </div>
        </div>
    </div>
    <hr />
    <dl class=""row"">
        <dt class=""col-sm-2"">
            ");
#nullable restore
#line 25 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.UserId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 28 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.UserId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 31 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 34 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 37 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.NickName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 40 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.NickName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 43 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 46 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 49 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.Role));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 52 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.Role));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 55 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.UpdatedAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 58 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.UpdatedAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 61 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.CreatedAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 64 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.CreatedAt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 67 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.LastLogin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 70 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.LastLogin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 73 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayNameFor(model => model.LoginsCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 76 "C:\Users\willi\Desktop\WEB\Project\2_developement\IBudget\ibudget.mvc\ibudget.mvc\Views\UserManagement\User.cshtml"
       Write(Html.DisplayFor(model => model.LoginsCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ibudget.mvc.Models.ViewModel.User> Html { get; private set; }
    }
}
#pragma warning restore 1591