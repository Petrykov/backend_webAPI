using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_web_api.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }

        public Client(string email, string password, string name, string img)
        {
            Email = email;
            Password = password;
            Name = name;
            Img = img;
        }
    }
}
