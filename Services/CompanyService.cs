using System;
using System.Collections.Generic;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> companies;

        public CompanyService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase(configuration.GetValue<string>("databaseName"));
            companies = database.GetCollection<Company>("Companies");
        }

        public List<Company> GetCompanies() => companies.Find(client => true).ToList();

        public Company GetCompany(string id) => companies.Find<Company>(comapny => comapny.Id == id).FirstOrDefault();

        public Company Create(Company company)
        {
            var newCompany = company;

            var hashedPassword = EncodePasswordToBase64(company.Password);
            newCompany.Password = hashedPassword;

            companies.InsertOne(newCompany);
            return newCompany;
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