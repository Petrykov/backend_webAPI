using System;
using System.Collections.Generic;
using System.Configuration;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class SolvedQuizService
    {
        private readonly IMongoCollection<SolvedQuiz> solvedQuizzes;
        private readonly IMongoCollection<BsonDocument> developers;

        public SolvedQuizService(IMongoClient client, IConfiguration configuration, LoginService _service)
        {
            var database = client.GetDatabase(configuration.GetValue<string>("databaseName"));
            solvedQuizzes = database.GetCollection<SolvedQuiz>("SolvedQuizzes");
            developers = database.GetCollection<BsonDocument>("Developers");
        }

        public List<SolvedQuiz> GetSolvedQuizzes() => solvedQuizzes.Find(solvedQuiz => true).ToList();

        public SolvedQuiz Create(SolvedQuiz solvedQuiz)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(solvedQuiz.SolvedBy));
            var update = Builders<BsonDocument>.Update.Push("SolvedQuizzesIds", solvedQuiz.SolvedQuizId);
            //var update = Builders<BsonDocument>.Update.Set("SolvedQuizzesIds", new string[] { solvedQuiz.SolvedQuizId });

            developers.UpdateOne(filter, update);
            solvedQuizzes.InsertOne(solvedQuiz);
            return solvedQuiz;
        }
    }
}