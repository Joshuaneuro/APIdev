
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;

    namespace APIdev.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class InvoiceProductsController : ControllerBase
        {
        private readonly CustomerContext _customersContext;

        public InvoiceProductsController(CustomerContext customersContext)
        {
            _customersContext = customersContext;
        }

        // GET: api/InvoiceProducts
        [HttpGet]
            public async Task<ActionResult<IEnumerable<InvoiceProduct>>> GetInvoiceProducts()
            {
                return await _customersContext.InvoiceProducts
                    .Include(ip => ip.Invoice)
                    .Include(ip => ip.Product)
                    .ToListAsync();
            }

            // GET: api/InvoiceProducts/5
            [HttpGet("{id}")]
            public async Task<ActionResult<InvoiceProduct>> GetInvoiceProduct(int id)
            {
                var invoiceProduct = await _customersContext.InvoiceProducts
                    .Include(ip => ip.Invoice)
                    .Include(ip => ip.Product)
                    .FirstOrDefaultAsync(ip => ip.InvoiceId == id);

                if (invoiceProduct == null)
                {
                    return NotFound();
                }

                return invoiceProduct;
            }

            // POST: api/InvoiceProducts
            [HttpPost]
            public async Task<ActionResult<InvoiceProduct>> PostInvoiceProduct(InvoiceProduct invoiceProduct)
            {
            _customersContext.InvoiceProducts.Add(invoiceProduct);
                await _customersContext.SaveChangesAsync();

                return CreatedAtAction("GetInvoiceProduct", new { id = invoiceProduct.InvoiceId }, invoiceProduct);
            }

            // PUT: api/InvoiceProducts/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutInvoiceProduct(int id, InvoiceProduct invoiceProduct)
            {
                if (id != invoiceProduct.InvoiceId)
                {
                    return BadRequest();
                }

            _customersContext.Entry(invoiceProduct).State = EntityState.Modified;

                try
                {
                    await _customersContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // DELETE: api/InvoiceProducts/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteInvoiceProduct(int id)
            {
                var invoiceProduct = await _customersContext.InvoiceProducts.FindAsync(id);
                if (invoiceProduct == null)
                {
                    return NotFound();
                }

                _customersContext.InvoiceProducts.Remove(invoiceProduct);
                await _customersContext.SaveChangesAsync();

                return NoContent();
            }

            private bool InvoiceProductExists(int id)
            {
                return _customersContext.InvoiceProducts.Any(e => e.InvoiceId == id);
            }
        }
    }
