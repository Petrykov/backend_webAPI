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
            try
            {
                return service.GetQuestions();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Question> GetQuestion(string id)
        {
            var question = service.GetQuestion(id);
            if (question == null)
            {
                return BadRequest("Question with id [" + id + "] does not exists.");
            }
            return Json(question);
        }

        [HttpPost]
        public ActionResult<Question> Create(Question question)
        {
            try
            {
                service.Create(question);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(question);
        }
    }
}