using System.Collections.Generic;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class DeveloperService
    {
        private readonly IMongoCollection <Developer> developers;

        public DeveloperService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("AdvancedAppDevelopment");
            developers = database.GetCollection<Developer>("Developers");
        }

        public List<Developer> GetDevelopers() => developers.Find(client => true).ToList();

        public Developer GetDeveloper(string id) => developers.Find<Developer>(developer => developer.Id == id).FirstOrDefault();

        public Developer Create(Developer developer)
        {
            developers.InsertOne(developer);
            return developer;
        }
    }
}