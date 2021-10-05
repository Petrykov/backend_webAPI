using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_dockerAPI.Models
{
    public class SolvedQuiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SolvedQuizId { get; set; }
        public string SolvedBy { get; set; }
        public int CorrectAnswers { get; set; }
    }
}