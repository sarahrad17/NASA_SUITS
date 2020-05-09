using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class take_pics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static IEnumerator Take_Scenery_Pics(string file_name, TextMesh Sample_Instructions_Text, TextMesh photo_time)
    {
        print("takin pics");
        int c = 0;
        photo_time.gameObject.SetActive(true);
        while (c < 50)
        {
            ScreenCapture.CaptureScreenshot("Sampling\\" + file_name + "\\" + c.ToString() + ".png");
            if ((c % 2) == 0)
            {
                if((25 - (c/2)) < 10)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
