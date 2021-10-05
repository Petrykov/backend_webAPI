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
    public class QuizzesController : Controller
    {
        private readonly QuizService service;

        public QuizzesController(QuizService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult<List<Quiz>> GetQuizzes()
        {
            return service.GetQuizzes();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Quiz> GetQuiz(string id)
        {
            var quiz = service.GetQuiz(id);
            return Json(quiz);
        }

        [HttpPost]
        public ActionResult<Quiz> Create(Quiz quiz)
        {
            service.Create(quiz);
            return Json(quiz);
        }

    }
}