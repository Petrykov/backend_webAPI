using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Data;
using MongoDB.Driver;

namespace backend_web_api.Models
{
    public class AccountController : Controller
    {

        private IMongoCollection <Client> clientsCollection;

        public AccountController(IMongoClient client)
        {
            var database = client.GetDatabase("BookstoreDb");
            clientsCollection = database.GetCollection<Client>("Clients");
        }
    }
}
