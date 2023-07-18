using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class ProductDB
    {
        public static List<Product> ProductList = new List<Product>() {
                new Product{ Id = 1, Name = "product1" },
                new Product{ Id = 2, Name = "product2" }
            };
    }
}
