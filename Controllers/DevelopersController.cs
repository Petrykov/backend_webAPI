using System.Collections.Generic;
using backend_dockerAPI.Models;
using backend_dockerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_dockerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : Controller
    {
        private readonly DeveloperService service;

        public DevelopersController(DeveloperService _service)
        {
            service = _service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<Developer>> GetDevelopers()
        {
            return service.GetDevelopers();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Developer> GetDeveloper(string id)
        {
            var developer = service.GetDeveloper(id);
            return Json(developer);
        }

        [HttpPost]
        public ActionResult<Developer> Create(Developer developer)
        {
            service.Create(developer);
            return Json(developer);
        }
    }
}