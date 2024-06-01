using System;
using System.Collections.Generic;

namespace ShopBusinessLogic.Models;

public partial class CategoryNews
{
    public int CategoryNewsId { get; set; }

    public string? CategoryNewsName { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
