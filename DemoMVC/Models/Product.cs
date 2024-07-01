using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Product
    {
        [Display(Name ="Mã Sản phẩm")]
        public int ProductId { get; set; }
        [Display(Name ="Tên Sản phẩm")]
        [Required(ErrorMessage ="Yêu cầu nhập tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name ="Đơn giá")]
        [Required(ErrorMessage ="Yêu cầu nhập đơn giá")]
        public decimal UnitPrice { get; set; }
    }
}
