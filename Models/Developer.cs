using backend_web_api.Models;

namespace backend_dockerAPI.Models
{
    public class Developer : Client
    {
        public string [] SolvedQuizzesIds { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}
