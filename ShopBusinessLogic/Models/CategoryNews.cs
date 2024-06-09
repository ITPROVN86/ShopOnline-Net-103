using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopBusinessLogic.Models;

public partial class CategoryNews
{
    public int CategoryNewsId { get; set; }

    public string? CategoryNewsName { get; set; }

    [Display(Name ="Trạng thái")]
    public bool Status { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
