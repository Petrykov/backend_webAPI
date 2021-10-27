using backend_web_api.Models;

namespace backend_dockerAPI.Models
{
    public class Developer : Client
    {
        public string [] SolvedQuizzesIds { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string OccupationField { get; set; }

        public Developer(string email, string password, string name, string img, string[] solvedQuizzesIds, string phoneNumber, string city, string description, string occupationField) : base(email, password, name, img)
        {
            SolvedQuizzesIds = solvedQuizzesIds;
            PhoneNumber = phoneNumber;
            City = city;
            Description = description;
            OccupationField = occupationField;
        }

    }
}
