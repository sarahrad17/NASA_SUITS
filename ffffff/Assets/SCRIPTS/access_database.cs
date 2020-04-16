using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class access_database : MonoBehaviour
{
    //private const string MONGO_URI = "mongodb://127.0.0.1:27017";
    //private const string DATABASE_NAME = "NASA_SUITS";
    private const string MONGO_URI = "mongodb+srv://sarah:lmao@lunar-lion-3oams.mongodb.net/test";   
    private const string DATABASE_NAME = "NASA-Suits";
    private MongoClient client;
    private IMongoDatabase db;
    public static string[] instruction_array;
    public static string[] instruct_text_array;
    public static string[] instruct_asset_array;
    public static string[] instruct_num_array;


    // Start is called before the first frame update
    void Start()
    {
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
        var collection = db.GetCollection<BsonDocument>("Instructions");
        var filter = Builders<BsonDocument>.Filter.Eq("id", 1);
        var found_from_filter = collection.Find(filter).FirstOrDefault();
        string thing = found_from_filter.ToString();
        int instruction_start = thing.IndexOf("\"instructions\"");
        string instruction = thing.Substring(instruction_start+15);
        int inst_start = thing.IndexOf("{");
        string inst = instruction.Substring(inst_start+3);

        print(inst);
        //find number of instructions
        int count = 0;
        int curr = 0;
        while (curr < inst.Length)
        {
            if (inst[curr] == '{')
            {
                count++;
            }
            curr++;
        }

        //make an array of the instructions
        instruction_array = new string[count];
        int i = 0;
        string instruct;
        while (i < count){
            int instruct_start = inst.IndexOf("{");
            int instruct_end = inst.IndexOf("}");
            instruct = inst.Substring(instruct_start, instruct_end);
            //print(instruct);
            instruction_array[i] = instruct;
            inst = inst.Substring(instruct_end + 1);
            i++;
        }
        //print(string.Join("\n", instruction_array));

        instruct_text_array = new string[count];
        //text array
        for (int j=0; j<count; j++)
        {
            inst = instruction_array[j];
            int instruct_text_start = inst.IndexOf("\"text\"");
            int instruct_text_end = inst.IndexOf("\",");
            string instruct_text = inst.Substring(instruct_text_start+10, instruct_text_end-12);
            instruct_text_array[j] = instruct_text;
            inst = inst.Substring(instruct_text_end + 1);
        }
        //print(string.Join("\n", instruct_text_array));


        instruct_asset_array = new string[count];
        //text array
        for (int k = 0; k < count; k++)
        {
            inst = instruction_array[k];
            int instruct_asset_start = inst.IndexOf("\"asset_urls\"");
            string instruct_asset = inst.Substring(instruct_asset_start + 16);
            int instruct_asset_end = instruct_asset.IndexOf("]");
            instruct_asset = instruct_asset.Substring(0, instruct_asset_end);
            //print(instruct_asset);
            instruct_asset_array[k] = instruct_asset;
            inst = inst.Substring(instruct_asset_end + 1);
        }
        //print(string.Join("\n", instruct_asset_array));

        //make an array of the instruction numbers
        instruct_num_array = new string[count];
        //text array
        for (int m = 0; m < count; m++)
        {
            inst = instruction_array[m];
            int instruct_num_start = inst.IndexOf("\"step\"");
            string instruct_num = inst.Substring(instruct_num_start + 8);
            int instruct_num_end = instruct_num.IndexOf("}");
            if (instruct_num_end == -1)
            {
                instruct_num = "1";
            }
            else
            {
                instruct_num = instruct_num.Substring(0,instruct_num_end);
            }
            instruct_num_array[m] = instruct_num;
            inst = inst.Substring(instruct_num_end + 1);
        }
    }
}