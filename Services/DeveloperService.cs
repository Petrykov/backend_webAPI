using System;
using System.Collections.Generic;
using System.Configuration;
using backend_dockerAPI.Configurations;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class DeveloperService
    {
        private readonly IMongoCollection<Developer> developers;
        public DeveloperService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase(configuration.GetValue<string>("databaseName"));
            developers = database.GetCollection<Developer>("Developers");
        }

        public List<Developer> GetDevelopers() => developers.Find(client => true).ToList();

        public Developer GetDeveloper(string id) => developers.Find<Developer>(developer => developer.Id == id).FirstOrDefault();

        public Developer ChangeDeveloper(string id, Developer developer)
        {
            var existingDeveloper = developers.Find<Developer>(developer => developer.Id == id).FirstOrDefault();

            if (developer.Password != null) { existingDeveloper.Password = developer.Password; }
            if (developer.Name != null) { existingDeveloper.Name = developer.Name; }
            if (developer.Img != null) { existingDeveloper.Img = developer.Img; }
            if (developer.PhoneNumber != null) { existingDeveloper.PhoneNumber = developer.PhoneNumber; }
            if (developer.City != null) { existingDeveloper.City = developer.City; }
            if (developer.Description != null) { existingDeveloper.Description = developer.Description; }

            developers.ReplaceOne(developer => developer.Id == id, existingDeveloper);

            return existingDeveloper;
        }

        public Developer Create(Developer developer)
        {
            developer.Password = EncodePasswordToBase64(developer.Password);
            developers.InsertOne(developer);
            return developer;
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
    }
}