using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class LoginService
    {
        private readonly IMongoCollection <Developer> developers;
        private readonly IMongoCollection <Company> companies;
        private readonly string key;

        public LoginService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("AdvancedAppDevelopment");
            developers = database.GetCollection<Developer>("Developers");
            companies = database.GetCollection<Company>("Companies");
            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public string GetClientType (string email) {
            var developer = developers.Find(x => x.Email == email).FirstOrDefault();
            var company = companies.Find(x => x.Email == email).FirstOrDefault();

            if(developer != null){
                return "developer";
            }

            return "company";
        }

        public string Authenticate(string email, string password)
        {
            var developer = developers.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
            var company = companies.Find(x => x.Email == email && x.Password == password).FirstOrDefault();

            if(developer == null && company == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, email),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials (
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}