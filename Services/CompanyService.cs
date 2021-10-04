using System;
using System.Collections.Generic;
using backend_dockerAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace backend_dockerAPI.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection <Company> companies;

        public CompanyService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase("AdvancedAppDevelopment");
            companies = database.GetCollection<Company>("Companies");
        }

        public List<Company> GetCompanies() => companies.Find(client => true).ToList();

        public Company GetCompany(string id) => companies.Find<Company>(comapny => comapny.Id == id).FirstOrDefault();

        public Company Create(Company company)
        {
            companies.InsertOne(company);
            return company;
        }
    }
}