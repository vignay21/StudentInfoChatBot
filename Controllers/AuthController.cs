using Microsoft.AspNetCore.Mvc;
using StudentInfoChatBot.Models;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private static readonly string AdminUsername = "admin";
    private static readonly string AdminPassword = "admin123";

    [HttpPost("login")]
    public IActionResult Login([FromBody] AdminLoginModel model)
    {
        Console.WriteLine($"🔍 Login Attempt: Username = {model.Username}");

        if (model.Username == AdminUsername && model.Password == AdminPassword)
        {
            Console.WriteLine("✅ Login Successful");
            return Ok(new { message = "Login successful" }); // No JWT needed
        }

        Console.WriteLine("❌ Invalid Credentials");
        return Unauthorized("Invalid credentials");
    }
}
