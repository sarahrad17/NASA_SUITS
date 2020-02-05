using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;

public class Speech : MonoBehaviour
{
    // Hook up the two properties below with a Text and Button object in your UI.

    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    public int yeet = 3;
    public GameObject Canvas;
    

private bool micPermissionGranted = false;

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


    void Start()
    {
        // Continue with normal initialization, Text and Button objects are present.
        micPermissionGranted = true;
        message = " ";
        SpeechContinuousRecognitionAsync();    
    }
}