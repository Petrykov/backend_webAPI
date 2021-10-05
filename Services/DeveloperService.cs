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

        public Developer ChangeDeveloper(string id, Developer developer){
            var existingDeveloper = developers.Find<Developer>(developer => developer.Id == id).FirstOrDefault();

            if(developer.Password != null) {existingDeveloper.Password = developer.Password;}
            if(developer.Img != null) {existingDeveloper.Img = developer.Img;}
            if(developer.PhoneNumber != null) {existingDeveloper.PhoneNumber = developer.PhoneNumber;}
            if(developer.City != null) {existingDeveloper.City = developer.City;}
            if(developer.Description != null) {existingDeveloper.Description = developer.Description;}

            developers.ReplaceOne(developer => developer.Id == id, existingDeveloper);

            return existingDeveloper;
        }   

        public Developer Create(Developer developer)
        {
            developers.InsertOne(developer);
            return developer;
        }
    }
}