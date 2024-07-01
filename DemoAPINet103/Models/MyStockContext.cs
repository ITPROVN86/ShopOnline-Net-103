using Microsoft.EntityFrameworkCore;

namespace DemoAPINet103.Models
{
    public class MyStockContext: DbContext
    {
        public MyStockContext(DbContextOptions<MyStockContext> options): base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }
    }
}
