using backend_web_api.Models;

namespace backend_dockerAPI.Models
{
    public class Company : Client
    {
        public string[] QuizzesIds { get; set; }

        public Company(string email, string password, string name, string img, string[] quizzesIds) : base (email, password, name, img)
        {
            QuizzesIds = quizzesIds;
        }
    }
}
