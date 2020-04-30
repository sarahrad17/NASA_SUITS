using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class sample : MonoBehaviour
{
    public static IEnumerator s;

    public TextMesh tm;

    //open display, append location & time
    public static void Start_NoteTaking(string file_name)
    {
        System.IO.Directory.CreateDirectory("Sampling");
        file_name = file_name + ".txt";
        System.IO.File.Create(file_name).Close();
        //record current time
        System.IO.File.AppendAllText(file_name, "Current Time: "+Get_Time()+"\n");
        //record current location
        UnityEngine.Vector3 cam_pos = Camera.main.transform.position;
        string cam_pos_string = cam_pos.ToString();
        System.IO.File.AppendAllText(file_name, "Current Location: "+cam_pos_string+"\n");
    }

    //takes pictures for 25 seconds
    public static IEnumerator Take_Scenery_Pics(string file_name)
    {
        if(System.IO.Directory.Exists("Sampling\\" + file_name))
        {
            System.IO.Directory.Delete("Sampling\\" + file_name);
        }
        System.IO.Directory.CreateDirectory("Sampling\\" + file_name);
        int c = 0;
        while (c < 50 )
        {
            ScreenCapture.CaptureScreenshot("Sampling\\" + file_name + "\\" + c.ToString()+".png");
            yield return new WaitForSeconds(.5f);
            c++;
        }
        yield break;
    }
    
    
    //append temperature to display
    public void Get_Temp(string file_name)
    {
        int temp = sort_telemetry.t_sub_value;
        System.IO.File.AppendAllText(file_name, "Temperature: ");
        System.IO.File.AppendAllText(file_name, temp.ToString());
        System.IO.File.AppendAllText(file_name, "\n");
    }

    //get ready to collect sample
    public void Collect_Environment_Display(string file_name, bool taking_vid, TextMesh tm)
    {
        if (taking_vid == true)
        {
            StartCoroutine(Take_Scenery_Pics(file_name));
            taking_vid = false;
        }
        else
        {
            tm.text = "Say \"Collect Video\" and look around \n to record environment!\n";
        }
           
    }

    // returns current UTC time

    public bool Notable_Features(string file_name, TextMesh tm, string f)
    {
        tm.text = "Speak record notable environmental\n features. \nTo stop, \n say \"Stop!\"\n To skip, say \"Skip!\"";
        System.IO.File.AppendAllText(file_name+".txt", "Notable Features:\n");
        
        return Record_Features(file_name, tm, f);
    }


    //while output is false, keep calling this function
    public static bool Record_Features(string file_name, TextMesh tm, string f)
    {
        if (f.Contains("Stop") || f.Contains("stop") || f.Contains("Skip") || f.Contains("Skip") )
        {
            return false;
        }
        else
        {
            if(!(f.Contains("Collect Sample")))
            {
                System.IO.File.AppendAllText(file_name + ".txt", f + "\n");
            }
            
            return true;
        }
    }

    public static string Get_Time()
    {
        return System.DateTime.UtcNow.ToString();  
    }
}
