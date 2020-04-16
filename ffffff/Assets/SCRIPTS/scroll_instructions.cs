using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_instructions : MonoBehaviour
{
    public static bool instructions_open;
    public static string[] instructions_arr;
    public static string[] instructions_models_arr;
    public static string[] instruct_num_arr;


    public static string current_text = "";
    public static string current_asset_text = "";
    public static string current_num_text = "";

    static public scroll_instructions instance;
    public static float speed = 20f;

    // Start is called before the first frame updates
    private void Start()
    {
        instructions_arr = access_database.instruct_text_array;
        instructions_models_arr = access_database.instruct_asset_array;
    }

    // Open: opens instruction pad
    public void open(GameObject Instructions, TextMesh Instructions_Text, int current, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {

        if (instructions_open == false)
        {
            update_instructions(current, rover_normal, jack_screw, tire, wrench, wheel_wedge, rover_no_tire, rover_cap_removed, rover_broken_tire, cap);
            Instructions_Text.text = current_text;
            Instructions.SetActive(true);
            instructions_open = true;
        }
    }

    //Close: close instruction pad
    public void close(GameObject Instructions, TextMesh Instructions_Text, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        if (instructions_open == true)
        {
            Instructions.SetActive(false);
            instructions_open = false;
        }
    }


    public static void update_instructions(int current, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        instructions_arr = access_database.instruct_text_array;
        instructions_models_arr = access_database.instruct_asset_array;
        instruct_num_arr = access_database.instruct_num_array;
        
        current_text = instructions_arr[current];
        current_asset_text = instructions_models_arr[current];
        current_num_text = instruct_num_arr[current];

        print(current_asset_text);
        print(current_num_text);
        

        rover_normal.SetActive(false);
        jack_screw.SetActive(false);
        tire.SetActive(false);
        wrench.SetActive(false);
        wheel_wedge.SetActive(false);
        rover_no_tire.SetActive(false);
        rover_cap_removed.SetActive(false);
        rover_broken_tire.SetActive(false);
        cap.SetActive(false);

        
        if (current_num_text.Contains("14") )
        {
            rover_normal.SetActive(true);
            jack_screw.SetActive(true);
            //jack_screw.gameObject.transform.localScale += new Vector3(.05f, .05f, .05f);
            rover_normal.transform.Rotate(0, 0, -20);
            instance.StartCoroutine(instruct_14(rover_normal, jack_screw));
        }

        else if (current_num_text.Contains("15"))
        {
            
        }

        else if (current_num_text.Contains("12"))
        {
            tire.SetActive(true);
        }

        else if (current_num_text.Contains("11"))
        {
            wrench.SetActive(true);
        }

        else if (current_num_text.Contains("10"))
        {
            wheel_wedge.SetActive(true);
        }

        else if (current_num_text.Contains("9"))
        {
            rover_no_tire.SetActive(true);
        }
        else if (current_num_text.Contains("8"))
        {
            rover_cap_removed.SetActive(true);
        }
        else if (current_num_text.Contains("13"))
        {
            rover_broken_tire.SetActive(true);
        }
        else if (current_num_text.Contains("6"))
        {
            cap.SetActive(true);
        }
        else if (current_num_text.Contains("5"))
        {
            cap.SetActive(true);
        }
        else if (current_num_text.Contains("8"))
        {
            cap.SetActive(true);
        }
        else if (current_num_text.Contains("6"))
        {
            rover_normal.SetActive(true);
            jack_screw.SetActive(true);
            rover_normal.transform.Rotate(0, 0, 0);
            jack_screw.transform.localScale = new Vector3(.004f, .002f, .004f);
            jack_screw.transform.position = new Vector3(-.75f, -1.5f, -2.1f);
            instance.StartCoroutine(instruct_6(rover_normal, jack_screw));
        }
        //actually 7
        else if (current_num_text.Contains("7"))
        {
            rover_no_tire.SetActive(true);
            tire.SetActive(true);
            jack_screw.SetActive(true);
            cap.SetActive(true);
            wrench.SetActive(true);
            wrench.transform.position = new Vector3(-2.5f, -.85f, -1.96f);
            rover_no_tire.transform.Rotate(0, 0, -20);
            tire.transform.Rotate(0, 0, -20);
            cap.transform.Rotate(0, 0, -20);
            wrench.transform.Rotate(0, 0f, 0);
            instance.StartCoroutine(instruct_7(cap, wrench));
        }
        //actually 1
        else if (current_num_text.Contains("0"))
        {
            cap.SetActive(true);
        }

    }

    //Go Forward: go forward one task item
    public int go_forward(TextMesh Instructions_Text, int current, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        
        current = current + 1;
        update_instructions(current, rover_normal, jack_screw, tire, wrench, wheel_wedge, rover_no_tire, rover_cap_removed, rover_broken_tire, cap);
        return current;
    }

    //Go Backward: go backwards one task item
    
    public int go_backward(TextMesh Instructions_Text, int current, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {

        current = current - 1;
        update_instructions(current, rover_normal, jack_screw, tire, wrench, wheel_wedge, rover_no_tire, rover_cap_removed, rover_broken_tire, cap);
        return current;
    }

    private void Awake()
    {
        instance = this;
    }
    static IEnumerator instruct_14(GameObject rover_normal, GameObject jack_screw) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        Vector3 rotAmount = new Vector3(0, 0, 15);
        var fromAngle = rover_normal.transform.eulerAngles; // start rotation
        var localScale = jack_screw.transform.localScale;
        var targetRot = rover_normal.transform.eulerAngles + rotAmount; // where we want to be at the end
        var targetScale = jack_screw.transform.localScale - new Vector3(.001f, .003f, .001f);
        var fromPos = jack_screw.transform.position;
        var targetPos = jack_screw.transform.position - new Vector3(-.1f,.25f,.15f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            rover_normal.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            jack_screw.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            jack_screw.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            yield return 0;
        }
    }
    static IEnumerator instruct_7(GameObject cap, GameObject wrench) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        var fromPos_13 = cap.transform.position;
        var targetPos_13 = cap.transform.position + new Vector3(-.5f, 0f, 0f);
        var fromPos_13a = wrench.transform.position;
        var targetPos_13a = wrench.transform.position + new Vector3(-.5f, 0f, 0f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            wrench.transform.position = Vector3.Lerp(fromPos_13a, targetPos_13a, t);
            cap.transform.position = Vector3.Lerp(fromPos_13, targetPos_13, t);
            yield return 0;
        }
    }

    static IEnumerator instruct_6(GameObject rover_normal, GameObject jack_screw) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        Vector3 rotAmount = new Vector3(0, 0, -15);
        var fromAngle = rover_normal.transform.eulerAngles; // start rotation
        var localScale = jack_screw.transform.localScale;
        var targetRot = rover_normal.transform.eulerAngles + rotAmount; // where we want to be at the end
        var targetScale = jack_screw.transform.localScale + new Vector3(.001f, .0005f, .001f);
        var fromPos = jack_screw.transform.position;
        var targetPos = jack_screw.transform.position + new Vector3(-.1f, .5f, .15f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            rover_normal.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            jack_screw.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            jack_screw.transform.eulerAngles = Vector3.Lerp(fromPos, targetPos, t);
            yield return 0;
        }
    }

    }
