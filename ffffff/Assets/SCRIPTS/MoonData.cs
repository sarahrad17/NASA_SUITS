using System;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MoonData : MonoBehaviour
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public string heart_bpm { get; set; }

    public string p_suit { get; set; }

    public string p_sub { get; set; }

    public string t_sub { get; set; }
    public string v_fan { get; set; }
    public string p_o2 { get; set; }
    public string rate_o2 { get; set; }
    public string cap_battery { get; set; }
    public string p_h2o_g { get; set; }
    public string p_h2o_l { get; set; }
    public string p_sop { get; set; }
    public string rate_sop { get; set; }
    public string t_battery { get; set; }
    public string t_oxygen { get; set; }
    public string t_water { get; set; }
    public DateTime create_date { get; set; }
    public int __v { get; set; }


    public MoonData()
    {


    }


}
