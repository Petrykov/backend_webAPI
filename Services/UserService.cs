using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_web_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection <Client> users;
        private readonly string key;

        public UserService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("BookstoreDb");
            users = database.GetCollection<Client>("Clients");
            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public List<Client> GetUsers() => users.Find(user => true).ToList();

        public Client GetUser(string id) => users.Find<Client>(user => user.id == id).FirstOrDefault();

        public Client Create(Client client)
        {
            users.InsertOne(client);
            return client;
        }

        public string Authenticate(string name, string password)
        {
            var client = users.Find(x => x.Name == name && x.Password == password).FirstOrDefault();

            if(client == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, name),
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