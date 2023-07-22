using WebApplication1.Models;
using WebApplication1.Models.Dto;

namespace WebApplication1.Data
{
    public static class ProductDB
    {
        public static List<ProductDto> ProductList = new List<ProductDto>() {
                new ProductDto{ Id = 1, Name = "product1", Qty = 10 },
                new ProductDto{ Id = 2, Name = "product2", Qty = 20 }
            };
    }
}
