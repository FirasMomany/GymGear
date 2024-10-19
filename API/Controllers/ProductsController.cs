using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {

        [HttpGet] //Get All Products
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {

            var spec = new ProductSpecification(specParams);
            
            return await CreatePagedResult(repo,spec,specParams.PageIndex,specParams.PageSize);
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
            var spec = new BrandListSpecification();

            return Ok(await repo.ListAsync(spec));
        }


        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await repo.ListAsync(spec));
        }



        private bool ProductExists(int id)
        {
            return repo.Exist(id);
        }

    }
}