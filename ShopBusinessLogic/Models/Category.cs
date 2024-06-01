using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBusinessLogic.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage ="Yêu cầu phải nhập tên danh mục sản phẩm")]
    public string? CategoryName { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
