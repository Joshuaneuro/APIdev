using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Domain;
using DAL;

namespace APIdev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly CustomerContext _customersContext;

        public InvoiceController(CustomerContext customersContext)
        {
            _customersContext = customersContext;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _customersContext.Invoices
                .Include(i => i.Customer) // Include customer data
                .Include(i => i.InvoiceProducts) // Include related products
                .ToListAsync();
        }

        // GET: api/Invoice/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _customersContext.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceProducts)
                    .ThenInclude(ip => ip.Product)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null) return NotFound();

            return invoice;
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            _customersContext.Invoices.Add(invoice);
            await _customersContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceId }, invoice);
        }

        // PUT: api/Invoice/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceId) return BadRequest();

            _customersContext.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _customersContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_customersContext.Invoices.Any(i => i.InvoiceId == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Invoice/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _customersContext.Invoices.FindAsync(id);
            if (invoice == null) return NotFound();

            _customersContext.Invoices.Remove(invoice);
            await _customersContext.SaveChangesAsync();

            return NoContent();
        }
    }

}
