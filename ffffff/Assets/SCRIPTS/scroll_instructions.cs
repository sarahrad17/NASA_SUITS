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
        cap.SetActive(false);

        
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

}
