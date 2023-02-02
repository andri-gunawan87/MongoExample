using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services
{
    public class MongoDBServices
    {
        private readonly IMongoCollection<SampleTable> _tableCollection;

        public MongoDBServices(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _tableCollection = database.GetCollection<SampleTable>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<SampleTable>> GetAsync() {
            return await _tableCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task CreateAsync(SampleTable sampleTable) {
            await Task.Run(() => _tableCollection.InsertOne(sampleTable));
            return;
        }
        public async Task AddToPlaylistAsync(string id, string movieId) {
            FilterDefinition<SampleTable> filter = Builders<SampleTable>.Filter.Eq("Id", id);
            UpdateDefinition<SampleTable> update = Builders<SampleTable>.Update.AddToSet<string>("movieIds", movieId);
            await _tableCollection.UpdateOneAsync(filter, update);
            return;
        }
        public async Task DeleteAsync(string id) {
            FilterDefinition<SampleTable> filter = Builders<SampleTable>.Filter.Eq("Id", id);
            await _tableCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
