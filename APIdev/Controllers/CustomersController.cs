using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace APIdev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _customersContext;

        public CustomersController(CustomerContext customersContext)
        {
            _customersContext = customersContext;
        }

        // GET: api/Costumers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCostumers()
        {
            return await _customersContext.Costumers.ToListAsync();
        }

        // GET: api/costumers/all
        [HttpGet("GetAll")]
        public IActionResult getAllCostumers()
        {
            return Ok(_customersContext.Costumers);
        }

        // GET: api/Costumers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCostumer(int id)
        {
            var costumer = await _customersContext.Costumers.FindAsync(id);

            if (costumer == null)
            {
                return NotFound();
            }

            return costumer;
        }

        // PUT: api/Costumers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCostumer(int id, Customer costumer)
        {
            if (id != costumer.CustomerId)
            {
                return BadRequest();
            }

            _customersContext.Entry(costumer).State = EntityState.Modified;

            try
            {
                await _customersContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CostumerExists(id))
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

        // POST: api/Costumers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCostumer(Customer costumer)
        {
            _customersContext.Costumers.Add(costumer);
            await _customersContext.SaveChangesAsync();

            return CreatedAtAction("GetCostumer", new { id = costumer.CustomerId }, costumer);
        }

        // DELETE: api/Costumers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCostumer(int id)
        {
            var costumer = await _customersContext.Costumers.FindAsync(id);
            if (costumer == null)
            {
                return NotFound();
            }

            _customersContext.Costumers.Remove(costumer);
            await _customersContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CostumerExists(int id)
        {
            return _customersContext.Costumers.Any(e => e.CustomerId == id);
        }
    }
}
