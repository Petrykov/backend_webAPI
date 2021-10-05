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
    public class QuestionsController : Controller
    {
        private readonly QuestionService service;

        public QuestionsController(QuestionService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult<List<Question>> GetQuestions()
        {
            return service.GetQuestions();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Question> GetQuestion(string id)
        {
            var questions = service.GetQuestion(id);
            return Json(questions);
        }

        [HttpPost]
        public ActionResult<Question> Create(Question question)
        {
            service.Create(question);
            return Json(question);
        } 
    }
}