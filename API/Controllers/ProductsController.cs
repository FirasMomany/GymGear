using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet] //Get All Products
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }


        [HttpGet("{id:int}")] //api/products/2
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }


        [HttpPost] //Create a Product
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            context.Products.Add(product);

            await context.SaveChangesAsync();

            return product;
        }


        [HttpPut("{Id:int}")] //Update a Product
        public async Task<ActionResult> UpdateProduct(int Id, Product product)
        {
            if (product.Id != Id || !ProductExists(Id))
            {
                return BadRequest("Cannot Update This Product");
            }

            context.Entry(product).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Id:int")]
        public async Task<ActionResult> DeleteProduct(int id)
        {

            var product = await context.Products.FindAsync(id);

            if(product == null) return NotFound();

            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return NoContent();

        }



        private bool ProductExists(int id)
        {
            return context.Products.Any(x => x.Id == id);
        }

    }
}