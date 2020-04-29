using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class sample : MonoBehaviour
{

    public TextMesh tm;
    // Start is called before the first frame update
    void Start()
    {
        Start_NoteTaking("4_29_2020");
    }


    public void Start_NoteTaking(string file_name)
    {
        file_name = file_name + ".txt";
        System.IO.File.Create(file_name).Close();
        //record current time
        System.IO.File.AppendAllText(file_name, "Current Time: "+Get_Time()+"\n");
        //record current location
        UnityEngine.Vector3 cam_pos = Camera.main.transform.position;
        string cam_pos_string = cam_pos.ToString();
        System.IO.File.AppendAllText(file_name, "Current Location: "+cam_pos_string+"\n");
    }

    public void Take_Scenery_Pics(string file_name)
    {
        System.IO.Directory.CreateDirectory(file_name);
        for (int curr=0; curr < 100; curr++)
        {
            ScreenCapture.CaptureScreenshot(file_name + curr.ToString());
            System.Threading.Thread.Sleep(100);
        }    
    }

    public void Get_Temp(string file_name)
    {
        int temp = sort_telemetry.t_sub_value;
        System.IO.File.AppendAllText(file_name, "Temperature: ");
        System.IO.File.AppendAllText(file_name, temp.ToString());
        System.IO.File.AppendAllText(file_name, "\n");
    }

    public void Collect_Environment_Display(string file_name, bool taking_vid, TextMesh tm)
    {
        if (taking_vid == true)
        {
            Take_Scenery_Pics(file_name);
            taking_vid = false;
        }
        else
        {
            tm.text = "Say \"Collect Video\" and look around \n to record environment!\n";
        }
           
    }

    // returns current UTC time

    public void Notable_Features(string file_name, TextMesh tm)
    {
        tm.text = "Speak record notable environmental\n features. \nTo stop, \n say \"Stop!\"\n To skip, say \"Skip!\"";
        System.IO.File.AppendAllText(file_name, "Notable Features:\n");

    }


    //while output is false, keep calling this function
    public bool Record_Features(string file_name, TextMesh tm, string sample_status, string f)
    {
        if(sample_status == "Stop" || sample_status == "Skip")
        {
            return true;
        }
        else
        {
            System.IO.File.AppendAllText(file_name, f+"\n");
            return false;
        }
    }

    public static string Get_Time()
    {
        return System.DateTime.UtcNow.ToString();  
    }
}
