using AmazonCrud.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AmazonCrud.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        public ProductService(IOptions<DataBaseSettingsAmazon> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = client.GetDatabase(databaseSettings.Value.DatabaseName);

            _productCollection = mongoDatabase.GetCollection<Product>(databaseSettings.Value.CollectionName);
        }
        public async Task<List<Product>> GetAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync();       
        }
        public async Task<Product?> GetAsync(int code)
        {
            return await _productCollection.Find(x => x.code == code).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Product movie)
        {
            await _productCollection.InsertOneAsync(movie);   
        }
        public async Task UpdateAsync(int code, Product updatedMovie)
        {
            await _productCollection.ReplaceOneAsync(x => x.code == code, updatedMovie);
        }
        public async Task RemoveAsync(int code)
        {
            await _productCollection.DeleteOneAsync(x => x.code == code);
        }
    }
}
