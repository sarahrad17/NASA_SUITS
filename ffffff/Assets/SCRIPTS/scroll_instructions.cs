using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_instructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public static void Scroll()
    {
        string[] arr = access_database.instruction_array;
        print(string.Join("\n", arr));
    }
}
