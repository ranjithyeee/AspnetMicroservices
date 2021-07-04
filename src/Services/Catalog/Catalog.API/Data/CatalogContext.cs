using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration Config)
        {
            var client = new MongoClient(Config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(Config.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(Config.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
