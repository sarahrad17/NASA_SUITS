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
    public GameObject g;
    

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

        double[,] position_start_vectors = new double[3, 3];
        double[,] rotation_start_vectors = new double[3, 3];
        double[,] scale_start_vectors = new double[3, 3];
        double[,] position_end_vectors = new double[3, 3];
        double[,] rotation_end_vectors = new double[3, 3];
        double[,] scale_end_vectors = new double[3, 3];

        for(int j = 0; j<model_names.Length; j++)
        {
            model_names[j] = null;
        }

        //for each asset
        int i = 0;
        foreach (Instruction.Asset asset in current_step.asset_urls)
        {
            print(asset.model_name);
            //associate asset name to model in asset bundle
            GameObject curr_asset = GameObject.Find("/Model_Path/Models/" + asset.model_name);
            model_names[i] = curr_asset;
            curr_asset.SetActive(true);
            //get asset details
            position_start_vectors[i, 0] = asset.position_start[0];
            position_start_vectors[i, 1] = asset.position_start[1];
            position_start_vectors[i, 2] = asset.position_start[2];

            rotation_start_vectors[i, 0] = asset.rotation_start[0];
            rotation_start_vectors[i, 1] = asset.rotation_start[1];
            rotation_start_vectors[i, 2] = asset.rotation_start[2];

            scale_start_vectors[i, 0] = asset.scale_start[0];
            scale_start_vectors[i, 1] = asset.scale_start[1];
            scale_start_vectors[i, 2] = asset.scale_start[2];

            position_end_vectors[i, 0] = asset.position_end[0];
            position_end_vectors[i, 1] = asset.position_end[1];
            position_end_vectors[i, 2] = asset.position_end[2];

            rotation_end_vectors[i, 0] = asset.rotation_end[0];
            rotation_end_vectors[i, 1] = asset.rotation_end[1];
            rotation_end_vectors[i, 2] = asset.rotation_end[2];

            scale_end_vectors[i, 0] = asset.scale_end[0];
            scale_end_vectors[i, 1] = asset.scale_end[1];
            scale_end_vectors[i, 2] = asset.scale_end[2];
            i = i + 1;
        }

        //yes i am fully aware that there is a better way to do this i just got caught up debugging in this mess and 
        //it works so im not changing it thank u have a nice day

        float[] pos_start0 = { (float)position_start_vectors[0, 0], (float)position_start_vectors[0, 1], (float)position_start_vectors[0, 2] };
        float[] pos_start1 = { (float)position_start_vectors[1, 0], (float)position_start_vectors[1, 1], (float)position_start_vectors[1, 2] };
        float[] pos_start2 = { (float)position_start_vectors[2, 0], (float)position_start_vectors[2, 1], (float)position_start_vectors[2, 2] };

        float[] rot_start0 = { (float)rotation_start_vectors[0, 0], (float)rotation_start_vectors[0, 1], (float)rotation_start_vectors[0, 2] };
        float[] rot_start1 = { (float)rotation_start_vectors[1, 0], (float)rotation_start_vectors[1, 1], (float)rotation_start_vectors[1, 2] };
        float[] rot_start2 = { (float)rotation_start_vectors[2, 0], (float)rotation_start_vectors[2, 1], (float)rotation_start_vectors[2, 2] };

        float[] sca_start0 = { (float)scale_start_vectors[0, 0], (float)scale_start_vectors[0, 1], (float)scale_start_vectors[0, 2] };
        float[] sca_start1 = { (float)scale_start_vectors[1, 0], (float)scale_start_vectors[1, 1], (float)scale_start_vectors[1, 2] };
        float[] sca_start2 = { (float)scale_start_vectors[2, 0], (float)scale_start_vectors[2, 1], (float)scale_start_vectors[2, 2] };

        float[] pos_end0 = { (float)position_end_vectors[0, 0], (float)position_end_vectors[0, 1], (float)position_end_vectors[0, 2] };
        float[] pos_end1 = { (float)position_end_vectors[1, 0], (float)position_end_vectors[1, 1], (float)position_end_vectors[1, 2] };
        float[] pos_end2 = { (float)position_end_vectors[2, 0], (float)position_end_vectors[2, 1], (float)position_end_vectors[2, 2] };

        float[] rot_end0 = { (float)rotation_end_vectors[0, 0], (float)rotation_end_vectors[0, 1], (float)rotation_end_vectors[0, 2] };
        float[] rot_end1 = { (float)rotation_end_vectors[1, 0], (float)rotation_end_vectors[1, 1], (float)rotation_end_vectors[1, 2] };
        float[] rot_end2 = { (float)rotation_end_vectors[2, 0], (float)rotation_end_vectors[2, 1], (float)rotation_end_vectors[2, 2] };

        float[] sca_end0 = { (float)scale_end_vectors[0, 0], (float)scale_end_vectors[0, 1], (float)scale_end_vectors[0, 2] };
        float[] sca_end1 = { (float)scale_end_vectors[1, 0], (float)scale_end_vectors[1, 1], (float)scale_end_vectors[1, 2] };
        float[] sca_end2 = { (float)scale_end_vectors[2, 0], (float)scale_end_vectors[2, 1], (float)scale_end_vectors[2, 2] };


        //move model
        do_stuff d = new do_stuff();
        do_stuff.Move_Overview(model_names[0], pos_start0, rot_start0, sca_start0, pos_end0, rot_end0, sca_end0, model_names[1], pos_start1, rot_start1, sca_start1, pos_end1, rot_end1, sca_end1, model_names[2], pos_start2, rot_start2, sca_start2, pos_end2, rot_end2, sca_end2);

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