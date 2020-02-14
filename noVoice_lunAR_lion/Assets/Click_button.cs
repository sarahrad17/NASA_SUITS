using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Click_button : MonoBehaviour
{
    public Button NoteTaking_Button;
    public Text Notepad_Text;
    public string t;
    public string texty;
    public Button Notepad_Button;
    public Image Notepad;
    public bool Notepad_Boolean;
    void Start()
    {
        Button btn = NoteTaking_Button.GetComponent<Button>();
        Notepad_Button.GetComponent<Button>().enabled = false;
        Notepad.GetComponent<Image>().enabled = false;
        Notepad_Boolean = Notepad.GetComponent<Image>().enabled;
        btn.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        
    }
    void TaskOnClick()
    {
        System.IO.File.AppendAllText(@"log.txt", "\nNotepad Button Clicked\n");
        Notepad_Boolean = !Notepad_Boolean;
        Notepad.GetComponent<Image>().enabled = Notepad_Boolean;
        Notepad_Text.GetComponent<Text>().enabled = Notepad_Boolean;
        Notepad_Button.GetComponent<Button>().enabled = Notepad_Boolean;
        
    }
    public string Read(string path)
    {
        texty = File.ReadAllText(path);
        return texty;
    }
 
}