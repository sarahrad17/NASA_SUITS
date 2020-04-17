using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Experimental.Networking;
using System.IO;
using System;
using System.Globalization;

public class retrieve_telemetry : MonoBehaviour
{
    bool get = true;
    string telemetry;
    string telem_print;
    int telem_length;
    int telem_start;

    // Start is called before the first frame update
    void Start()
    {
        System.IO.File.Create(@"telemetry.txt").Close();
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        while (get == true)
        {
            
            yield return new WaitForSeconds(3);
            UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/api/suit");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                System.IO.File.Create(@"telemetry.txt").Close();
                telemetry = www.downloadHandler.text;
                telem_length = telemetry.Length - 2;
                telem_start = telemetry.Length - 290;
                telem_print = telemetry.Substring(telem_start);
                System.IO.File.AppendAllText(@"telemetry.txt", telem_print);
            }
        }
        
    }
}
