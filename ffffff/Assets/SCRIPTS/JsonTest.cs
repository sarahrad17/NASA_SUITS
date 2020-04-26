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
        print(model_names[0]);
        print(position_start_vectors[0]);
        //move model
        //do_stuff.Move_Overview(model_names[0], );

        //Move_Overview(GameObject model, float[] pos, float[] rot, float[] sca, float[] pos_move, float[] rot_move, float[] sca_move, GameObject model2, float[] pos2, float[] rot2, float[] sca2, float[] pos_move2, float[] rot_move2, float[] sca_move2, GameObject model3, float[] pos3, float[] rot3, float[] sca3, float[] pos_move3, float[] rot_move3, float[] sca_move3)
   





            /*
            foreach (Instruction.Step step in steps)
            {
                Debug.Log("Step Number:" + step.step);
                Debug.Log("Step Name: " + step.text);
                foreach (Instruction.Asset asset in step.asset_urls)
                {
                    Debug.Log("Asset Model Name: " + asset.model_name);
                    Debug.Log("Asset Position: " + asset.position_start);
                    Debug.Log("Asset Rotation: " + asset.rotation_start);
                    Debug.Log("Asset Scale: " + asset.scale_start);
                }
            }
            */

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