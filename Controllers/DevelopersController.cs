using System;
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
            try
            {
                return service.GetDevelopers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Developer> GetDeveloper(string id)
        {
            var developer = service.GetDeveloper(id);
            if (developer == null)
            {
                return BadRequest("Developer with id [" + id + "] does not exists.");
            }
            return Json(developer);
        }

        [HttpPut("{id:length(24)}")]
        [Authorize]
        public ActionResult<Developer> ChangeDeveloper(string id, Developer developer)
        {
            try
            {
                return service.ChangeDeveloper(id, developer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult<Developer> Create(Developer developer)
        {
            try
            {
                service.Create(developer);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(developer);
        }
    }
}