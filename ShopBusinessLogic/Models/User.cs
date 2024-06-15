using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBusinessLogic.Models;

public partial class User
{
    public int UserId { get; set; }

    [Display(Name = "Tên đăng nhập")]
    [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
    public string UserName { get; set; } = null!;

    [Display(Name = "Địa chỉ email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [Required(ErrorMessage = "Yêu cầu nhập thư điện tử")]

    public string Email { get; set; } = null!;
    [Display(Name = "Mật khẩu")]
    [DataType(DataType.Password)]

    public string? Password { get; set; } = null!;
    [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
    [Display(Name = "Họ tên")]
    public string? FullName { get; set; }

    [Display(Name = "Vai trò")]
    [Required(ErrorMessage = "Yêu cầu chọn vai trò")]
    public int RoleId { get; set; }

    [Display(Name = "Trạng thái")]
    public bool Status { get; set; }

    public virtual ICollection<News>? News { get; set; } = new List<News>();

    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product>? Products { get; set; } = new List<Product>();

    [Display(Name = "Vai trò")]
    public virtual Role? Role { get; set; } = null!;
}
