using System.Collections.Generic;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class QuizService
    {
        
        private readonly IMongoCollection <Quiz> quizzes;

        public QuizService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("AdvancedAppDevelopment");
            quizzes = database.GetCollection<Quiz>("Quizzes");
        }

        public List<Quiz> GetQuizzes() => quizzes.Find(quiz => true).ToList();

        public Quiz GetQuiz(string id) => quizzes.Find<Quiz>(quiz => quiz.Id == id).FirstOrDefault();

        public Quiz Create(Quiz quiz)
        {
            quizzes.InsertOne(quiz);
            return quiz;
        }
    }
}