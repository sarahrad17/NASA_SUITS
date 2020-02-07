using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Microsoft.CognitiveServices.Speech;
public class Voice_Navigate : MonoBehaviour
{
    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    public int yeet = 3;
    public int counter = 0; 
    public GameObject Canvas;
    private bool micPermissionGranted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Continue with normal initialization, Text and Button objects are present.
        micPermissionGranted = true;
        message = " ";
        SpeechContinuousRecognitionAsync();
    }

    public async void SpeechContinuousRecognitionAsync()
    {
        System.IO.File.Create(@"notepad.txt").Close();

        // Creates an instance of a speech config with specified subscription key and service region.
        var config = SpeechConfig.FromSubscription("82b1859945464df6a90737eef58dc46f", "westus");

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
                    System.IO.File.AppendAllText(@"notepad.txt", message);
                    System.IO.File.AppendAllText(@"notepad.txt", "\n");
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
        if (counter <150){
            counter = counter + 1;
        }
        //if 15th frame
        else
        {
            //see if input file exists yet
            if (File.Exists(@"notepad.txt"))
            {
                //read input into array of lines
                string[] lines = System.IO.File.ReadAllLines(@"notepad.txt");
                if (!File.Exists(@"log.txt"))
                {
                    System.IO.File.Create(@"log.txt");
                }
                foreach (string l in lines)
                {
                    System.IO.File.AppendAllText(@"yeet.txt", l);
                    System.IO.File.AppendAllText(@"yeet.txt", "\n");
                }
                System.IO.File.Create(@"notepad.txt").Close();
            }

            counter = 0;
        
         
        }
    }
}