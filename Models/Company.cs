using backend_web_api.Models;
using Newtonsoft.Json;

namespace backend_dockerAPI.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Company : Client
    {
        public string[] QuizzesIds { get; set; }

        public Company(string email, string password, string name, string img, string[] quizzesIds) : base (email, password, name, img)
        {
            QuizzesIds = quizzesIds;
        }
    }
}
