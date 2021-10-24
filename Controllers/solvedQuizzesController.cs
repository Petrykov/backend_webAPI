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
    [Authorize]
    public class solvedQuizzesController : Controller
    {
        private readonly SolvedQuizService service;

        public solvedQuizzesController(SolvedQuizService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult<List<SolvedQuiz>> GetSolvedQuizzes()
        {
            try
            {
                return service.GetSolvedQuizzes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<SolvedQuiz> Create(SolvedQuiz solvedQuiz)
        {
            try
            {
                service.Create(solvedQuiz);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(solvedQuiz);
        }
    }
}