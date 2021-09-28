using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_web_api.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        
        [BsonElement("Name")]
        public string name { get; set; }

        [BsonElement("Login")]
        public string login { get; set; }

        [BsonElement("Password")]
        public string password { get; set; }
    }
}
