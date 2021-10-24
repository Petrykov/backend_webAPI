using backend_dockerAPI.Services;
using backend_web_api.Models;
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

            var clientData = service.getClient(client.Email);
            var companyData = service.getCompany(client.Email);
            var user = clientData != null ? clientData : companyData;

            var clientType = service.GetClientType(client.Email);
            return Ok(new { token, clientType, user });
        }
    }
}
