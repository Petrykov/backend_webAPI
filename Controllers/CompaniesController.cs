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
            return service.GetCompanies();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Company> GetCompany(string id)
        {
            var company = service.GetCompany(id);
            return Json(company);
        }

        [HttpPost]
        public ActionResult<Company> Create(Company company)
        {
            service.Create(company);
            return Json(company);
        }
    }
}