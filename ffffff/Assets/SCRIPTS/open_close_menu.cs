using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_close_menu : MonoBehaviour
{
    public bool Menu_Open;
    public GameObject Menu;
    void Start()
    {
        Menu.SetActive(false);
        Menu_Open = false; 
    }
    void Update()
    {
        if (Menu_Open != true)
        {
            Menu.SetActive(false);
        }
        if (Menu_Open != false)
        {
            Menu.SetActive(true);
        }
    }
}
