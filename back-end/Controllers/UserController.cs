using back_end.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<User>> ListAll()
    {
        return Ok();
    }
}