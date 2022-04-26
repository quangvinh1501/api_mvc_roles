using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Models;
using System;
using API.Services;
using Microsoft.AspNetCore.Cors;
using API.Entities;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly minidressContext context;
        private readonly IAdminReposity IAdminReposity;
        public AdminController(IAdminReposity IAdminReposity, minidressContext context)
        {
            this.IAdminReposity = IAdminReposity;
            this.context = context;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthenticateRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (context.Admins.Any(x => x.Email == model.Email))
                    {
                        var user = IAdminReposity.Login(model);
                        if (null != user)
                        {
                            return Ok(user);
                        }
                        else
                        {
                            return NotFound(new { title = "Password is incorrect", status = 404 });
                        }
                    }
                    else
                    {
                        return NotFound(new { title = "Email not existing", status = 404 });
                    }
                }
                else
                {
                    var json = JsonSerializer.Serialize(ModelState);
                    return BadRequest(json);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Authorize(Roles="SuperAdmin,Admin")]
        [HttpGet("getallusers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var employeeList = context.Admins.OrderByDescending(a => a.Id).ToList();
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
