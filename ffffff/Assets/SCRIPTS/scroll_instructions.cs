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
            Instructions.SetActive(false);
            instructions_open = false;
        }
    }


    public static void update_instructions(Material MMSEV, GameObject Instructions, TextMesh Instructions_Text, int current, GameObject ORIG_ROVER, GameObject ORIG_TIRE, GameObject rover_normal, GameObject jack_screw, GameObject tire, GameObject wrench, GameObject wheel_wedge, GameObject rover_no_tire, GameObject rover_cap_removed, GameObject rover_broken_tire, GameObject cap)
    {
        print("CURR " + current);
        current = current + 5;

        instructions_arr = access_database.instruct_text_array;
        instructions_models_arr = access_database.instruct_asset_array;
        instruct_num_arr = access_database.instruct_num_array;

        current_text = instructions_arr[current];
        current_asset_text = instructions_models_arr[current];
        current_num_text = (current-3).ToString();

        //Instructions_Text.text = instructions_arr[current];

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


        if (current_num_text.Contains("14"))
        {

            rover_normal.SetActive(true);
            jack_screw.SetActive(true);
            //jack_screw.gameObject.transform.localScale += new Vector3(.05f, .05f, .05f);
            rover_normal.transform.Rotate(0, 0, -20);
            instance.StartCoroutine(instruct_14(rover_normal, jack_screw));
        }


        //ACTUALLY 9
        else if ( current-3 == 6) 
        //else if (current_num_text.Contains("6"))
        {
            Instructions_Text.text = "Mount the spare on the axel";
            rover_no_tire.SetActive(true);
            tire.SetActive(true);
            tire.GetComponentInChildren<MeshRenderer>().material = MMSEV;
            jack_screw.SetActive(true);
            rover_no_tire.transform.position = new Vector3(0f, 0f, 0f);
            rover_no_tire.transform.eulerAngles = new Vector3(0f, 0f, -20f);
            rover_no_tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            jack_screw.transform.position = new Vector3(-.75f, -1.25f, -2.1f);
            jack_screw.transform.localScale = new Vector3(.005f, .005f, .005f);
            tire.transform.position = new Vector3(-.204f, -.404f, 0f);
            tire.transform.eulerAngles = new Vector3(0f, 0f, -20f);
            tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            instance.StartCoroutine(instruct_9(tire));
            
        }
        //ACTUALLY 8
        else if (current - 3 == 5)
        //else if (current_num_text.Contains("5"))
        {
            Instructions_Text.text = "Remove the flat tire from \nthe axel";
            rover_no_tire.SetActive(true);
            tire.SetActive(true);
            jack_screw.SetActive(true);
            ORIG_ROVER.SetActive(true);
            ORIG_TIRE.SetActive(true);
            rover_no_tire.transform.position = new Vector3(0f, 0f, 0f);
            rover_no_tire.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            rover_no_tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            jack_screw.transform.position = new Vector3(-.75f, -1.25f, -2.1f);
            jack_screw.transform.localScale = new Vector3(.005f, .005f, .005f);
            tire.transform.position = new Vector3(0f, 0f, 0f);
            tire.transform.eulerAngles = new Vector3(0f, 0f, -15f);
            tire.transform.localScale = new Vector3(.75f, .75f, .75f);
            instance.StartCoroutine(instruct_8(tire));
        }

        //ACTUALLY 7
        else if (current -3 ==4)
        //else if (current_num_text.Contains("4"))
        {
            Instructions_Text.text = "Completely remove the cap \nthe rest of the way";
            rover_no_tire.SetActive(true);
            tire.SetActive(true);
            jack_screw.SetActive(true);
            cap.SetActive(true);
            wrench.SetActive(true);
            ORIG_TIRE.SetActive(true);
            ORIG_ROVER.SetActive(true);
            ORIG_ROVER.transform.Rotate(0f, 0f, -15f);
            ORIG_TIRE.transform.Rotate(0f, 0f, -15f);
            jack_screw.transform.position = new Vector3(-.75f,-1.25f,-2.1f);
            wrench.transform.position = new Vector3(-2.5f, -.85f, -1.96f);
            rover_no_tire.transform.Rotate(0, 0, 0);
            tire.transform.Rotate(0, 0, 0);
            cap.transform.Rotate(0, 0, -15);
            wrench.transform.Rotate(0, 0f, 0);
            instance.StartCoroutine(instruct_7(cap, wrench));
        }
        //ACTUALLY 6
        else if (current - 3 == 3)
        //else if (current_num_text.Contains("3"))
        {
            Instructions_Text.text = "Using the jack, raise the \n tire until the tire is about \n6 inches off the surface";
            rover_no_tire.SetActive(true);
            tire.SetActive(true);
            jack_screw.SetActive(true);
            ORIG_ROVER.SetActive(true);
            ORIG_TIRE.SetActive(true);
            rover_normal.transform.Rotate(0, 0, 0);
            jack_screw.transform.localScale = new Vector3(.004f, .002f, .004f);
            jack_screw.transform.position = new Vector3(-.75f, -1.5f, -2.1f);
            jack_screw.transform.Rotate(0f, 0f, 0f);
            instance.StartCoroutine(instruct_6(rover_no_tire, tire, jack_screw));
        }
        //ACTUALLY 5
        else if (current - 3 == 2)
        //else if (current_num_text.Contains("2"))
        {
            Instructions_Text.text = "Place the jack under rover \nat the base frame by the \ndamaged wheel.";
            rover_no_tire.SetActive(true);
            tire.SetActive(true);
            jack_screw.SetActive(true);
            wheel_wedge.SetActive(true);
            ORIG_ROVER.SetActive(true);
            ORIG_TIRE.SetActive(true);
            jack_screw.transform.position = new Vector3(-.75f, -1.55f, -2.9f);
            jack_screw.transform.localScale = new Vector3(.004f, .0015f, .004f);
            wheel_wedge.transform.position = new Vector3(-1.28f, -1.6f, -2f);
            instance.StartCoroutine(instruct_5(jack_screw));
        }
        //ACTUALLY 1
        else if (current_num_text.Contains("0"))
        {
            cap.SetActive(true);
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
        var targetPos = jack_screw.transform.position - new Vector3(-.1f, .25f, .15f);
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            rover_normal.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            jack_screw.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            //jack_screw.transform.position = Vector3.Lerp(fromPos, targetPos, t);
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

    static IEnumerator instruct_9(GameObject tire) // A couroutine can be run each frame so we can do animation.
    {
        float step = 1f / (speed*2); // How much to step by per sec
        float endTime = Time.time + (speed/2); // When to end the coroutine
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

}
