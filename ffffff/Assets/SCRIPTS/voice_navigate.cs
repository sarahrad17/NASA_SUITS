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
using MongoDB.Bson.IO;

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
    public GameObject Sample_Instructions;
    public TextMesh Sample_Instructions_Text;
    public bool recording_sample_notes;
    public bool sample_status;
    public TextMesh photo_time;
    public string start_time;
    public int sample_session;
    public string sample_start_time;
    public int sample_num;
    public bool recording_sample;
    public bool recording_sample_size;
    public bool recording_sample_color;
    public bool recording_sample_texture;
    public bool recording_major_components;
    public bool recording_char_features;
    public string fff;

    //instructions
    public GameObject Instructions;
    public TextMesh Instructions_Text;
    public int current;

    public List<GameObject> models = new List<GameObject>();
    public GameObject rover_normal;
    public GameObject jack_screw;
    public GameObject tire;
    public GameObject wrench;
    public GameObject wheel_wedge;
    public GameObject rover_no_tire;
    public GameObject cap;

    public GameObject ORIG_ROVER;
    public GameObject ORIG_TIRE;
    
    

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

        models.Add(rover_normal);
        models.Add(jack_screw);
        models.Add(tire);
        models.Add(wrench);
        models.Add(wheel_wedge);
        models.Add(rover_no_tire);
        models.Add(cap);

        foreach(GameObject m in models)
        {
            m.SetActive(false);
        }

        //for demo
        ORIG_ROVER.SetActive(false);
        ORIG_TIRE.SetActive(false);

        //sample
        Sample.SetActive(false);
        Sample_Instructions.SetActive(false);
        recording_sample_notes = false;
        recording_sample = false;
        recording_sample_size = false;
        recording_sample_color = false;
        recording_sample_texture = false;
        recording_major_components = false;
        recording_char_features = false;
        sample_num = 0;
        sample_session = 0;
        Sample_Instructions_Text.text = "";

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
                System.IO.File.WriteAllText(@"speech_output.txt", string.Empty);

                foreach (string f in file_contents)
                {
                    System.IO.File.AppendAllText(@"speech_finaloutput.txt", f);
                    System.IO.File.AppendAllText(@"speech_finaloutput.txt", "\n");

                    //CHECK IF TAKING NOTES
                    Regex rx_start_taking_notes = new Regex(@"\bstart taking notes\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection matches_start_taking_notes = rx_start_taking_notes.Matches(f);
                    Regex rx_start_taking_notes2 = new Regex(@"\btake notes\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection matches_start_taking_notes2 = rx_start_taking_notes2.Matches(f);
                    not_first = true; 
                    if (matches_start_taking_notes.Count + matches_start_taking_notes2.Count > 0)
                    {
                        taking_notes = true;
                        not_first = false;
                    }

                    //TAKING NOTES
                    if (taking_notes && not_first)
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

                    //else if recording notes abt environment
                    else if(recording_sample_notes)
                    {
                        recording_sample_notes = sample.Record_Features("picture_testing", Sample_Text, f, start_time);
                    }

                    else if (recording_sample_size)
                    {
                        print("here");
                        recording_sample_size = sample.Record_Sample_Size("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", Sample_Text, f);
                        Sample_Text.text = JsonTest.add_newlines(System.IO.File.ReadAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt"), 40);
                        if (!recording_sample_size && fff!=f)
                        {
                            fff = f;
                            Sample_Instructions_Text.text = JsonTest.add_newlines("Speak now to record sample color or tone (Ex. grey, black, streaked, shiny)", 30);
                            System.IO.File.AppendAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", "Sample Texture:\n");
                            recording_sample_color = true;
                        }
                    }

                    else if (recording_sample_color)
                    {
                        recording_sample_color = sample.Record_Sample_Color("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", Sample_Text, f);
                        Sample_Text.text = JsonTest.add_newlines(System.IO.File.ReadAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt"), 40);
                        if (!recording_sample_color && fff!=f)
                        {
                            fff = f;
                            Sample_Instructions_Text.text = JsonTest.add_newlines("Speak now to record sample grain size & texture (Ex.fine, metallic, grassy)", 30);
                            System.IO.File.AppendAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", "Sample Texture:\n");
                            recording_sample_texture = true;
                        }
                    }

                    else if (recording_sample_texture)
                    {
                        recording_sample_texture = sample.Record_Sample_Texture("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", Sample_Text, f);
                        Sample_Text.text = JsonTest.add_newlines(System.IO.File.ReadAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt"),40);
                        if (!recording_sample_texture)
                        {
                            Sample_Instructions_Text.text = JsonTest.add_newlines("Speak now to record sample mineral/clast description (Ex. Size, Shape, Color, Sorting/Approximate %)", 30);
                            System.IO.File.AppendAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", "Sample Mineral/Clast Description:\n");
                            recording_major_components = true;
                        }
                    }

                    else if (recording_major_components)
                    {
                        recording_major_components = sample.Record_Major_Components("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", Sample_Text, f);
                        Sample_Text.text = JsonTest.add_newlines(System.IO.File.ReadAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt"), 40);
                        if (!recording_major_components)
                        {
                            Sample_Instructions_Text.text = JsonTest.add_newlines("Speak now to record sample density, durability, & any surface features. ", 26);
                            System.IO.File.AppendAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", "Major Components:\n");
                            recording_char_features = true;
                        }
                    }

                    else if (recording_char_features)
                    {
                        recording_char_features = sample.Record_Major_Components("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info", Sample_Text, f);
                        Sample_Text.text = JsonTest.add_newlines(System.IO.File.ReadAllText("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info"), 40);
                        if (!recording_char_features)
                        {
                            Sample_Instructions_Text.text = JsonTest.add_newlines("Speak now to record any initial geological interpretations or additional comments", 30);
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
                            if (!menu_open)
                            {
                                Menu_Buttons.SetActive(true);
                                menu_open = true;
                            }
                        }
                        Regex rx1 = new Regex(@"\bClose menu\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches1 = rx1.Matches(f);
                        if (matches1.Count > 0)
                        {
                            if (menu_open)
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
                            if (!notepad_open)
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

                        Regex rx_set_up_sample = new Regex(@"\bSet up Sample\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches_set_up_sample = rx_set_up_sample.Matches(f);
                        position_matches = matches_set_up_sample.Count;
                        if (position_matches > 0)
                        {
                            print("oof");
                            //PRESAMPLE
                            //prelimiary set up                     
                            start_time = sample.Start_NoteTaking("picture_testing", Sample, Sample_Text, sample_session, Sample_Instructions_Text, photo_time);
                            //record additional features
                            sample sam = Sample.AddComponent<sample>() as sample;
                            recording_sample_notes =sam.Notable_Features("picture_testing", Sample_Instructions_Text, f, start_time); 
                        }

                        //COLLECT SAMPLE
                        Regex rx_collect_sample = new Regex(@"\bCollect Sample\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches_collect_sample = rx_collect_sample.Matches(f);
                        position_matches = matches_collect_sample.Count;
                        if(position_matches > 0)
                        {
                            if (!recording_sample_size)
                            {
                                //COLLECT A SAMPLE
                                sample_start_time = sample.Take_Sample(Sample, Sample_Text, sample_session, sample_num);
                                Sample_Instructions.SetActive(true);
                                Sample_Instructions_Text.text = JsonTest.add_newlines("Speak now to record approximate sample size and shape.", 24)+ "\n \nSay stop to end recording \nand proceed.";
                                Sample_Text.text = Sample_Text.text+"\n Sample Size:\n";
                                recording_sample_size = sample.Record_Sample_Size("Sampling\\" + sample_session.ToString() + "\\" + sample_num.ToString() + "\\info.txt", Sample_Text, f);
                                fff = f;
                            }
                        }







                    }

                }
            }
            System.IO.File.Create(@"speech_output.txt").Close();
            counter = 0;
        }
    }
}