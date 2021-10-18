using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_dockerAPI.Models;
using backend_web_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<Developer> developers;
        private readonly IMongoCollection<Company> companies;
        private readonly string key;

        public LoginService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("AdvancedAppDevelopment");
            developers = database.GetCollection<Developer>("Developers");
            companies = database.GetCollection<Company>("Companies");
            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public string GetClientType(string email)
        {
            var developer = developers.Find(x => x.Email == email).FirstOrDefault();
            var company = companies.Find(x => x.Email == email).FirstOrDefault();

            if (developer != null)
            {
                return "developer";
            }

            return "company";
        }

        public Client getClient(string email)
        {
            return developers.Find(x => x.Email == email).FirstOrDefault();
        }

        public Company getCompany(string email)
        {
            return companies.Find(x => x.Email == email).FirstOrDefault();
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string Authenticate(string email, string password)
        {
            var encodedInput = EncodePasswordToBase64(password);
            var developer = developers.Find(x => x.Email == email && x.Password == encodedInput).FirstOrDefault();
            var company = companies.Find(x => x.Email == email && x.Password == encodedInput).FirstOrDefault();

            if (developer == null && company == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, email),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}