using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(IWebHostEnvironment webHostEnvironment)
        {
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
            userRepository = new UserRepository();
            this.webHostEnvironment = webHostEnvironment;
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

            /*IEnumerable<Category> roles = await categoryRepository.GetAllCategory();
            // Tạo SelectList từ danh sách quyền truy cập
            SelectList selectList = new SelectList(roles, "CategoryId", "CategoryName");

            // Lưu SelectList vào ViewBag để sử dụng trong View
            ViewBag.CategoryId = selectList;*/
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName");

            ViewBag.Page = 10;
            return View(product.ToPagedList(page ?? 1, (int)ViewBag.Page));
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllCategory(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Ncontent,ImageUrl,ImageFile,CategoryId,Price,Stock,DateUpdate,UserPost,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                var user = User as ClaimsPrincipal;
                var userName = user?.FindFirstValue(ClaimTypes.Name);
                var userInfo = await userRepository.GetAllUser();
                userInfo = userInfo.Where(u => u.UserName == userName);
                product.UserPost = userInfo.ToList()[0].UserId;

                product.DateUpdate = Common.GetServerDateTime();
                if (product.ImageFile != null)
                {
                    string uniqueFileName = UploadedFile(product);
                    product.ImageUrl = uniqueFileName;
                }
                await productRepository.Add(product);
                SetAlert(Constant.UPDATE_SUCCESS, Constant.SUCCESS);
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
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Ncontent,ImageUrl,ImageFile,CategoryId,Price,Stock,DateUpdate,UserPost,Status")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uniqueFileName = UploadedFile(product);
                    product.ImageUrl = uniqueFileName;
                }
                await productRepository.Update(product);
                SetAlert(Constant.UPDATE_SUCCESS, Constant.SUCCESS);
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

        public string UploadedFile(Product product)
        {
            string wwwRootPath = this.webHostEnvironment.WebRootPath;
            var exe = product.ImageFile.FileName;
            string fileName = Path.GetFileNameWithoutExtension(exe);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            product.ImageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Upload/Avatar/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                product.ImageFile.CopyTo(fileStream);
            }
            ViewBag.Anh = product.ImageUrl;
            return fileName;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                // Get the current date and format it
                var currentDate = DateTime.Now;
                var year = currentDate.Year.ToString();
                var month = currentDate.Month.ToString().PadLeft(2, '0');
                var day = currentDate.Day.ToString().PadLeft(2, '0');

                // Create the directory string and ensure the directory exists
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/images", year, month);
                Directory.CreateDirectory(directoryPath); // Creates all directories on the path if not exist

                // Modify the filename to include the date
                var fileName = $"{year}{month}{day}_{Path.GetFileName(upload.FileName)}";
                var filePath = Path.Combine(directoryPath, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }

                // Return the JSON result with the URL to the uploaded file
                return Json(new { uploaded = true, url = $"/upload/images/{year}/{month}/{fileName}" });
            }

            return Json(new { uploaded = false, message = "Upload failed" });
        }
    }
}
