using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace APIdev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly CustomerContext _customersContext;

        public ReviewController(CustomerContext customersContext)
        {
            _customersContext = customersContext;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _customersContext.Reviews
                .Include(r => r.Product)
                .ToListAsync();
        }

        // GET: api/Review/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _customersContext.Reviews
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (review == null) return NotFound();

            return review;
        }

        // POST: api/Review
        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(Review review)
        {
            _customersContext.Reviews.Add(review);
            await _customersContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = review.ReviewID }, review);
        }

        // PUT: api/Review/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            if (id != review.ReviewID) return BadRequest();

            _customersContext.Entry(review).State = EntityState.Modified;

            try
            {
                await _customersContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_customersContext.Reviews.Any(r => r.ReviewID == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Review/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _customersContext.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            _customersContext.Reviews.Remove(review);
            await _customersContext.SaveChangesAsync();

            return NoContent();
        }
    }

}
