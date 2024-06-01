using System;
using System.Collections.Generic;

namespace ShopBusinessLogic.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Ncontent { get; set; }

    public DateTime? DateUpdate { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryNewsId { get; set; }

    public int UserPost { get; set; }

    public bool Status { get; set; }

    public virtual CategoryNews CategoryNews { get; set; } = null!;

    public virtual User UserPostNavigation { get; set; } = null!;
}
