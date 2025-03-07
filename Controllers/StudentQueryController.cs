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
                .Where(q => q.IsValid == true &&
                            q.Question != null && q.Answer != null &&
                            (string.IsNullOrEmpty(searchText) ||
                             q.Question.ToLower().Contains(searchText.ToLower())))
                .ToListAsync();

            if (!queries.Any())
            {
                return Ok(new { message = "Sorry, I don't understand." });
            }

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

        [HttpPost("reportInvalid")]
        public IActionResult ReportInvalidQuery([FromBody] ReportInvalidDto request)
        {
            var query = _context.StudentQueries.FirstOrDefault(q => q.Id == request.Id);
            if (query == null)
            {
                return NotFound(new { message = "Query not found" });
            }

            query.IsInvalid = true;
            query.IsValid = false;  // ✅ Mark as NOT valid
            _context.SaveChanges();

            return Ok(new { message = "Query marked as invalid" });
        }


        [HttpGet("Invalid")]
        public IActionResult GetInvalidQueries()
        {
            var invalidQueries = _context.StudentQueries
                .Where(q => q.IsInvalid == true && q.IsValid == false) // ✅ Ensure both conditions are met
                .Select(q => new { id = q.Id, question = q.Question, answer = q.Answer })
                .ToList();

            return Ok(invalidQueries);
        }


        // ✅ PUT: api/StudentQueries/UpdateAnswer/{id} (Admin updates answer)
        [HttpPut("UpdateAnswer/{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, [FromBody] string newAnswer)
        {
            var query = await _context.StudentQueries.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }

            query.Answer = newAnswer;
            query.IsValid = true; // Mark as valid after update
            await _context.SaveChangesAsync();

            return Ok(new { message = "Answer updated successfully" });
        }

        // ✅ DELETE: api/StudentQueries/Delete/{id} (Admin deletes query)
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteQuery(int id)
        {
            var query = await _context.StudentQueries.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }

            _context.StudentQueries.Remove(query);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Query deleted successfully" });
        }
        [HttpPost("Admin/Login")]
        public IActionResult AdminLogin([FromBody] AdminLoginModel login)
        {
            if (login.Username == "admin" && login.Password == "admin123")
            {
                return Ok(new { token = "sample-jwt-token" }); // Simulated token
            }
            return Unauthorized();
        }

    }
}