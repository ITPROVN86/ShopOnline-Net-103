using Microsoft.AspNetCore.Mvc;

namespace ShopOnlineMVC.Areas.Admin.Controllers
{
    public class BaseController: Controller
    {
        public void SetAlert(string msg, string type)
        {
            TempData["AlertMessage"] = msg;
            if (type == "success")
            {
                TempData["Type"] = "alert-success";
            }
            if (type == "warning")
            {
                TempData["Type"] = "alert-warning";
            }
            if (type == "error")
            {
                TempData["Type"] = "alert-danger";
            }
        }
    }
}
