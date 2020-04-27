using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class JsonTest : MonoBehaviour
{
    private const string MONGO_URI = "mongodb+srv://admin:lmao@lunar-lion-3oams.mongodb.net/test?retryWrites=true&w=majority"; //SET PROPER URI 
    private const string DATABASE_NAME = "NASA-Suits";


    private MongoClient client;
    private IMongoDatabase db;
    public static JsonTest instance;
    

    public void Yeet(int step_num, TextMesh inst)
    {
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);

        int instruction_id = 1;
        Instruction instructionQuery = queryDatabase("yeet", instruction_id);
        List<Instruction.Step> steps = instructionQuery.instructions;

        Instruction.Step current_step = steps.ElementAt(step_num);
        //get instruction text
        string current_text = add_newlines(current_step.text);
        //display instruction text
        inst.text = current_text;
        
        //create empty array
        GameObject[] model_names = new GameObject[3];

        Vector3[] position_start_vectors = new Vector3[3];
        Vector3[] rotation_start_vectors = new Vector3[3];
        Vector3[] scale_start_vectors = new Vector3[3];
        Vector3[] position_end_vectors = new Vector3[3];
        Vector3[] rotation_end_vectors = new Vector3[3];
        Vector3[] scale_end_vectors = new Vector3[3];

        for(int j = 0; j<model_names.Length; j++)
        {
            model_names[j] = null;
        }

        //for each asset
        int i = 0;
        foreach (Instruction.Asset asset in current_step.asset_urls)
        {
            //associate asset name to model in asset bundle
            GameObject curr_asset = GameObject.Find("/Model_Path/Models/"+asset.model_name);
            model_names[i] = curr_asset;
            //get asset details
            
            position_start_vectors[i] = new Vector3(asset.position_start[0], asset.position_start[1], asset.position_start[2]);
            rotation_start_vectors[i] = new Vector3(asset.rotation_start[0], asset.rotation_start[1], asset.rotation_start[2]);
            scale_start_vectors[i] = new Vector3(asset.scale_start[0], asset.scale_start[1], asset.scale_start[2]);
            position_end_vectors[i] = new Vector3(asset.position_end[0], asset.position_end[1], asset.position_end[2]);
            rotation_end_vectors[i] = new Vector3(asset.rotation_end[0], asset.rotation_end[1], asset.rotation_end[2]);
            scale_end_vectors[i] = new Vector3(asset.scale_end[0], asset.scale_end[1], asset.scale_end[2]);
        }

        float[] position_start = new[] {position_start_vectors[0].x, position_start_vectors[0].y, position_start_vectors[0].z};
        float[] position_start_2 = new[] { position_start_vectors[1].x, position_start_vectors[1].y, position_start_vectors[1].z };
        float[] position_start_3 = new[] { position_start_vectors[2].x, position_start_vectors[2].y, position_start_vectors[2].z };

        float[] rotation_start = new[] { rotation_start_vectors[0].x, rotation_start_vectors[0].y, rotation_start_vectors[0].z };
        float[] rotation_start_2 = new[] { rotation_start_vectors[1].x, rotation_start_vectors[1].y, rotation_start_vectors[1].z };
        float[] rotation_start_3 = new[] { rotation_start_vectors[2].x, rotation_start_vectors[2].y, rotation_start_vectors[2].z };

        float[] scale_start = new[] { scale_start_vectors[0].x, scale_start_vectors[0].y, scale_start_vectors[0].z };
        float[] scale_start_2 = new[] { scale_start_vectors[1].x, scale_start_vectors[1].y, scale_start_vectors[1].z };
        float[] scale_start_3 = new[] { scale_start_vectors[2].x, scale_start_vectors[2].y, scale_start_vectors[2].z };



        float[] position_end = new[] { position_end_vectors[0].x, position_end_vectors[0].y, position_end_vectors[0].z };
        float[] position_end_2 = new[] { position_end_vectors[1].x, position_end_vectors[1].y, position_end_vectors[1].z };
        float[] position_end_3 = new[] { position_end_vectors[2].x, position_end_vectors[2].y, position_end_vectors[2].z };

        float[] rotation_end = new[] { rotation_end_vectors[0].x, rotation_end_vectors[0].y, rotation_end_vectors[0].z };
        float[] rotation_end_2 = new[] { rotation_end_vectors[1].x, rotation_end_vectors[1].y, rotation_end_vectors[1].z };
        float[] rotation_end_3 = new[] { rotation_end_vectors[2].x, rotation_end_vectors[2].y, rotation_end_vectors[2].z };

        float[] scale_end = new[] { scale_end_vectors[0].x, scale_end_vectors[0].y, scale_end_vectors[0].z };
        float[] scale_end_2 = new[] { scale_end_vectors[1].x, scale_end_vectors[1].y, scale_end_vectors[1].z };
        float[] scale_end_3 = new[] { scale_end_vectors[2].x, scale_end_vectors[2].y, scale_end_vectors[2].z };

        //move model
        do_stuff d = new do_stuff();
        d.Move_Overview(model_names[0], position_start, rotation_start, scale_start, position_end, rotation_end, scale_end, model_names[1], position_start_2, rotation_start_2, scale_start_2, position_end_2, rotation_end_2, scale_end_2, model_names[2], position_start_3, rotation_start_3, scale_start_3, position_end_3, rotation_end_3, scale_end_3);
        //print("here");
    }

    /// <summary>
    /// Returns an Instruction object from the MongoDB database
    /// </summary>
    /// <param name="collectionName"></param>
    /// <param name="document_id"></param>
    /// <returns></returns>
    Instruction queryDatabase(string collectionName, int document_id)
    {
        IMongoCollection<Instruction> collection = db.GetCollection<Instruction>(collectionName);
        //List<Instruction> allInstructionSets = collection.Find(d => true).ToList();
        //Debug.Log(allInstructionSets.Count);
        Instruction objectToFind = collection.Find(d => d._id.Equals(document_id)).FirstOrDefault();
        return objectToFind;
    }

    public static string add_newlines(string full_string)
    {
        //if less than 26 chars
        if (full_string.Length <= 26)
        {
            return full_string;
        }
        //substring if more than 26 remain
        else
        {
            //substring first 26
            string sub_26 = full_string.Substring(0, 26);
            //find last space
            int last_space = sub_26.LastIndexOf(" ");
            //end line at last space
            string first_line = full_string.Substring(0, last_space);
            //length of new first line 
            int curr_length = first_line.Length;
            //add space
            full_string = first_line + "\n" + full_string.Substring(curr_length);
            //add \n 
            curr_length = curr_length + 1;
            //while remaining chars is longer than 26
            while ((full_string.Length - curr_length) > 26)
            {
                //new substring of next 26 chars
                sub_26 = full_string.Substring(curr_length, 26);
                //find last space
                last_space = sub_26.LastIndexOf(" ");
                //add \n at end of last space
                full_string = full_string.Substring(0, curr_length + last_space) + "\n" + full_string.Substring(curr_length + last_space);
                //increment curr_length
                curr_length = curr_length + last_space + 1;
            }
            return full_string;
        }

    }
}