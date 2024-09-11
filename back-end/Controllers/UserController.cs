using back_end.Models;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<User>> ListAll()
    {
        var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Birthday = new DateOnly(1990, 1, 1), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new User { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Birthday = new DateOnly(1992, 2, 2), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
        };

        return Ok(users);
    }
}