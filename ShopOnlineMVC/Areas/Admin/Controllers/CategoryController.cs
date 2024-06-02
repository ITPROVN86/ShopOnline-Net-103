using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class CategoryController : BaseController
    {
        //private readonly Net103Context _context;
        ICategoryRepository categoryRepository = null;


        public CategoryController()
        {
            categoryRepository = new CategoryRepository();
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index(string searchString, int? page, string sortby)
        {
            TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
            var category = categoryRepository.GetAllCategory().Where(c => Common.ConvertToUnSign(c.CategoryName).Contains(searchString != null ? Common.ConvertToUnSign(TempData["searchString"].ToString()) : "", StringComparison.OrdinalIgnoreCase));
            switch (sortby)
            {
                case "name":
                    category = category.OrderBy(o => o.CategoryName);
                    break;
                case "namedesc":
                    category = category.OrderByDescending(o => o.CategoryName);
                    break;
                default:
                    break;
            }
            return View(category.ToPagedList(page ?? 1, 5));
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var category = categoryRepository.GetCategoryById(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Update(category);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        public JsonResult DeleteId(int id)
        {
            try
            {
                var category = categoryRepository.GetCategoryById(Convert.ToInt32(id));
                if (category == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                categoryRepository.Delete(id);
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

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = categoryRepository.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
