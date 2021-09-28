using System.Collections.Generic;
using backend_dockerAPI.Services;
using backend_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_dockerAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly UserService service;

        public ClientController(UserService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult<List<Client>> GetUsers()
        {
            return service.GetUsers();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Client> GetUser(string id)
        {
            var user = service.GetUser(id);
            return Json(user);
        }


        [HttpPost]
        public ActionResult<Client> Create(Client client)
        {
            service.Create(client);
            return Json(client);
        }
    }
}