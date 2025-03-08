using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInfoChatBot.Data;

[Route("api/AdminPanel")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
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
}
