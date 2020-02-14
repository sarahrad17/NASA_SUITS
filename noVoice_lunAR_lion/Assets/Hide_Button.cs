using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Hide_Button : MonoBehaviour
{
    public Button Notepad_Button;
    public Text Notepad_Text;
    public Image Notepad;

    // Start is called before the first frame update
    void Start()
    {
        Notepad_Text.GetComponent<Text>().enabled = false;
        Notepad.GetComponent<Image>().enabled = false;

        if (!File.Exists(@"yeet.txt"))
        {
            System.IO.File.Create(@"yeet.txt");
            System.IO.File.AppendAllText(@"yeet.txt", "yeeet");
        }
    }
}
