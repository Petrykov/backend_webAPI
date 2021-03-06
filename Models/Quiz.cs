using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace backend_dockerAPI.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public string Name { get; set; }
        public string RequiredStack { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string[] QuestionIds { get; set; }
        public int Complexity { get; set; }

        public Quiz(string creatorId, string name, string requiredStack, int duration, string[] questionIds, int complexity)
        {
            CreatorId = creatorId;
            Name = name;
            RequiredStack = requiredStack;
            Duration = duration;
            QuestionIds = questionIds;
            Complexity = complexity;
        }
    }
}
