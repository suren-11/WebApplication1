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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = ProductDB.ProductList;
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProductDto> CreateProduct([FromBody]ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(productDto);
            }
            if (productDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            var maxProductId = ProductDB.ProductList.OrderByDescending(P => P.Id).FirstOrDefault().Id;
            productDto.Id = maxProductId + 1;

            ProductDB.ProductList.Add(productDto);
            return CreatedAtRoute("GetProduct", new { id = productDto.Id}, productDto);
        }
    }
}
