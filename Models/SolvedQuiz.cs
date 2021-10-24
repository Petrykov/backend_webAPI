using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_dockerAPI.Models
{
    public class SolvedQuiz
    {
        [BsonId]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string SolvedQuizId { get; set; }
        [Required]
        public string SolvedBy { get; set; }
        [Required]
        public int CorrectAnswers { get; set; }
    }
}