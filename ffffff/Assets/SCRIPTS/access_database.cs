using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class access_database : MonoBehaviour
{
    //private const string MONGO_URI = "mongodb://127.0.0.1:27017";
    private const string MONGO_URI = "mongodb+srv://sarah:lmao@lunar-lion-3oams.mongodb.net/test";   
    private const string DATABASE_NAME = "NASA-Suits";
    //private const string DATABASE_NAME = "NASA_SUITS";
    private MongoClient client;
    private IMongoDatabase db;

    // Start is called before the first frame update
    void Start()
    {
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        //var collection = db.GetCollection<BsonDocument>("Rover_Repair");
        var collection = db.GetCollection<BsonDocument>("Instructions");
        var filter = Builders<BsonDocument>.Filter.Eq("id", 1);
        var found_from_filter = collection.Find(filter).FirstOrDefault();
        Debug.Log(found_from_filter.ToString());
    }
}