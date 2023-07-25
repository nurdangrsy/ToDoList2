using ToDoList.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace ToDoList.Services
{
    public class TodosService
    {
        private readonly IMongoCollection<ToDoItem> _todosCollection;

        public TodosService(
            IOptions<TodoListDatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _todosCollection = mongoDatabase.GetCollection<ToDoItem>("Items");
        }

        public async Task<List<ToDoItem>> GetAsync() =>
            await _todosCollection.Find(_ => true).ToListAsync();

        public async Task<ToDoItem?> GetAsync(string id) =>
            await _todosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ToDoItem newTodo) =>
            await _todosCollection.InsertOneAsync(newTodo);

        public async Task UpdateAsync(string id, ToDoItem updatedTodo) =>
            await _todosCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);

        public async Task RemoveAsync(string id) =>
            await _todosCollection.DeleteOneAsync(x => x.Id == id);
    }
}

