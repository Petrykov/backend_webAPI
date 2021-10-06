using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_dockerAPI.Models
{
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string RequiredStack { get; set; }
        public int duration { get; set; }
        public string[] QuestionIds { get; set; }
        public int Complexity { get; set; }
    }
}
