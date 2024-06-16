using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBusinessLogic.Models;

public partial class Product
{
    public int ProductId { get; set; }

    [Display(Name ="Tên sản phẩm")]
    [Required(ErrorMessage ="Yêu cầu nhập tên sản phẩm")]
    public required string ProductName { get; set; }
    [Display(Name = "Mô tả")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }
    [Display(Name = "Nội dung")]
    public string? Ncontent { get; set; }
    [Display(Name = "Hình ảnh")]
    public string? ImageUrl { get; set; }
    [Display(Name = "Chọn danh mục sản phẩm")]
    public int CategoryId { get; set; }
    [Display(Name = "Đơn giá")]
    public decimal? Price { get; set; }
    [Display(Name = "Số lượng")]
    public int? Stock { get; set; }
    [Display(Name = "Ngày cập nhật")]
    public DateTime? DateUpdate { get; set; }

    public int? UserPost { get; set; }
    [Display(Name = "Trạng thái")]
    public bool Status { get; set; }

    public virtual Category? Category { get; set; } = null!;

    public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? UserPostNavigation { get; set; } = null!;
}
