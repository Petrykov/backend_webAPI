using System.Collections.Generic;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class SolvedQuizService
    {
        private readonly IMongoCollection <SolvedQuiz> solvedQuizzes;

        public SolvedQuizService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("AdvancedAppDevelopment");
            solvedQuizzes = database.GetCollection<SolvedQuiz>("SolvedQuizzes");
        }

        public List<SolvedQuiz> GetSolvedQuizzes() => solvedQuizzes.Find(solvedQuiz => true).ToList();

        public SolvedQuiz Create(SolvedQuiz solvedQuiz)
        {
            solvedQuizzes.InsertOne(solvedQuiz);
            return solvedQuiz;
        }
    }
}