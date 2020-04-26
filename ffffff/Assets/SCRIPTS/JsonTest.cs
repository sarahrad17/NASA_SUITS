using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

public class JsonTest : MonoBehaviour
{
    private const string MONGO_URI = "mongodb+srv://admin:lmao@lunar-lion-3oams.mongodb.net/test?retryWrites=true&w=majority"; //SET PROPER URI 
    private const string DATABASE_NAME = "NASA-Suits";


    private MongoClient client;
    private IMongoDatabase db;

    // Start is called before the first frame update
    void Start()
    {
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);

        string instruction_id = "1";
        Instruction instructionQuery = queryDatabase("yeet", instruction_id); //the database must be changed to type "yeet"


        //Debug Code demonstration to demonstrate functionality 
        Debug.Log("Name: " + instructionQuery.instruction_name);
        Debug.Log("Id: " + instructionQuery._id);
        List<Instruction.Step> steps = instructionQuery.instructions;
        foreach (Instruction.Step step in steps)
        {
            Debug.Log("Step Number:" + step.step);
            Debug.Log("Step Name: " + step.text);
            foreach (Instruction.Asset asset in step.asset_urls)
            {
                Debug.Log("Asset Model Name: " + asset.model_name);
                Debug.Log("Asset Position: " + asset.position_start[1]);
                Debug.Log("Asset Rotation: " + asset.rotation_start[1]);
                Debug.Log("Asset Scale: " + asset.scale_start[1]);
            }
        }

    }

    /// <summary>
    /// Returns an Instruction object from the MongoDB database
    /// </summary>
    /// <param name="collectionName"></param>
    /// <param name="document_id"></param>
    /// <returns></returns>
    Instruction queryDatabase(string collectionName, string document_id)
    {
        IMongoCollection<Instruction> collection = db.GetCollection<Instruction>(collectionName);
        //List<Instruction> allInstructionSets = collection.Find(d => true).ToList();
        //Debug.Log(allInstructionSets.Count);
        Instruction objectToFind = collection.Find(d => d._id.Equals(document_id)).FirstOrDefault();
        return objectToFind;


    }
}