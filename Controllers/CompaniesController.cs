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
    public class CompaniesController : Controller
    {
        private readonly CompanyService service;

        public CompaniesController(CompanyService _service)
        {
            service = _service;
        }

        // Action returns a list of all available companies in DB
        [HttpGet]
        [Authorize]
        public ActionResult<List<Company>> GetCompanies()
        {
            try
            {
                return service.GetCompanies();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Specifying company/{id} an id field in url
        // Returns a single object of Company model if exists by id
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Company> GetCompany(string id)
        {
            Company company = null;

            if (id.ToString().Length == 24)
            {
                company = service.GetCompany(id);
            }

            if (company == null)
            {
                return BadRequest("Company with id [" + id + "] does not exists.");
            }
            return company;
        }

        // Creating a new Company object 
        // Request body fields of a new client (email, password)
        [HttpPost]
        public ActionResult<Company> Create(Company company)
        {
            try
            {
                service.Create(company);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Json(company);
        }
    }
}