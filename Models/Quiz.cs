using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_dockerAPI.Models
{
    public class Quiz
    {
        [BsonId]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public string Name { get; set; }
        public string RequiredStack { get; set; }
        [Required]
        public int duration { get; set; }
        [Required]
        public string[] QuestionIds { get; set; }
        public int Complexity { get; set; }

        public Quiz(string creatorId, string name, string requiredStack, int duration, string[] questionIds, int complexity)
        {
            CreatorId = creatorId;
            Name = name;
            RequiredStack = requiredStack;
            this.duration = duration;
            QuestionIds = questionIds;
            Complexity = complexity;
        }
    }
}
