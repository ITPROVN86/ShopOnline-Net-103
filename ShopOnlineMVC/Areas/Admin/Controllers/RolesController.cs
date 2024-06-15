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

    public class RolesController : BaseController
    {
        IRoleRepository roleRepository;
        public RolesController()
        {
            roleRepository = new RoleRepository();
        }

        // GET: Admin/Roles
        public async Task<IActionResult> Index(string searchString, int? page, string sortby)
        {
            TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
            var role = await roleRepository.GetAllRole();
            role=role.Where(c => Common.ConvertToUnSign(c.RoleName).Contains(searchString != null ? Common.ConvertToUnSign(TempData["searchString"].ToString()) : "", StringComparison.OrdinalIgnoreCase));
            switch (sortby)
            {
                case "name":
                    role = role.OrderBy(o => o.RoleName);
                    break;
                case "namedesc":
                    role = role.OrderByDescending(o => o.RoleName);
                    break;
                default:
                    break;
            }
            return View(role.ToPagedList(page ?? 1, 5));
        }

        // GET: Admin/Category/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Status")] Role role)
        {
            if (ModelState.IsValid)
            {
                await roleRepository.Add(role);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/Role/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var role = await roleRepository.GetRoleById(Convert.ToInt32(id));
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                await roleRepository.Update(role);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                var role = await roleRepository.GetRoleById(Convert.ToInt32(id));
                if (role == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                await roleRepository.Delete(id);
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

        public async Task<JsonResult> ListName(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var data = await roleRepository.GetRoleByName(q.ToLower());
                var responseData = data.Select(c => c.RoleName).ToList();
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
