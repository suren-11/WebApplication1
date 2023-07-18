using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Dto;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = ProductDB.ProductList;
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            if ( id == 0)
            {
                return BadRequest();
            }
            var product = ProductDB.ProductList.FirstOrDefault(P => P.Id == id);
            if ( product == null )
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
