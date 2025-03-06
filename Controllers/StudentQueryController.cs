using Microsoft.AspNetCore.Mvc;
using StudentInfoChatBot.Data;
using StudentInfoChatBot.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentInfoChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentQueryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentQueryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/StudentQueries?searchText=admission
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentQuery>>> GetQueries([FromQuery] string searchText = "")
        {
            var queries = await _context.StudentQueries
                .Where(q => string.IsNullOrEmpty(searchText) ||
                            q.Question.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();

            return Ok(queries);
        }

        // ✅ POST: api/StudentQueries (Insert a new query)
        [HttpPost]
        public async Task<ActionResult<StudentQuery>> CreateQuery(StudentQuery query)
        {
            _context.StudentQueries.Add(query);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQueries), new { id = query.Id }, query);
        }

        // ✅ DELETE: api/StudentQueries/{id} (Remove a query)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuery(int id)
        {
            var query = await _context.StudentQueries.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }

            _context.StudentQueries.Remove(query);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}