using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopBusinessLogic.Models;
using ShopOnlineMVC.App_Code;
using ShopOnlineMVC.Areas.Admin.Models;
using ShopRepository;

namespace ShopOnlineMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        IUserRepository userRepository = null;
        public LoginController()
        {
            userRepository = new UserRepository();
        }

        // GET: Admin/Login
        public async Task<IActionResult> Index(string ReturnUrl = null)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var username = model.UserName;
                var password = Common.EncryptMD5(model.Password);
                User user = userRepository.GetUserByUserNamePass(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, username),
                         new Claim("FullName", user.UserName),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, "Admin");
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    await HttpContext.SignInAsync("Admin", principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                    SetAlert("Đăng nhập thành công!", "success");
                    var routeValues = new RouteValueDictionary
                {
                    {"area","Admin" },
                    {"returnURL",Request.Query["ReturnUrl"] },
                    {"claimValue","true" }
                };
                    if (TempData["ReturnUrl"] != null)
                    {
                        return Redirect(TempData["ReturnUrl"].ToString());
                    }

                    return RedirectToAction("Index", "Home", routeValues);

                }
                else
                {
                    SetAlert("Tên đăng nhập hoặc mật khẩu không đúng!", "error");
                }
            }
            else
            {
                ModelState.AddModelError("Error",
                                 "Please input field full!");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Đăng xuất người dùng
            await HttpContext.SignOutAsync("Admin");
            SetAlert("Đăng xuất thành công!", "warning");
            // Chuyển hướng đến trang đăng nhập hoặc trang chính
            return RedirectToAction("Index", "Login", new { area = "Admin" }); // Thay thế bằng tên trang đăng nhập hoặc trang chính của bạn
        }

    }
}
