using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ToDoList.Models
{
    public class ToDoItem
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int EklenmeTarihi { get; set; }

        public string note { get; set; } = string.Empty;

        public bool check { get; set; } 

    }
}

