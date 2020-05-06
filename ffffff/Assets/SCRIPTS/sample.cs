using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class sample : MonoBehaviour
{
    public static IEnumerator s;

    public TextMesh tm;

    //open display, append location & time
    public static string Start_NoteTaking(string file_name, GameObject Sample, TextMesh Sample_Text, int sample_session, TextMesh Sampling_Instructions_Text, TextMesh photo_time)
    {
        Sample.SetActive(true);
        //create sampling directory & text file
        System.IO.Directory.CreateDirectory("Sampling");
        System.IO.Directory.CreateDirectory("Sampling\\"+sample_session.ToString());
        file_name = file_name + ".txt";
        System.IO.File.Create("Sampling\\" +sample_session.ToString() + "\\" + file_name).Close();
        //record start time
        string start_time = Get_Time();
        System.IO.File.AppendAllText(file_name, "Start Time: "+start_time+"\n");
        //record current location
        UnityEngine.Vector3 cam_pos = Camera.main.transform.position;
        string cam_pos_string = cam_pos.ToString();
        System.IO.File.AppendAllText(file_name, "Current Location: "+cam_pos_string+"\n");
        //record current temperature
        Get_Temp(file_name);
        //update display pad
        Sample_Text.text = JsonTest.add_newlines(System.IO.File.ReadAllText(file_name), 40);
        //display something here on instructions about move around to take pictures
        Sampling_Instructions_Text.text =JsonTest.add_newlines("Move around slowly to record environment", 30);
        //take pictures of environment & display time remaining
        Take_Scenery_Pics(file_name, photo_time);
        return start_time;
    }

    //takes pictures for 25 seconds
    public static IEnumerator Take_Scenery_Pics(string file_name, TextMesh photo_time)
    {
        if(System.IO.Directory.Exists("Sampling\\" + file_name))
        {
            System.IO.Directory.Delete("Sampling\\" + file_name);
        }
        System.IO.Directory.CreateDirectory("Sampling\\" + file_name);
        int c = 0;
        photo_time.gameObject.SetActive(true);
        while (c < 50 )
        {
            ScreenCapture.CaptureScreenshot("Sampling\\" + file_name + "\\" + c.ToString()+".png");
            if((c % 2) == 0)
            {
                photo_time.text = "00:" + (c % 2);
            }
            yield return new WaitForSeconds(.5f);
            c++;
        }
        photo_time.gameObject.SetActive(false);
        yield break;
    }
    
    
    //append temperature to display
    public static void Get_Temp(string file_name)
    {
        int temp = sort_telemetry.t_sub_value;
        System.IO.File.AppendAllText(file_name, "Temperature: ");
        System.IO.File.AppendAllText(file_name, temp.ToString());
        System.IO.File.AppendAllText(file_name, "\n");
    }


    public bool Notable_Features(string file_name, TextMesh tm, string f, string start_time)
    {
        tm.text = JsonTest.add_newlines("Speak to record notable environmental features. To stop recording, say \"Stop!\" To skip, say \"Skip!\"", 30);
        System.IO.File.AppendAllText(file_name+".txt", "Notable Features:\n");       
        return Record_Features(file_name, tm, f, start_time);
    }

    //while output is false, keep calling this function
    public static bool Record_Features(string file_name, TextMesh tm, string f, string start_time)
    {
        if (f.Contains("Stop") || f.Contains("Skip") )
        {
            string end_time = Get_Time();
            System.IO.File.AppendAllText(file_name, "End Time: " + end_time + "\n");
            
            return false;
        }
        else
        {
            if(!(f.Contains("Set up Sample")))
            {
                System.IO.File.AppendAllText(file_name + ".txt", f + "\n");
            }          
            return true;
        }
    }

    public static string Get_Time()
    {
        //for example: 5/6/2005 09:34:42 PM
        return System.DateTime.UtcNow.ToString();  
    }

    public static string Take_Sample(GameObject Sample, TextMesh Sample_Text, int sample_session, int sample_num)
    {
        Sample.SetActive(true);
        System.IO.Directory.CreateDirectory("Sampling\\"+sample_session.ToString());
        System.IO.Directory.CreateDirectory("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString());
        string file_name = "Sampling\\"+sample_session.ToString()+"\\"+sample_num.ToString()+"\\info.txt";
        System.IO.File.Create(file_name).Close();
        string sample_start_time = Get_Time();
        System.IO.File.AppendAllText(file_name,"Start Time: "+sample_start_time+"\n");
        Sample_Text.text = JsonTest.add_newlines(System.IO.File.ReadAllText(file_name),40);
        
        return sample_start_time;
    }

    public static bool Record_Sample_Size(string file_name, TextMesh Sample_Text, string f)
    {
        if (f.Contains("Next") || f.Contains("Skip"))
        {
            return false;
        }
        else
        {
            if (!(f.Contains("Collect sample")))
            {
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f+"\n", 40));
                Sample_Text.text = System.IO.File.ReadAllText(file_name);
            }
            return true;
        }
    }

    public static bool Record_Sample_Color(string file_name, TextMesh tm, string f)
    {
        if (f.Contains("Next") || f.Contains("Skip"))
        {
            return false;
        }
        else
        {
            if (!(f.Contains("Collect sample")))
            {
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 40));
            }
            return true;
        }
    }

    public static bool Record_Sample_Texture(string file_name, TextMesh tm, string f)
    {
        if (f.Contains("Next") || f.Contains("Skip"))
        {
            return false;
        }
        else
        {
            if (!(f.Contains("Collect sample")))
            {
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 40));
            }
            return true;
        }
    }

    public static bool Record_Major_Components(string file_name, TextMesh tm, string f)
    {
        if (f.Contains("Next") || f.Contains("Skip"))
        {
            return false;
        }
        else
        {
            if (!(f.Contains("Collect sample")))
            {
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 40));
            }
            return true;
        }
    }

    public static bool Record_Other_Features(string file_name, TextMesh tm, string f)
    {

        if (f.Contains("Next") || f.Contains("Skip"))
        {
            return false;
        }
        else
        {
            if (!(f.Contains("Collect sample")))
            {
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f+"\n", 40));
            }
            return true;
        }
    }

    public static bool Record_Other_Notes(string file_name, TextMesh tm, string f)
    {
        if (f.Contains("Next") || f.Contains("Skip"))
        {
            return false;
        }
        else
        {
            if (!(f.Contains("Collect sample")))
            {
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 40));
            }
            return true;
        }
    }

    public static bool Ready_To_Close(string file_name, TextMesh tm, string f)
    {
        if (f.Contains("Stop") || f.Contains("Close"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool Exit_Sample(string f)
    {
        if (f.Contains("Stop") || f.Contains("Close") || f.Contains("Exit"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Continue_Sample(string f)
    {
        if (f.Contains("Continue") )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}