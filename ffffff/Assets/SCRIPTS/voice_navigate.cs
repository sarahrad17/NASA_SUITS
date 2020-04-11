﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Microsoft.CognitiveServices.Speech;
using System.Text.RegularExpressions;
public class voice_navigate : MonoBehaviour
{

    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    public int yeet = 3;
    public int counter = 0;
    private bool micPermissionGranted = false;

    //open/close menu
    public bool menu_open;
    public GameObject Menu_Buttons;
    //open/close notes
    public bool notepad_open;
    public GameObject Notepad;
    public int notepad_matches;
    //take notes
    public bool taking_notes;
    public TextMesh Notes_Text;
    public int notes_matches;
    public bool not_first;

    // Start is called before the first frame update
    void Start()
    {
        //Continue with normal initialization, Text and Button objects are present.
        micPermissionGranted = true;
        message = " ";
        //menu
        menu_open = false;
        Menu_Buttons.SetActive(false);
        //notepad
        notepad_open = false;
        Notepad.SetActive(false);
        //take notes
        taking_notes = false;


        System.IO.File.Create(@"speech_output.txt").Close();
        System.IO.File.Create(@"speech_finaloutput.txt").Close();
        SpeechContinuousRecognitionAsync();
    }

    public async void SpeechContinuousRecognitionAsync()
    {
        // Creates an instance of a speech config with specified subscription key and service region.
        var config = SpeechConfig.FromSubscription("11320a35e5eb4440a0a582af2e169169", "eastus");
        
        // Creates a speech recognizer from microphone.
        using (var recognizer = new SpeechRecognizer(config))
        {
            // Subscribes to events.
            recognizer.Recognizing += (s, e) => {
                var result = e.Result;
                message = result.Reason.ToString();
            };

            recognizer.Recognized += (s, e) => {
                var result = e.Result;
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    message = result.Text;
                    System.IO.File.AppendAllText(@"speech_output.txt", message);
                    System.IO.File.AppendAllText(@"speech_output.txt", "\n");
                }
            };

            recognizer.Canceled += (s, e) => {
                message = $"\n    Recognition Canceled. Reason: {e.Reason.ToString()}, CanceledReason: {e.Reason}";
            };
            recognizer.SessionStarted += (s, e) => {
                message = "\n    Session started event.";
            };

            recognizer.SessionStopped += (s, e) => {
                message = "\n    Session stopped event.";
            };

            // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
            await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

            do
            {
                message = "";
            } while (yeet == 3);
            // Stops recognition.
            await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if not 15th frame
        if (counter < 150)
        {
            counter = counter + 1;
        }
        //if 15th frame
        else
        {
            //see if input file exists yet
            if (File.Exists(@"speech_output.txt"))
            {
                //read input into array of lines
                string[] file_contents = System.IO.File.ReadAllLines(@"speech_output.txt");

                foreach (string f in file_contents)
                {
                    System.IO.File.AppendAllText(@"speech_finaloutput.txt", f);
                    System.IO.File.AppendAllText(@"speech_finaloutput.txt", "\n");

                    //CHECK IF TAKING NOTES
                    Regex rx0 = new Regex(@"\bstart taking notes\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection matches0 = rx0.Matches(f);
                    Regex rx0_1 = new Regex(@"\btake notes\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection matches0_1 = rx0_1.Matches(f);
                    not_first = true; 
                    if (matches0_1.Count + matches0.Count > 0)
                    {
                        taking_notes = true;
                        not_first = false;
                    }

                    //TAKING NOTES
                    if (taking_notes == true && not_first == true)
                    {
                        //check if stop taking notes
                        Regex rx_0 = new Regex(@"\bstop taking notes\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches_0 = rx_0.Matches(f);
                        if (matches_0.Count > 0)
                        {
                            taking_notes = false;
                        }
                        else
                        {
                            Notes_Text.text = Notes_Text.text + f + "\n";

                        }
                    }

                    //NOT TAKING NOTES
                    else
                    {

                        //MENU
                        Regex rx = new Regex(@"\bOpen menu\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches = rx.Matches(f);
                        if (matches.Count > 0)
                        {
                            //System.IO.File.AppendAllText(@"speech_finaloutput.txt", f);
                            if (menu_open == false)
                            {
                                Menu_Buttons.SetActive(true);
                                menu_open = true;
                            }
                        }
                        Regex rx1 = new Regex(@"\bClose menu\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches1 = rx1.Matches(f);
                        if (matches1.Count > 0)
                        {
                            if (menu_open == true)
                            {
                                Menu_Buttons.SetActive(false);
                                menu_open = false;
                            }
                        }

                        //NOTEPAD
                        Regex rx2 = new Regex(@"\bOpen notepad\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches2 = rx2.Matches(f);
                        Regex rx3 = new Regex(@"\bOpen notes\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches3 = rx3.Matches(f);
                        notepad_matches = matches2.Count + matches3.Count;
                        if (notepad_matches > 0)
                        {
                            //System.IO.File.AppendAllText(@"speech_finaloutput.txt", f);
                            if (notepad_open == false)
                            {
                                Notepad.SetActive(true);
                                notepad_open = true;
                            }
                        }
                        Regex rx4 = new Regex(@"\bClose notepad\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches4 = rx4.Matches(f);
                        Regex rx5 = new Regex(@"\bClose notes\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches5 = rx5.Matches(f);
                        notepad_matches = matches4.Count + matches5.Count;
                        if (notepad_matches > 0)
                        {
                            //System.IO.File.AppendAllText(@"speech_finaloutput.txt", f);
                            if (notepad_open == true)
                            {
                                Notepad.SetActive(false);
                                notepad_open = false;
                            }
                        }

                        //MARK LOCATION
                        Regex rx9 = new Regex(@"\bMark Location\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches9 = rx9.Matches(f);
                        Regex rx10 = new Regex(@"\bTag Location\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches10 = rx10.Matches(f);
                        int location_matches = matches9.Count + matches10.Count;
                        if(location_matches > 0)
                        {
                            UnityEngine.Vector3 cam_pos = Camera.main.transform.position;
                            string cam_pos_string = cam_pos.ToString();
                            System.IO.File.AppendAllText(@"mark_location.txt", cam_pos_string);
                        }
                    }

                }
            }
            System.IO.File.Create(@"speech_output.txt").Close();
            counter = 0;
        }
    }
}