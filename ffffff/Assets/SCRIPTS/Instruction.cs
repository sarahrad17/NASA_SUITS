using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
public class Instruction : MonoBehaviour
{
	//all variables must be PUBLIC
	//do not put System.Serializable 

	public string _id { get; set; }
	public string instruction_name { get; set; }
	public List<Step> instructions { get; set; }

	public Instruction()
	{
	}


	public class Step
	{
		public string text { get; set; }
		public List<Asset> asset_urls { get; set; }
		public int step { get; set; }
		public Step()
		{

		}

	}

	public class Asset
	{
		public string model_name { get; set; }
		//Tuple<int, int, int> position { get; set; }
		//Tuple<int, int, int> rotation { get; set; }
		//Tuple<int, int, int> scale { get; set; }
		public List<int> position_start { get; set; }
		public List<int> rotation_start { get; set; }
		public List<int> scale_start { get; set; }
		public List<int> position_end { get; set; }
		public List<int> rotation_end { get; set; }
		public List<int> scale_end { get; set; }

		public Asset()
		{

		}
	}
}