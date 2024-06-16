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
    public class ProductsController : BaseController
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public ProductsController()
        {
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(string searchString, int? page, string sortby, int categoryId)
        {
            TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
            var product = await productRepository.GetAllProduct();
            product = product.Where(c => Common.ConvertToUnSign(c.ProductName).Contains(searchString != null ? Common.ConvertToUnSign(TempData["searchString"].ToString()) : "", StringComparison.OrdinalIgnoreCase));
            if (categoryId != 0)
            {
                product = product.Where(u => u.CategoryId == categoryId);
            }
            switch (sortby)
            {
                case "name":
                    product = product.OrderBy(o => o.ProductName);
                    break;
                case "namedesc":
                    product = product.OrderByDescending(o => o.ProductName);
                    break;
                default:
                    break;
            }
            ViewBag.Page = 10;
            return View(product.ToPagedList(page ?? 1, (int)ViewBag.Page));
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Category> roles = await categoryRepository.GetAllCategory();
            // Tạo SelectList từ danh sách quyền truy cập
            SelectList selectList = new SelectList(roles, "CategoryId", "CategoryName");

            // Lưu SelectList vào ViewBag để sử dụng trong View
            ViewBag.RoleId = selectList;
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Ncontent,ImageUrl,CategoryId,Price,Stock,DateUpdate,UserPost,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                await productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await productRepository.GetProductById(Convert.ToInt32(id));
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Ncontent,ImageUrl,CategoryId,Price,Stock,DateUpdate,UserPost,Status")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    await productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                var product = await productRepository.GetProductById(id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                await productRepository.Delete(id);
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
            var result = await productRepository.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public async Task<JsonResult> ListName(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var data = await productRepository.GetProductByName(q.ToLower());
                var responseData = data.Select(c => c.ProductName).ToList();
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
