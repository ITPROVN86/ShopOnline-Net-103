using System;
using System.Collections.Generic;

namespace ShopBusinessLogic.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public string? Ncontent { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public DateTime? DateUpdate { get; set; }

    public int UserPost { get; set; }

    public bool? Status { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User UserPostNavigation { get; set; } = null!;
}
