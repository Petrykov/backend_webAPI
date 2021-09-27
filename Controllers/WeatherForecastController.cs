using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_dockerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend_dockerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            var client = new MongoClient("mongodb+srv://admin:admin@realmcluster.qwn66.mongodb.net/BookstoreDb?retryWrites=true&w=majority");
            var database = client.GetDatabase("BookstoreDb");
            var collection = database.GetCollection<Book>("Books");
            return collection.Find(book => true).ToList();
        }
    }
}
