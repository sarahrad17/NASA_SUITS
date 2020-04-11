using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mark_location : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        System.IO.File.Create(@"mark_location.txt");
    }
    void OnClick()
    {
        UnityEngine.Vector3 cam_pos = Camera.main.transform.position;
        string cam_pos_string = cam_pos.ToString();
        Debug.Log(cam_pos_string);
        System.IO.File.AppendAllText(@"mark_location.txt", cam_pos_string);
    }

    
}
