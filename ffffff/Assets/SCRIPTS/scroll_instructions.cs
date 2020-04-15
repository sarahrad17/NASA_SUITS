using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_instructions : MonoBehaviour
{
    public static bool instructions_open;
    public static string[] instructions_arr;
    public static string[] instructions_models_arr;


    public static string current_text = "";
    public static string current_asset_text = "";
    public static GameObject rover_normal = GameObject.Find("Rover_Normal");
    public static GameObject jack_screw = GameObject.Find("Jack_Screw");
    public static GameObject tire = GameObject.Find("Tire");
    public static GameObject wrench = GameObject.Find("Wrench");
    public static GameObject wheel_wedge = GameObject.Find("Wheel_Wedge");
    public static GameObject rover_no_tire = GameObject.Find("Rover_No_Tire");
    public static GameObject rover_cap_removed = GameObject.Find("Rover_Cap_Removed");
    public static GameObject rover_broken_tire = GameObject.Find("Rover_Broken_Tire");
    public static GameObject cap = GameObject.Find("Cap");
    

    // Start is called before the first frame updates
    void Start()
    {
        set_up();
    }

    // Open: opens instruction pad
    public static void open(GameObject Instructions, TextMesh Instructions_Text, int current)
    { 
        if (instructions_open == false)
        {
            update_instructions(current);
            Instructions_Text.text = current_text;
            Instructions.SetActive(true);
            instructions_open = true;
        }
    }

    //Close: close instruction pad
    public static void close(GameObject Instructions, TextMesh Instructions_Text)
    {
        if (instructions_open == true)
        {
            Instructions.SetActive(false);
            instructions_open = false;
        }
    }

    //Setup: setup instruction array text
    public static void set_up()
    {
        instructions_arr = access_database.instruct_text_array;
        instructions_models_arr = access_database.instruct_asset_array;
        print(string.Join("\n", instructions_arr));
    }

    public static void update_instructions(int current)
    {
        rover_normal = GameObject.Find("Rover_Normal");
        jack_screw = GameObject.Find("Jack_Screw");
        tire = GameObject.Find("Tire");
        wrench = GameObject.Find("Wrench");
        wheel_wedge = GameObject.Find("Wheel_Wedge");
        rover_no_tire = GameObject.Find("Rover_No_Tire");
        rover_cap_removed = GameObject.Find("Rover_Cap_Removed");
        rover_broken_tire = GameObject.Find("Rover_Broken_Tire");
        cap = GameObject.Find("Cap");
        
        set_up();
        current_text = instructions_arr[current];
        current_asset_text = instructions_models_arr[current];
        print(current_asset_text);

        rover_normal.SetActive(false);
        jack_screw.SetActive(false);
        tire.SetActive(false);
        wrench.SetActive(false);
        wheel_wedge.SetActive(false);
        rover_no_tire.SetActive(false);
        rover_cap_removed.SetActive(false);
        rover_broken_tire.SetActive(false);

        if (current_asset_text.Contains("Rover_Normal") )
        {
            print("YEET");
            rover_normal.SetActive(true);
        }

        if (current_asset_text.Contains("Jack_Screw"))
        {
            jack_screw.SetActive(true);
        }

        if (current_asset_text.Contains("Tire"))
        {
            tire.SetActive(true);
        }

        if (current_asset_text.Contains("Wrench"))
        {
            wrench.SetActive(true);
        }

        if (current_asset_text.Contains("Wheel_Wedge"))
        {
            wheel_wedge.SetActive(true);
        }

        if (current_asset_text.Contains("Rover_No_Tire"))
        {
            rover_no_tire.SetActive(true);
        }
        if (current_asset_text.Contains("Rover_Cap_Removed"))
        {
            rover_cap_removed.SetActive(true);
        }
        if (current_asset_text.Contains("Rover_Broken_Tire"))
        {
            rover_broken_tire.SetActive(true);
        }
        if (current_asset_text.Contains("Cap"))
        {
            cap.SetActive(true);
        }


    }

    //Go Forward: go forward one task item
    public static int go_forward(TextMesh Instructions_Text, int current)
    {
        
        current = current + 1;
        update_instructions(current);
        return current;
    }

    //Go Backward: go backwards one task item
    public static int go_backward(TextMesh Instructions_Text, int current)
    {

        current = current - 1;
        update_instructions(current);
        return current;
    }

}
