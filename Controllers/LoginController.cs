using backend_dockerAPI.Services;
using backend_web_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_dockerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginService service;
        public LoginController(LoginService _service)
        {
            service = _service;
        }

        [HttpPost]
        public ActionResult Login([FromBody] Client client)
        {
            var token = service.Authenticate(client.Email, client.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { token, client.Email });
        }
    }
}
