
using System.Collections.Generic;
using backend_dockerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class QuestionService 
    {
        private readonly IMongoCollection <Question> questions;

        public QuestionService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase(configuration.GetValue<string>("databaseName"));
            questions = database.GetCollection<Question>("Questions");
        }

        public List<Question> GetQuestions() => questions.Find(question => true).ToList();

        public Question GetQuestion(string id) => questions.Find<Question>(question => question.Id == id).FirstOrDefault();

        public Question Create(Question question)
        {
            questions.InsertOne(question);
            return question;
        }
    }
}