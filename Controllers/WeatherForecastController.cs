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
        private IMongoCollection <Book> bookCollection;
        public WeatherForecastController(IMongoClient client)
        {
            var database = client.GetDatabase("BookstoreDb");
            bookCollection = database.GetCollection<Book>("Books");
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return bookCollection.Find(book => true).ToList();
        }
    }
}
