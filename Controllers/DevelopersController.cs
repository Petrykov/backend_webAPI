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

        // Action returns a list of all available developers in DB
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

        // Specifying developers/{id} an id field in url
        // Returns a single object of Developer model if exists by id
        [HttpGet("{id}")]
        public ActionResult<Developer> GetDeveloper(string id)
        {
            Developer developer = null;

            if (id.ToString().Length == 24)
            {
                developer = service.GetDeveloper(id);
            }

            if (developer == null)
            {
                return BadRequest("Developer with id [" + id + "] does not exists.");
            }
            return Json(developer);
        }


        // Specifying developers/{id} an id field in url
        // Request body with the fields of the model that must be changed
        // Returns an update object with new fields     
        [HttpPut("{id}")]
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

        // Creating a new Developer object 
        // Request body fields of a new client (email, password)
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