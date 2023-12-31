﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Logging;
using WebApplication1.Models;
using WebApplication1.Models.Dto;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogging _logger;
        public ProductsController(ILogging logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            _logger.Log("Get all products","");
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
                _logger.Log("ID 0", "error");
                return BadRequest();
            }
            var product = ProductDB.ProductList.FirstOrDefault(P => P.Id == id);
            if ( product == null )
            {
                //logger.LogWarning("Product not found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProductDto> CreateProduct([FromBody]ProductDto productDto)
        {
           
            if (productDto == null)
            {
                return BadRequest(productDto);
            }
            if(ProductDB.ProductList.Any(P => P.Name == productDto.Name))
            {
                ModelState.AddModelError("CustomError", "Product Already Exists");
                return BadRequest(ModelState);
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

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = ProductDB.ProductList.FirstOrDefault(P => P.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ProductDB.ProductList.Remove(product);
            return NoContent();

        }

        [HttpPut("{id:int}",Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateProduct(int id, [FromBody]ProductDto productDto)
        {
            if(productDto == null || id != productDto.Id) 
            {
                return BadRequest();
            }

            var product = ProductDB.ProductList.FirstOrDefault(P => P.Id == id);
            product.Name = productDto.Name;
            product.Qty = productDto.Qty;
            return NoContent();
        }
    }
}
