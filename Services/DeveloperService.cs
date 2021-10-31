using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using backend_dockerAPI.Models;
using backend_dockerAPI.Helpers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace backend_dockerAPI.Services
{
    public class DeveloperService
    {
        private readonly IMongoCollection<Developer> developers;
        PasswordEncryption passwordEncryption = new PasswordEncryption();
        public DeveloperService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase(configuration.GetValue<string>("databaseName"));
            developers = database.GetCollection<Developer>("Developers");
        }

        public List<Developer> GetDevelopers() => developers.Find(client => true).ToList();

        public Developer GetDeveloper(string id) => developers.Find<Developer>(developer => developer.Id == id).FirstOrDefault();

        public Developer ChangeDeveloper(string id, Developer developer)
        {
            Developer existingDeveloper = developers.Find<Developer>(developer => developer.Id == id).FirstOrDefault();

            string[] developerFields = new string[] { "Password", "Name", "Img", "PhoneNumber", "City", "Description", "OccupationField" };

            Developer changedDeveloper = checkFields(existingDeveloper, developer, developerFields);
            developers.ReplaceOne(developer => developer.Id == id, changedDeveloper);

            return changedDeveloper;
        }

        private Developer checkFields(Developer existing, Developer requestBody, string[] array)
        {
            dynamic dynData = JsonConvert.DeserializeObject(requestBody.ToJson());

            foreach (var item in array)
            {
                if (dynData[item].Value != null)
                {
                    if (item == "Password")
                    {
                        var hashedPassword = passwordEncryption.HashPassword(dynData[item].Value);
                        existing.GetType().GetProperty(item).SetValue(existing, hashedPassword);
                    }else{
                        existing.GetType().GetProperty(item).SetValue(existing, dynData[item].Value);
                    }
                }
            }
            return existing;
        }

        public Developer Create(Developer developer)
        {
            var newDeveloer = developer;

            var hashedPassword = passwordEncryption.HashPassword(developer.Password);
            newDeveloer.Password = hashedPassword;

            developers.InsertOne(newDeveloer);
            return newDeveloer;
        }
    }
}