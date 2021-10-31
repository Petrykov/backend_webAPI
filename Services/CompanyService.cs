using System.Collections.Generic;
using backend_dockerAPI.Models;
using backend_dockerAPI.Helpers;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> companies;

        PasswordEncryption passwordEncryption = new PasswordEncryption();

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

            var hashedPassword = passwordEncryption.HashPassword(company.Password);
            newCompany.Password = hashedPassword;

            companies.InsertOne(newCompany);
            return newCompany;
        }
    }
}