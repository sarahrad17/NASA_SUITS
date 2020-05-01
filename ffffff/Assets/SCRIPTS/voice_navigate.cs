using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Microsoft.CognitiveServices.Speech;
using System.Text.RegularExpressions;
using UnityEngine.Profiling;
using JetBrains.Annotations;

public class voice_navigate : MonoBehaviour
{

    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    public int yeet = 3;
    public int counter = 1;
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
    //map
    public GameObject Location_Data;
    public bool location_data_open;
    public TextMesh Location_Text;
    //sampling
    public GameObject Sample;
    public TextMesh Sample_Text;
    public bool sample_status; 
    //instructions
    public GameObject Instructions;
    public TextMesh Instructions_Text;
    public int current;

    public List<GameObject> models = new List<GameObject>();
    public GameObject my_rover_done_broke;
    public GameObject rover_normal;
    public GameObject jack_screw;
    public GameObject tire;
    public GameObject wrench;
    public GameObject wheel_wedge;
    public GameObject rover_no_tire;
    public GameObject rover_cap_removed;
    public GameObject rover_broken_tire;
    public GameObject cap;
    public Material MMSEV;
    public GameObject ORIG_ROVER;
    public GameObject ORIG_TIRE;
    public bool recording_sample_notes;
    

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
        //location data
        location_data_open = false;
        Location_Data.SetActive(false);
        //instructions
        Instructions.SetActive(false);
        current = 0;

        models.Add(my_rover_done_broke);
        models.Add(rover_normal);
        models.Add(jack_screw);
        models.Add(tire);
        models.Add(wrench);
        models.Add(wheel_wedge);
        models.Add(rover_no_tire);
        models.Add(rover_cap_removed);
        models.Add(cap);
        models.Add(ORIG_ROVER);
        models.Add(ORIG_TIRE);

        foreach(GameObject m in models)
        {
            m.SetActive(false);
        }
        //wrench.SetActive(true);
        //rover_normal.SetActive(true);
        //for demo
        ORIG_ROVER.SetActive(false);
        ORIG_TIRE.SetActive(false);
        rover_broken_tire.SetActive(true);
        //sample

        Sample.SetActive(false);
        recording_sample_notes = false;


        System.IO.File.Create(@"mark_location.txt").Close();
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


                    else if(recording_sample_notes == true)
                    {
                        recording_sample_notes = sample.Record_Features("picture_testing", Sample_Text, f);
                    }

                    //NOT TAKING NOTES
                    else
                    {

                        //MENU
                        Regex rx = new Regex(@"\bOpen menu\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches = rx.Matches(f);
                        if (matches.Count > 0)
                        {
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
                            if (notepad_open == true)
                            {
                                Notepad.SetActive(false);
                                notepad_open = false;
                            }
                        }

                        //LOCATION DATA
                        Regex rx11 = new Regex(@"\bOpen location data\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches11 = rx11.Matches(f);
                        Regex rx12 = new Regex(@"\bOpen locations\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches12 = rx12.Matches(f);
                        int loc_matches = matches11.Count + matches12.Count;
                        if (loc_matches > 0)
                        {
                            if (location_data_open == false)
                            {
                                Location_Data.SetActive(true);
                                location_data_open = true;
                            }
                        }
                        Regex rx13 = new Regex(@"\bClose location data\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches13 = rx13.Matches(f);
                        Regex rx14 = new Regex(@"\bClose locations\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches14 = rx14.Matches(f);
                        loc_matches = matches13.Count + matches14.Count;
                        if (loc_matches > 0)
                        {
                            if (location_data_open == true)
                            {
                                Location_Data.SetActive(false);
                                location_data_open = false;
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
                            System.IO.File.AppendAllText(@"mark_location.txt", System.DateTime.Now + " : ");
                            System.IO.File.AppendAllText(@"mark_location.txt", cam_pos_string + "\n");
                            string location_data = System.IO.File.ReadAllText(@"mark_location.txt");
                            Location_Text.text = location_data;

                        }


                        //SCROLL
                        Regex rx16 = new Regex(@"\bOpen Instructions\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches16 = rx16.Matches(f);
                        Regex rx17 = new Regex(@"\bStart Instructions\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches17 = rx17.Matches(f);
                        int scroll_matches = matches16.Count + matches17.Count;
                        if (scroll_matches > 0)
                        {
                            Instructions.SetActive(true);
                            JsonTest j = new JsonTest();
                            j.Yeet(current, Instructions_Text);
                            print("OPEN INSTRUCTIONS");
                        }

                        Regex rx18 = new Regex(@"\bClose Instructions\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches18 = rx18.Matches(f);
                        Regex rx19 = new Regex(@"\bClose the Instructions\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches19 = rx19.Matches(f);
                        scroll_matches = matches18.Count + matches19.Count;
                        if (scroll_matches > 0)
                        {
                            Instructions.SetActive(false);
                            //scroll_instructions sc = new scroll_instructions();
                            //sc.close(MMSEV, Instructions, Instructions_Text, ORIG_ROVER, ORIG_TIRE, rover_normal, jack_screw, tire, wrench, wheel_wedge, rover_no_tire, rover_cap_removed, rover_broken_tire, cap);
                            foreach (GameObject m in models)
                            {
                                m.SetActive(false);
                            }
                        }

                        Regex rx20 = new Regex(@"\bNext Instruction\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches20 = rx20.Matches(f);
                        Regex rx21 = new Regex(@"\bGo forward\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches21 = rx21.Matches(f);
                        Regex rx26 = new Regex(@"\bNext\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches26 = rx26.Matches(f);
                        int position_matches = matches20.Count + matches21.Count + matches26.Count;
                        if (position_matches > 0)
                        {
                            foreach (GameObject m in models)
                            {
                                m.SetActive(false);
                            }
                            current = current + 1;
                            JsonTest j = new JsonTest();
                            j.Yeet(current, Instructions_Text);
                        }

                        Regex rx22 = new Regex(@"\bLast Instruction\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches22 = rx22.Matches(f);
                        Regex rx23 = new Regex(@"\bGo back\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches23 = rx23.Matches(f);
                        Regex rx24 = new Regex(@"\bPrevious instruction[s]\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches24 = rx24.Matches(f);
                        Regex rx25 = new Regex(@"\bPrevious\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches25 = rx25.Matches(f);
                        position_matches = matches22.Count + matches23.Count + matches24.Count + matches25.Count;
                        if (position_matches > 0)
                        {
                            foreach (GameObject m in models)
                            {
                                m.SetActive(false);
                            }
                            current = current - 1;
                            JsonTest j = new JsonTest();
                            j.Yeet(current, Instructions_Text);
                        }

                        Regex rxsample = new Regex(@"\bCollect Sample\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches_sample = rxsample.Matches(f);
                        position_matches = matches_sample.Count;
                        if (position_matches > 0)
                        {
                            print("sample");
                            Sample.SetActive(true);
                            sample.Start_NoteTaking("picture_testing", Sample_Text);
                            //StartCoroutine(sample.Take_Scenery_Pics("yeet"));
                            sample sam = Sample.AddComponent<sample>() as sample;
                            recording_sample_notes =sam.Notable_Features("picture_testing", Instructions_Text, f);
                            
                        }









                    }

                }
            }
            System.IO.File.Create(@"speech_output.txt").Close();
            counter = 0;
        }
    }
}