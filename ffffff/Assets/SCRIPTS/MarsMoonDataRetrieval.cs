using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

public class MarsMoonDataRetrieval : MonoBehaviour
{
    private const string MONGO_URI = "mongodb://127.0.0.1:27017"; //SET PROPER URI 
    private const string DATABASE_NAME = "spacesuit";
    private const string MOON_COLLECTION_NAME = "moons";
    private const string MARS_COLLECTION_NAME = "mars";

    private MongoClient client;
    private IMongoDatabase db;

    private int current = 200;

    //TELEMETRY
    public static int heart_bpm_value;
    public static float p_suit_value;
    public static float p_sub_value;
    public static int t_sub_value;
    public static int v_fan_value;
    public static int p_o2_value;
    public static float rate_o2_value;
    public static int cap_battery_value;
    public static int p_h2o_g_value;
    public static int p_h2o_l_value;
    public static int p_sop_value;
    public static float rate_sop_value;
    public static string t_battery_value;
    public static string t_oxygen_value;
    public static string t_water_value;
    //mars
    public static bool sop_on_value;
    public static bool sspe_value;
    public static bool fan_error_value;
    public static bool vent_error_value;
    public static bool vehicle_power_value;
    public static bool h2o_off_value;
    public static bool o2_off_value;


    // Start is called before the first frame update
    void Start()
    {
        client = new MongoClient(MONGO_URI);
        db = client.GetDatabase(DATABASE_NAME);
    }


    // Update is called once per frame
    void Update()
    {
        if(current == 200)
        {

            //MOON TELEMETRY
            MoonData dataMoon = getLatestMoonTelemtry();
            //check id num
            Debug.Log("_id" + dataMoon._id);
            heart_bpm_value = Int32.Parse(dataMoon.heart_bpm);
            p_suit_value = float.Parse(dataMoon.p_suit);
            p_sub_value = float.Parse(dataMoon.p_sub);
            t_sub_value = Int32.Parse(dataMoon.t_sub);
            v_fan_value = Int32.Parse(dataMoon.v_fan);
            p_o2_value = Int32.Parse(dataMoon.p_o2);
            rate_o2_value = float.Parse(dataMoon.rate_o2);
            cap_battery_value = Int32.Parse(dataMoon.cap_battery);
            p_h2o_g_value = Int32.Parse(dataMoon.p_h2o_g);
            p_h2o_l_value = Int32.Parse(dataMoon.p_h2o_l);
            p_sop_value = Int32.Parse(dataMoon.p_sop);
            rate_sop_value = float.Parse(dataMoon.rate_sop);
            t_battery_value = dataMoon.t_battery;
            t_oxygen_value = dataMoon.t_oxygen;
            t_water_value = dataMoon.t_water;

            //MARS TELEMETRY
            MarsData dataMars = getLatestMarsTelemtry();
            sop_on_value = dataMars.sop_on;
            sspe_value = dataMars.sspe;
            fan_error_value = dataMars.fan_error;
            vent_error_value = dataMars.vent_error;
            vehicle_power_value = dataMars.vehicle_power;
            h2o_off_value = dataMars.h2o_off;
            o2_off_value = dataMars.o2_off;

            current = 0;
        }
        else
        {
            current = current + 1;
        }
        
        
    }

    //NOTE THESE METHODS ARE NOT OPTIMIZED FOR PERFORMANCE - PROBABLY WILL NEED TO DO THAT LATER LOL OTHERWISE RIP 

    /// <summary>
    /// Gets the latest Mars telemtry data. 
    /// </summary>
    /// <returns></returns>
    MarsData getLatestMarsTelemtry()
    {
        IMongoCollection<MarsData> collection = db.GetCollection<MarsData>(MARS_COLLECTION_NAME);
        MarsData objectToFind = collection.Find(d => true).ToList().LastOrDefault();
        return objectToFind;
    }

    /// <summary>
    /// Gets the latest Mooon telemtry data. 
    /// </summary>
    /// <returns></returns>
    MoonData getLatestMoonTelemtry()
    {
        IMongoCollection<MoonData> collection = db.GetCollection<MoonData>(MOON_COLLECTION_NAME);
        //lol so for some reason .Last isn't supported so we're doing it a jank way 
        //MoonData objectToFind = collection.Find(d => true).Limit(1).FirstOrDefault();

        MoonData objectToFind = collection.Find(d => true).ToList().LastOrDefault();

        //MoonData objectToFind = collection.Find(d => true).SortByDescending()
        //MoonData objectToFind = collection.Find(d => DateTime.Compare(d.create_date, date) > 0 ).FirstOrDefault(); //should in theory return the latest one 
        return objectToFind;
    }

}
