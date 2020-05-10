using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class sample : MonoBehaviour
{
    private IEnumerator instance;

    public TextMesh tm;



    //open display, append location & time
    public string Start_NoteTaking(string file, GameObject Sample, TextMesh Sample_Text, int sample_session, GameObject Sample_Instructions, TextMesh Sample_Instructions_Text, TextMesh photo_time)
    {
        Sample.SetActive(true);
        Sample_Instructions.SetActive(true);
        print(3);
        //record start time
        string start_time = Get_Time();
        
        System.IO.File.WriteAllText(file, "<b>Start Time:</b>\n"+start_time+"\n");
        //record current location
        UnityEngine.Vector3 cam_pos = Camera.main.transform.position;
        string cam_pos_string = cam_pos.ToString();
        System.IO.File.AppendAllText(file, "<b>Current Location:</b>\n"+cam_pos_string+"\n");
        //record current temperature
        Get_Temp(file);
        //update display pad
        Sample_Text.text = System.IO.File.ReadAllText(file);
        //display something here on instructions about move around to take pictures
        Sample_Instructions_Text.text ="Move around slowly to record \nenvironment.\n \nTime remaining to record:";
        System.IO.File.AppendAllText(file, "<b>Additional Features:</b>\n");
        Sample_Text.text = System.IO.File.ReadAllText(file);
        print("yeet");
        sample s = Sample.AddComponent<sample>();
        IEnumerator coroutine = s.Take_Scenery_Pics(sample_session.ToString(), Sample_Instructions_Text, photo_time);
        s.StartCoroutine(coroutine);
        //StartCoroutine(Take_Scenery_Pics(file, Sample_Instructions_Text, photo_time));
        //instance = Take_Scenery_Pics(file, Sample_Instructions_Text, photo_time);
        //StartCoroutine(instance);
        return start_time;
    }


    public IEnumerator Take_Scenery_Pics(string file_name, TextMesh Sample_Instructions_Text, TextMesh photo_time)
    {
        print("takin pics");
        
        int c = 0;
        photo_time.gameObject.SetActive(true);
        while (c < 50)
        {
            ScreenCapture.CaptureScreenshot("Sampling\\" + file_name + "\\" + c.ToString() + ".png");
            if ((c % 2) == 0)
            {
                if ((25 - (c / 2)) < 10)
                {
                    photo_time.text = "00:0" + (25 - (c / 2));
                }
                else
                {
                    photo_time.text = "00:" + (25 - (c / 2));
                }

            }
            yield return new WaitForSeconds(.5f);
            c++;
        }
        
        photo_time.gameObject.SetActive(false);
        Sample_Instructions_Text.text = "Speak now to record any notable\n features.\n \n When done, say <b>stop</b> to end \nrecording and exit";
        yield break;
    }

    //takes pictures for 25 seconds



    //append temperature to display
    public static void Get_Temp(string file_name)
    {
        int temp = sort_telemetry.t_sub_value;
        System.IO.File.AppendAllText(file_name, "<b>Temperature:</b>\n");
        System.IO.File.AppendAllText(file_name, temp.ToString());
        System.IO.File.AppendAllText(file_name, "\n");
    }


    public static bool Record_Notable_Features(string file_name, TextMesh Sample_Text, string f)
    {
        if (f.Contains("Stop") || f.Contains("Skip"))
        {          
            return false;
        }
        else
        {
            if (!(f.Contains("Collect sample")))
            {
                //print("P: "+System.IO.File.ReadAllText(file_name));
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 30));
                Sample_Text.text = System.IO.File.ReadAllText(file_name);
            }
            return true;
        }
    }

    //while output is false, keep calling this function

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
        System.IO.File.AppendAllText(file_name,"<b>Start Time:</b>" +sample_start_time + "\n");
        Sample_Text.text = System.IO.File.ReadAllText(file_name);
        
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
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f+"\n", 30));
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
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 30));
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
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 30));
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
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 30));
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
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f+"\n", 30));
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
                System.IO.File.AppendAllText(file_name, JsonTest.add_newlines(f + "\n", 30));
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