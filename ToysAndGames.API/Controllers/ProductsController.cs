using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToysAndGames.Data.Models;
using ToysAndGames.DTOs;
using ToysAndGames.Services.Contracts;

namespace ToysAndGames.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        //https://localhost:44338/api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productRepo.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            try
            {
                var product = _productRepo.GetProduct(x => x.Id == id);
                if (product == null)
                {
                    return NotFound($"Status not found with this id ({id})");
                }
                var productDto = _mapper.Map<ProductDTO>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var result = _productRepo.CreateProduct(product);
            if (!result)
            {
                return StatusCode(500, $"Something went wrong when creating the product {product.Name}");
            }
            return StatusCode(201, product);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Id from route doesn't match the body object's Id");
            }
            var product = _mapper.Map<Product>(productDto);
            var result = _productRepo.UpdateProduct(product);
            if (!result)
            {
                return StatusCode(500, $"Something went wrong when updating the product {product.Name}");
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Id from route doesn't match the body object's Id");
            }
            var product = _mapper.Map<Product>(productDto);
            var result = _productRepo.DeleteProduct(product);
            if (!result)
            {
                return StatusCode(500, $"Something went wrong when removing the product {product.Name}");
            }
            return NoContent();
        }
    }
}
