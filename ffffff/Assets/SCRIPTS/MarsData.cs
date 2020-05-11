using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class MarsData : MonoBehaviour
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public bool sop_on { get; set; }

    public bool sspe { get; set; }
    public bool fan_error { get; set; }
    public bool vent_error { get; set; }
    public bool vehicle_power { get; set; }
    public bool h2o_off { get; set; }
    public bool o2_off { get; set; }

    public DateTime create_date { get; set; }
    public int __v { get; set; }


}
