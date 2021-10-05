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
            return service.GetSolvedQuizzes();
        }

        [HttpPost]
        public ActionResult<SolvedQuiz> Create(SolvedQuiz solvedQuiz)
        {
            service.Create(solvedQuiz);
            return Json(solvedQuiz);
        }
    }
}