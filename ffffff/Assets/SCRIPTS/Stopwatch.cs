using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using JetBrains.Annotations;

public class Stopwatch : MonoBehaviour
{
    public bool timer_on;

    //create a timer
    void Start() 
    {
        Stopwatch timer = new Stopwatch();
        timer_on = false;
    }

    //use timer
    void Update()
    {
        
    }


}
