using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Common
{
    public class MongoDBRepository : IRepository
    {
        private MongoClient _client;
        private IMongoCollection<ToDoItem> _toDoItems;
        public MongoDBRepository(MongoDBConfig config)
        {
            _client = new MongoClient(config.ConnectionString);
        }
        public async Task InitAsync()
        {
            var db = _client.GetDatabase("test");
            _toDoItems = db.GetCollection<ToDoItem>("ToDoItem");

            //create index
        }

        public async Task DeleteAsync(string id)
        {
            await _toDoItems.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<ToDoItem> GetAsync(string id)
        {
            return await _toDoItems.FindSync(t => t.Id == id).FirstAsync();
        }

        public async Task<List<ToDoItem>> QueryAsync(string description, bool? done)
        {
            return null;
        }

        public async Task<ToDoItem> UpdateAsync(string id, ToDoItemUpdateModel updateModel)
        {
            return null;
        }

        public async Task UpsertAsync(ToDoItem model)
        {
            var opt = new ReplaceOptions { IsUpsert = true };
            await _toDoItems.ReplaceOneAsync(t => t.Id == model.Id, model, opt);
        }
    }

    public class MongoDBConfig
    {
        public string ConnectionString { get; set; }
    }

}
