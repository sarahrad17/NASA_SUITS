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
    public static float speed = 10f;

    // Start is called before the first frame updates
    private void Start()
    {
        instructions_arr = access_database.instruct_text_array;
        instructions_models_arr = access_database.instruct_asset_array;
    }

    // Open: opens instruction pad
    public void open(Material MMSEV, GameObject Instructions, TextMesh Instructions_Text, int current, GameObject ORIG_ROVER, GameObject ORIG_TIRE, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        if (instructions_open == false)
        {
            update_instructions(MMSEV, Instructions, Instructions_Text, current, ORIG_ROVER, ORIG_TIRE, rover_normal, jack_screw, tire, wrench, wheel_wedge, rover_no_tire, rover_cap_removed, rover_broken_tire, cap);
            Instructions.SetActive(true);
            instructions_open = true;
        }
    }

    //Close: close instruction pad
    public void close(Material MMSEV, GameObject Instructions, TextMesh Instructions_Text, GameObject ORIG_ROVER, GameObject ORIG_TIRE, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        if (instructions_open == true)
        {
            rover_normal.SetActive(false);
            jack_screw.SetActive(false);
            tire.SetActive(false);
            wrench.SetActive(false);
            wheel_wedge.SetActive(false);
            rover_no_tire.SetActive(false);
            rover_cap_removed.SetActive(false);
            rover_broken_tire.SetActive(false);
            cap.SetActive(false);

            Instructions.SetActive(false);
            instructions_open = false;
        }
    }

    public static string add_newlines(string full_string)
    {
        //if less than 26 chars
        if (full_string.Length <= 26)
        {
            return full_string;
        }
        //substring if more than 26 remain
        else
        {
            //substring first 26
            string sub_26 = full_string.Substring(0, 26);
            //find last space
            int last_space = sub_26.LastIndexOf(" ");
            //end line at last space
            string first_line = full_string.Substring(0, last_space);
            //length of new first line 
            int curr_length = first_line.Length;
            //add space
            full_string = first_line + "\n" + full_string.Substring(curr_length);
            //add \n 
            curr_length = curr_length + 1;
            //while remaining chars is longer than 26
            while ((full_string.Length - curr_length) > 26)
            {
                //new substring of next 26 chars
                sub_26 = full_string.Substring(curr_length, 26);
                //find last space
                last_space = sub_26.LastIndexOf(" ");
                //add \n at end of last space
                full_string = full_string.Substring(0, curr_length + last_space) + "\n" + full_string.Substring(curr_length + last_space);
                //increment curr_length
                curr_length = curr_length + last_space + 1;
            }
            return full_string;
        }

    }

    public static void update_instructions(Material MMSEV, GameObject Instructions, TextMesh Instructions_Text, int current, GameObject ORIG_ROVER, GameObject ORIG_TIRE, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        instructions_arr = access_database.instruct_text_array;
        instructions_models_arr = access_database.instruct_asset_array;
        instruct_num_arr = access_database.instruct_num_array;

        current_text = instructions_arr[current];
        current_asset_text = instructions_models_arr[current];
        current_num_text = (current-3).ToString();

        //SET INSTRUCTION TEXT

        Instructions_Text.text = add_newlines(instructions_arr[current]);
        print(current);

        rover_normal.SetActive(false);
        jack_screw.SetActive(false);
        tire.SetActive(false);
        wrench.SetActive(false);
        wheel_wedge.SetActive(false);
        rover_no_tire.SetActive(false);
        rover_cap_removed.SetActive(false);
        rover_broken_tire.SetActive(false);
        cap.SetActive(false);
        ORIG_ROVER.SetActive(false);
        ORIG_TIRE.SetActive(false);

        //ACTUALLY 12
        //Tighten the cap with the wrench the rest of the way
        if (current == 12)
        {
            //set rover active and elevated 
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0f, 0f, 0f);
            rover_normal.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set active wrench
            wrench.SetActive(true);
            wrench.transform.position = new Vector3(-2.1f, -1.36f, -1.94f);
            wrench.transform.eulerAngles = new Vector3(0, 0, 0);
            wrench.transform.localScale = new Vector3(.05f, .05f, .05f);
            //set active cap
            cap.SetActive(true);
            cap.transform.position = new Vector3(0, 0, 0);
            cap.transform.eulerAngles = new Vector3(0, 0, 0);
            cap.transform.localScale = new Vector3(.75f, .75f, .75f);

            instance.StartCoroutine(instruct_4(wrench));
        }

        //ACTUALLY 11
        //Using the jack, lower the rover all the way down
        else if (current == 11)
        {
            //set rover active and elevated 
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0f, 0f, 0f);
            rover_normal.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set jack screw active and elevated
            jack_screw.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.25f, -2.1f);
            jack_screw.transform.eulerAngles = new Vector3(0, 0, -15f);
            jack_screw.transform.localScale = new Vector3(.005f, .005f, .005f);

            instance.StartCoroutine(instruct_11(rover_normal, jack_screw));
        }

        //ACTUALLY 10
        //Place on the cap and tighten with the wrench
        else if (current == 10)
        {
            //set rover active and elevated 
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0f, 0f, 0f);
            rover_normal.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            //jack screw active and elevated
            jack_screw.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.25f, -2.1f);
            jack_screw.transform.eulerAngles = new Vector3(0, 0, -15f);
            jack_screw.transform.localScale = new Vector3(.005f, .005f, .005f);
            //set cap active and away from vehicle
            cap.SetActive(true);
            cap.transform.position = new Vector3(-.5f, 0f, 0f);
            cap.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            cap.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set wrench active and away from vehicle
            wrench.SetActive(true);
            wrench.transform.position = new Vector3(-3f, -.86f, -1.96f);
            wrench.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            wrench.transform.localScale = new Vector3(.05f, .05f, .05f);

            instance.StartCoroutine(instruct_10(wrench, cap));
        }

        //ACTUALLY 9
        //Mount the spare on the axel
        else if (current == 9) 
        {
            //set rover active and elevated
            rover_no_tire.SetActive(true);
            rover_no_tire.transform.position = new Vector3(0f, 0f, 0f);
            rover_no_tire.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            rover_no_tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set tire active and away from rover
            tire.SetActive(true);
            tire.GetComponentInChildren<MeshRenderer>().material = MMSEV;
            tire.transform.position = new Vector3(-.204f, -.404f, 0f);
            tire.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set tire jack screw active and elevated 
            jack_screw.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.25f, -2.1f);
            jack_screw.transform.eulerAngles = new Vector3(0, 0, -15f);
            jack_screw.transform.localScale = new Vector3(.005f, .005f, .005f);
            
            instance.StartCoroutine(instruct_9(tire));   
        }

        //ACTUALLY 8
        //Remove the flat tire from the axel
        else if (current == 8)
        {
            //set rover active and elevated
            rover_no_tire.SetActive(true);
            rover_no_tire.transform.position = new Vector3(0f, 0f, 0f);
            rover_no_tire.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            rover_no_tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set tire active and elevated
            tire.SetActive(true);
            tire.GetComponentInChildren<MeshRenderer>().material = MMSEV;
            tire.transform.position = new Vector3(0f, 0f, 0f);
            tire.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set jack_screw active and elevated
            jack_screw.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.25f, -2.1f);
            jack_screw.transform.eulerAngles = new Vector3(0, 0, -15);
            jack_screw.transform.localScale = new Vector3(.005f, .005f, .005f);
            
            instance.StartCoroutine(instruct_8(tire));
        }

        //ACTUALLY 7
        //Completely remove the cap the rest of the way
        else if (current == 7)
        {
            //set rover active & in raised position
            rover_no_tire.SetActive(true);
            rover_no_tire.transform.position = new Vector3(0, 0, 0);
            rover_no_tire.transform.eulerAngles = new Vector3(0, 0, -15);
            rover_no_tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set tire active & in raised position
            tire.SetActive(true);
            tire.transform.position = new Vector3(0, 0, 0);
            tire.transform.eulerAngles = new Vector3(0, 0, -15);
            tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //jack screw active & in raised position
            jack_screw.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.25f, -2.1f);
            jack_screw.transform.eulerAngles = new Vector3(0,0,-15);
            jack_screw.transform.localScale = new Vector3(.005f, .005f, .005f);
            //cap active & at original position
            cap.SetActive(true);
            cap.transform.position = new Vector3(0, 0, 0);
            cap.transform.eulerAngles = new Vector3(0, 0, -15);
            cap.transform.localScale = new Vector3(.75f,.75f,.75f);
            //wrench active & at original position
            wrench.SetActive(true);
            wrench.transform.position = new Vector3(-2.5f, -.85f, -1.96f);
            wrench.transform.eulerAngles = new Vector3(0, 0, 0);
            wrench.transform.localScale = new Vector3(.05f,.05f,.05f);

            instance.StartCoroutine(instruct_7(cap, wrench));
        }

        //ACTUALLY 6
        //Using the jack raise the tire until the tire is about 1 ft off the surface
        else if (current == 6)
        {
            //set rover active & at origin 
            rover_no_tire.SetActive(true);
            rover_no_tire.transform.position = new Vector3(0, 0, 0);
            rover_no_tire.transform.eulerAngles = new Vector3(0, 0, 0);
            rover_no_tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set tire active & at origin 
            tire.SetActive(true);
            tire.transform.position = new Vector3(0, 0, 0);
            tire.transform.eulerAngles = new Vector3(0, 0, 0);
            tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set jack screw active and placed under rover
            jack_screw.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.5f, -2.1f);
            jack_screw.transform.eulerAngles = new Vector3(0, 0, 0);
            jack_screw.transform.localScale = new Vector3(.004f, .002f, .004f);

            instance.StartCoroutine(instruct_6(rover_no_tire, tire, jack_screw));
        }

        //ACTUALLY 5
        //Place jack under rover at the axel of the damaged wheel
        else if (current == 5)
        //else if (current_num_text.Contains("2"))
        {
            //set rover active & to origin
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0, 0, 0);
            rover_normal.transform.eulerAngles = new Vector3(0, 0, 0);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set jack screw active & away from rover
            jack_screw.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.55f, -2.9f);
            jack_screw.transform.eulerAngles = new Vector3(0, 0, 0);
            jack_screw.transform.localScale = new Vector3(.004f, .0015f, .004f);
            //set wheel wedge active and behind wheel
            wheel_wedge.SetActive(true);
            wheel_wedge.transform.position = new Vector3(-1.28f, -1.6f, -2f);
            wheel_wedge.transform.eulerAngles = new Vector3(0, 0, 0);
            wheel_wedge.transform.localScale = new Vector3(.1f,.1f,.1f);

            instance.StartCoroutine(instruct_5(jack_screw));
        }

        //ACTUALLY 4
        //Use wrench to loosen the cap.
        else if (current == 4)
        {
            //set rover_normal active & to origin 
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0, 0, 0);
            rover_normal.transform.eulerAngles = new Vector3(0, 0, 0);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set wrench active & to start location
            wrench.SetActive(true);
            wrench.transform.position = new Vector3(-2.1f, -1.36f, -1.94f);
            wrench.transform.eulerAngles = new Vector3(0, 0, 0);
            wrench.transform.localScale = new Vector3(.05f, .05f, .05f);
            //set cap active & to origin
            cap.SetActive(true);
            cap.transform.position = new Vector3(0, 0, 0);
            cap.transform.eulerAngles = new Vector3(0, 0, 0);
            cap.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set wheel wedge active and behind wheel
            wheel_wedge.SetActive(true);
            wheel_wedge.transform.position = new Vector3(-1.28f, -1.6f, -2f);
            wheel_wedge.transform.eulerAngles = new Vector3(0, 0, 0);
            wheel_wedge.transform.localScale = new Vector3(.1f, .1f, .1f);

            instance.StartCoroutine(instruct_4(wrench));
        }

        //ACTUALLY 3
        //Once the tire is in place identify the cap that secures the tire
        else if (current == 3)
        {
            //set rover_normal active & to origin
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0, 0, 0);
            rover_normal.transform.eulerAngles = new Vector3(0, 0, 0);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set cap active to origin 
            cap.SetActive(true);
            cap.transform.position = new Vector3(0, 0, 0);
            cap.transform.eulerAngles = new Vector3(0, 0, 0);
            cap.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set wheel wedge active and behind wheel
            wheel_wedge.SetActive(true);
            wheel_wedge.transform.position = new Vector3(-1.28f, -1.6f, -2f);
            wheel_wedge.transform.eulerAngles = new Vector3(0, 0, 0);
            wheel_wedge.transform.localScale = new Vector3(.1f, .1f, .1f);

            instance.StartCoroutine(instruct_3(cap));
        }

        //ACTUALLY 2
        //Position the wheel wedge behind the damaged wheel
        else if (current == 2)
        {
            //set rover_normal active & to origin
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0, 0, 0);
            rover_normal.transform.eulerAngles = new Vector3(0, 0, 0);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            //set wheel wedge active and to away from wheel
            wheel_wedge.SetActive(true);
            wheel_wedge.transform.position = new Vector3(-1.28f,-1.6f,-2.5f);
            wheel_wedge.transform.eulerAngles = new Vector3(0,0,0);
            wheel_wedge.transform.localScale = new Vector3(.1f, .1f, .1f);

            instance.StartCoroutine((instruct_2(wheel_wedge)));
        }

        //ACTUALLY 1
        //Make sure the parking brake is applied
        else if (current == 1)
        {
            //set rover_normal active & to origin
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0, 0, 0);
            rover_normal.transform.eulerAngles = new Vector3(0, 0, 0);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
            
        }

        //ACTUALLY 0
        //Locate the flat tire
        else if (current == 0)
        {
            //set rover_normal active & to origin 
            rover_normal.SetActive(true);
            rover_normal.transform.position = new Vector3(0, 0, 0);
            rover_normal.transform.eulerAngles = new Vector3(0, 0, 0);
            rover_normal.transform.localScale = new Vector3(.75f, .75f, .75f);
        }

    }

    //Go Forward: go forward one task item
    public int go_forward(Material MMSEV, GameObject Instructions, TextMesh Instructions_Text, int current, GameObject ORIG_ROVER, GameObject ORIG_TIRE, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        //Instructions_Text.text = instructions_arr[current];
        current = current + 1;
        update_instructions(MMSEV, Instructions, Instructions_Text, current, ORIG_ROVER, ORIG_TIRE, rover_normal, jack_screw, tire, wrench, wheel_wedge, rover_no_tire, rover_cap_removed, rover_broken_tire, cap);
        //current = current + 1;
        return current;
    }

    //Go Backward: go backwards one task item

    public int go_backward(Material MMSEV, GameObject Instructions, TextMesh Instructions_Text, int current, GameObject ORIG_ROVER, GameObject ORIG_TIRE, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    { 
        //Instructions_Text.text = instructions_arr[current];
        current = current - 1;
        update_instructions(MMSEV, Instructions, Instructions_Text, current, ORIG_ROVER, ORIG_TIRE, rover_normal, jack_screw, tire, wrench, wheel_wedge, rover_no_tire, rover_cap_removed, rover_broken_tire, cap);
        //current = current - 1;
        return current;
    }

    private void Awake()
    {
        instance = this;
    }
    static IEnumerator instruct_11(GameObject rover_normal, GameObject jack_screw) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        Vector3 rotAmount = new Vector3(0, 0, 15);
        var fromAngle = rover_normal.transform.eulerAngles; // start rotation
        var targetRot = rover_normal.transform.eulerAngles + rotAmount; // where we want to be at the end
        var localScale = jack_screw.transform.localScale;
        var targetScale = jack_screw.transform.localScale - new Vector3(.001f, .003f, .001f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            rover_normal.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            jack_screw.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            yield return 0;
        }
    }

    static IEnumerator instruct_10(GameObject cap, GameObject wrench) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        var fromPos_13 = cap.transform.position;
        var targetPos_13 = cap.transform.position + new Vector3(.5f, 0f, 0f);
        var fromPos_13a = wrench.transform.position;
        var targetPos_13a = wrench.transform.position + new Vector3(.5f, 0f, 0f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            wrench.transform.position = Vector3.Lerp(fromPos_13a, targetPos_13a, t);
            cap.transform.position = Vector3.Lerp(fromPos_13, targetPos_13, t);
            yield return 0;
        }
    }
    static IEnumerator instruct_9(GameObject tire) // A couroutine can be run each frame so we can do animation.
    {
        float step = 1f / (speed * 2); // How much to step by per sec
        float endTime = Time.time + (speed / 2); // When to end the coroutine
        var fromPos = tire.transform.position;
        var targetPos = tire.transform.position + new Vector3(0f, 1.8f, 0f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            tire.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            yield return 0;
        }

        float stepa = 1f / (speed * 2); // How much to step by per sec
        float endTimea = Time.time + (speed / 2); // When to end the coroutine
        var fromPosa = tire.transform.position;
        var targetPosa = new Vector3(.5f, 0f, 0f);
        float ta = 0; // how far we are. 0-1
        while (Time.time <= endTimea)
        {
            ta += stepa * Time.deltaTime;
            tire.transform.position = Vector3.Lerp(fromPosa, targetPosa, ta);
            yield return 0;
        }

    }

    static IEnumerator instruct_8(GameObject tire) // A couroutine can be run each frame so we can do animation.
    {
        float step = 1f / speed; // How much to step by per sec
        float endTime = Time.time + speed; // When to end the coroutine
        var fromPos = tire.transform.position;
        var targetPos = tire.transform.position + new Vector3(-.204f, .204f, 0f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            tire.transform.position = Vector3.Lerp(fromPos, targetPos, t);
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

    static IEnumerator instruct_6(GameObject rover_no_tire, GameObject tire, GameObject jack_screw) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        Vector3 rotAmount = new Vector3(0, 0, -15);
        var fromAngle = rover_no_tire.transform.eulerAngles; // start rotation
        var localScale = jack_screw.transform.localScale;
        var targetRot = rover_no_tire.transform.eulerAngles + rotAmount; // where we want to be at the end
        var targetScale = jack_screw.transform.localScale + new Vector3(.001f, .0007f, .001f);
        var fromPos = jack_screw.transform.position;
        var targetPos = jack_screw.transform.position + new Vector3(-.1f, .09f, .15f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            rover_no_tire.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            tire.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            jack_screw.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            jack_screw.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            jack_screw.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            yield return 0;
        }
    }

    static IEnumerator instruct_5(GameObject jack_screw) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        var fromPos = jack_screw.transform.position;
        var targetPos = jack_screw.transform.position + new Vector3(0f, 0f, .8f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            jack_screw.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            yield return 0;
        }
    }

    static IEnumerator instruct_4(GameObject wrench) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        var fromAngle = wrench.transform.eulerAngles; // start rotation
        Vector3 rotAmount = new Vector3(90, 0, 0);
        var targetRot = wrench.transform.eulerAngles + rotAmount; // where we want to be at the end
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            wrench.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            yield return 0;
        }
    }

    static IEnumerator instruct_3(GameObject cap) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        var fromPos = cap.transform.position;
        var targetPos = cap.transform.position + new Vector3(-.5f, 0f, 0f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            cap.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            yield return 0;
        }
        float endTime2 = Time.time + speed; // When to end the coroutine
        float step2 = 1f / speed; // How much to step by per sec
        var fromPos2 = cap.transform.position;
        var targetPos2 = cap.transform.position + new Vector3(.5f, 0f, 0f);
        float t2 = 0; // how far we are. 0-1
        while (Time.time <= endTime2)
        {
            t2 += step2 * Time.deltaTime;
            cap.transform.position = Vector3.Lerp(fromPos2, targetPos2, t2);
            yield return 0;
        }
    }

    static IEnumerator instruct_2(GameObject wheel_wedge) // A couroutine can be run each frame so we can do animation.
    {
        float endTime = Time.time + speed; // When to end the coroutine
        float step = 1f / speed; // How much to step by per sec
        var fromPos = wheel_wedge.transform.position;
        var targetPos = wheel_wedge.transform.position + new Vector3(0f, 0f, .5f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            wheel_wedge.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            yield return 0;
        }
    }
}
