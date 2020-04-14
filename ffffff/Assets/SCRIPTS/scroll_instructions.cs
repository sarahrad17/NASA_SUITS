using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_instructions : MonoBehaviour
{
    public static bool instructions_open;
    public static string[] instructions_arr;
    public static string[] instructions_models_arr;
    //public static int current;
    public static string current_text = "";
    public static string current_asset_text = "";
    

    // Start is called before the first frame update
    void Start()
    {
        instructions_open = false;
        set_up();
    }

    // Open: opens instruction pad
    public static void open(GameObject Instructions, TextMesh Instructions_Text, int current, GameObject rover, GameObject jackscrew)
    { 
        if (instructions_open == false)
        {
            update_instructions(current);
            Instructions_Text.text = current_text;
            Instructions.SetActive(true);
            rover.SetActive(true);
            jackscrew.SetActive(true);
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
        set_up();
        current_text = instructions_arr[current];
        current_asset_text = instructions_models_arr[current];
    }

    //Go Forward: go forward one task item
    public static int go_forward(TextMesh Instructions_Text, int current)
    {
        Instructions_Text.text = instructions_arr[current + 1];
        current = current + 1;
        return current;
    }

    //Go Backward: go backwards one task item
    public static int go_backward(TextMesh Instructions_Text, int current)
    {
        Instructions_Text.text = instructions_arr[current - 1];
        current = current - 1;
        return current;
    }

}
