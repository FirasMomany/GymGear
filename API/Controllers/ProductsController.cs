using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {


        [HttpGet] //Get All Products
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand , string? type,string? sort)
        {

            var spec = new ProductSpecification(brand,type,sort);

            var products = await repo.ListAsync(spec);


            return Ok(products);
        }


        [HttpGet("{id:int}")] //Get One Product 
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            return product;
        }


        [HttpPost] //Create a Product
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {

            repo.Add(product);

            if (await repo.SaveAllAsync())
            {

                return CreatedAtAction("GetProduct", new { id = product.Id }, product);

            }

            return BadRequest("Problem Creating Product");
        }


        [HttpPut("{Id:int}")] //Update a Product
        public async Task<ActionResult> UpdateProduct(int Id, Product product)
        {
            if (product.Id != Id || !ProductExists(Id))
            {
                return BadRequest("Cannot Update This Product");
            }

            repo.Update(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();

            }

            return BadRequest("Problem Updating the Product");

        }

        [HttpDelete("Id:int")] //Delete a Product
        public async Task<ActionResult> DeleteProduct(int id)
        {

            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            repo.Remove(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();

            }

            return BadRequest("Problem Deleting the Product");

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok();

        }


        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok();
        }



        private bool ProductExists(int id)
        {
            return repo.Exist(id);
        }

    }
}