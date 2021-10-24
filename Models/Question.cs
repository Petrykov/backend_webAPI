using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace backend_dockerAPI.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string QuestionValue { get; set; }
        [Required]
        public string[] Answers { get; set; }
        [Required]
        public int CorrectAnswerNumber { get; set; }

        public Question(string questionValue, string[] answers, int correctAnswerNumber)
        {
            QuestionValue = questionValue;
            Answers = answers;
            CorrectAnswerNumber = correctAnswerNumber;
        }
    }
}
