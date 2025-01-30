using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace APIdev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CustomerContext _customersContext;

        public ProductController(CustomerContext customersContext)
        {
            _customersContext = customersContext;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _customersContext.Products.Include(p => p.Reviews).ToListAsync();
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _customersContext.Products
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null) return NotFound();

            return product;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _customersContext.Products.Add(product);
            await _customersContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductID) return BadRequest();

            _customersContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _customersContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_customersContext.Products.Any(p => p.ProductID == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _customersContext.Products.FindAsync(id);
            if (product == null) return NotFound();

            _customersContext.Products.Remove(product);
            await _customersContext.SaveChangesAsync();

            return NoContent();
        }
    }

}
