using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopBusinessLogic.Models;
using ShopOnlineMVC.App_Code;
using ShopRepository;
using X.PagedList;

namespace ShopOnlineMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UsersController : BaseController
    {
        IUserRepository userRepository;
        IRoleRepository roleRepository;

        public UsersController()
        {
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(string searchString, int? page, string sortby)
        {
            TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
            var users = await userRepository.GetAllUser();
            users= users.Where(c => Common.ConvertToUnSign(c.FullName).Contains(searchString != null ? Common.ConvertToUnSign(TempData["searchString"].ToString()) : "", StringComparison.OrdinalIgnoreCase));
            switch (sortby)
            {
                case "name":
                    users = users.OrderBy(o => o.FullName);
                    break;
                case "namedesc":
                    users = users.OrderByDescending(o => o.FullName);
                    break;
                default:
                    break;
            }
            ViewBag.Page = 10;
            var pagedUsers = await users.ToPagedListAsync(page ?? 1, (int)ViewBag.Page);
            return View(pagedUsers);
        }


        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(roleRepository.GetAllRole(), "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus(int id)
        {
            var result = await userRepository.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Email,Password,FullName,RoleId,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Password))
                {
                    SetAlert(Constant.PASSWORD_FAIL, "error");
                    ViewData["RoleId"] = new SelectList(roleRepository.GetAllRole(), "RoleId", "RoleName", user.RoleId);
                    return View(user);
                }
                user.Password = Common.EncryptMD5(user.Password);
                await userRepository.Add(user);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(roleRepository.GetAllRole(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await userRepository.GetUserById(Convert.ToInt32(id));
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(roleRepository.GetAllRole(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Email,Password,FullName,RoleId,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                await userRepository.Update(user);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(roleRepository.GetAllRole(), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                var user = await userRepository.GetUserById(Convert.ToInt32(id));
                if (user == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                await userRepository.Delete(id);
                SetAlert(Constant.DELETE_SUCCESS, "success");
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public JsonResult ListName(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var data = userRepository.GetUserByName(q.ToLower());
                var responseData = data.Select(c => c.FullName).ToList();
                return Json(new
                {
                    data = responseData,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}
