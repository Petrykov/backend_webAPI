using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace backend_dockerAPI.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Quesiton { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswerNumber { get; set; }
    }
}
