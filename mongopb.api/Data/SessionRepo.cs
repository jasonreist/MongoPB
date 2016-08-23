using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mongopb.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace mongopb.Data
{
    class SessionRepo : ISessionRepo
    {
        IMongoDatabase _database;
        protected IMongoCollection<Session> _contacts;
        
        public SessionRepo()
        {
            string connection = "mongodb://localhost:27017";

            MongoClient client = new MongoClient(connection);

            _database = client.GetDatabase("PB");
            _contacts = _database.GetCollection<Session>("Session");
        }


        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Session>> Get()
        {
            List<Session> list = new List<Session>();
            var filter = new BsonDocument();
            using (var cursor = await _contacts.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        list.Add(document);
                    }
                }
            }

            return list;
        }

        public async Task<Session> Get(string id)
        {
            Session s = null;
            var filter = new BsonDocument("userid", id);
            using (var cursor = await _contacts.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        // process document
                        if (document.userid == id)
                        {
                            s = document;
                            break;
                        }
                    }
                }
            }
            return s;
        }

        public Session Post(Session item)
        {
            item.Id = ObjectId.GenerateNewId();
            item.expires = DateTime.UtcNow.AddMinutes(20);
            _contacts.InsertOne(item);
            return item;
        }

        public bool Put(Session item)
        {
            UpdateDefinition<Session> u = Builders<Session>.Update
                .Set("Expires", DateTime.UtcNow.AddMinutes(20));

            MongoDB.Driver.UpdateOptions uo = new UpdateOptions();

            FilterDefinition<Session> f = Builders<Session>.Filter.Eq("userid", item.userid);
            UpdateResult result = _contacts.UpdateOne(f, u, uo, System.Threading.CancellationToken.None);
            return result.ModifiedCount == 1;
        }
    }
}
