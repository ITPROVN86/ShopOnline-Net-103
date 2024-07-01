using DemoMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DemoMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private string ProductURL = "";
        public ProductsController()
        {
            _httpClient = new HttpClient();
            var contentType= new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ProductURL = "https://localhost:7146/api/Products";
        }
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage res = await _httpClient.GetAsync(ProductURL);
            string strData= await res.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(strData,options);
            return View(products);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strData = JsonSerializer.Serialize(p);
                    var contentData= new StringContent(strData,System.Text.Encoding.UTF8,"application/json");
                    HttpResponseMessage res = await _httpClient.PostAsync(ProductURL, contentData);
                    if (res.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Insert success!";
                    }
                    else
                    {
                        ViewBag.Message = "Insert fail!";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(p);
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
