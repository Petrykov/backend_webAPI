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

        [HttpGet("{id:length(24)}")]
        [Authorize]
        public ActionResult<Company> GetCompany(string id)
        {
            var company = service.GetCompany(id);
            if (company == null)
            {
                return BadRequest("Company with id [" + id + "] does not exists.");
            }
            return company;
        }

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