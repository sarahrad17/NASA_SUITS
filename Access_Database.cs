using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;



public class Access_Database : MonoBehaviour
{
    private const string MONGO_URI = "mongodb://127.0.0.1:27017";
    private const string DATABASE_NAME = "test-database";
    private MongoClient client;
    private IMongoDatabase db;

    // Start is called before the first frame update
    void Start()
    {
        db_connect();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void db_connect()
    {
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        var collection = db.GetCollection<BsonDocument>("numbers");
        //var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
        //print(firstDocument.ToString());
        var filter = Builders<BsonDocument>.Filter.Eq("num",2);
        var found_from_filter = collection.Find(filter).FirstOrDefault();
        print(found_from_filter.ToString());

    }   
}