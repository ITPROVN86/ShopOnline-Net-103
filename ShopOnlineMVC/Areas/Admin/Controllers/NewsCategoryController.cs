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
    public class NewsCategoryController : BaseController
    {
        ICategoryNewsRepository newsCategoryRepository;

        public NewsCategoryController()
        {
            newsCategoryRepository = new CategoryNewsRepository();
        }

        // GET: Admin/NewsCategory
        public async Task<IActionResult> Index(string searchString, int? page, string sortby)
        {
            TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
            var category = await newsCategoryRepository.GetAllCategoryNews();
            category=category.Where(c => Common.ConvertToUnSign(c.CategoryNewsName).Contains(searchString != null ? Common.ConvertToUnSign(TempData["searchString"].ToString()) : "", StringComparison.OrdinalIgnoreCase));
            switch (sortby)
            {
                case "name":
                    category = category.OrderBy(o => o.CategoryNewsName);
                    break;
                case "namedesc":
                    category = category.OrderByDescending(o => o.CategoryNewsName);
                    break;
                default:
                    break;
            }
            ViewBag.Page = 10;
            return View(category.ToPagedList(page ?? 1, (int)ViewBag.Page));
        }


        // GET: Admin/NewsCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryNewsId,CategoryNewsName,Status")] CategoryNews categoryNews)
        {
            if (ModelState.IsValid)
            {
                await newsCategoryRepository.Add(categoryNews);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            return View(categoryNews);
        }

        // GET: Admin/NewsCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var newsCategory = await newsCategoryRepository.GetCategoryNewsById(Convert.ToInt32(id));
            if (newsCategory == null)
            {
                return NotFound();
            }
            return View(newsCategory);
        }

        // POST: Admin/NewsCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryNewsId,CategoryNewsName,Status")] CategoryNews categoryNews)
        {
            if (ModelState.IsValid)
            {
                await newsCategoryRepository.Update(categoryNews);
                SetAlert(Constant.UPDATE_SUCCESS, "success");
                return RedirectToAction(nameof(Index));
            }
            return View(categoryNews);
        }

        // GET: Admin/NewsCategory/Delete/5
        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                var newsCategory = await newsCategoryRepository.GetCategoryNewsById(Convert.ToInt32(id));
                if (newsCategory == null)
                {
                    SetAlert(Constant.DELETE_FAIL, "erro");
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                await newsCategoryRepository.Delete(id);
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
        public async Task<JsonResult> ChangeStatus(int id)
        {
            var result = await newsCategoryRepository.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public async Task<JsonResult> ListName(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var data = await newsCategoryRepository.GetCategoryNewsByName(q.ToLower());
                var responseData = data.Select(c => c.CategoryNewsName).ToList();
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
