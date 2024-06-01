using System;
using System.Collections.Generic;

namespace ShopBusinessLogic.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserOrder { get; set; }

    public DateTime? DateOrder { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User UserOrderNavigation { get; set; } = null!;
}
