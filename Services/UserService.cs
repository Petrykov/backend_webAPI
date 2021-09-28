using System.Collections.Generic;
using backend_web_api.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection <Client> users;

        public UserService(IMongoClient client)
        {
            var database = client.GetDatabase("BookstoreDb");
            users = database.GetCollection<Client>("Clients");
        }

        public List<Client> GetUsers() => users.Find(user => true).ToList();

        public Client GetUser(string id) => users.Find<Client>(user => user.id == id).FirstOrDefault();

        public Client Create(Client client)
        {
            users.InsertOne(client);
            return client;
        }
    }
}