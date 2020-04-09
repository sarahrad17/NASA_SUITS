using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System;
using System.IO;

public class prioritize_telemetry : MonoBehaviour
{
    public TextMesh t_battery_text;
    public TextMesh t_oxygen_text;
    public TextMesh t_water_text;
    public TextMesh emergency_warning_text;

    public static bool heart_bpm_bool;
    public static bool p_suit_bool;
    public static bool p_sub_bool;
    public static bool t_sub_bool;
    public static bool v_fan_bool;
    public static bool p_o2_bool;
    public static bool rate_o2_bool;
    public static bool cap_battery_bool;
    public static bool p_h2o_g_bool;
    public static bool p_h2o_l_bool;
    public static bool p_sop_bool;
    public static bool rate_sop_bool;
    public static bool t_battery_bool;
    public static bool t_oxygen_bool;
    public static bool t_water_bool;

    public bool yes = true;

    public Canvas warning_flash;

    // Start is called before the first frame update
    void Start()
    {
        emergency_warning_text.text = "";
        StartCoroutine(Update_Telemetry());

    }

    // Enumerator
    IEnumerator Update_Telemetry()
    {
        yield return new WaitForSeconds(5);
        
        while (yes == true)
        {
            StartCoroutine(Flash_Screen());
            yield return new WaitForSeconds(3);

            //check values
            emergency_warning_text.text = "";
            heart_bpm_bool = false;
            p_suit_bool = false;
            p_sub_bool = false;
            t_sub_bool = false;
            v_fan_bool = false;
            p_o2_bool = false;
            rate_o2_bool = false;
            cap_battery_bool = false;
            p_h2o_g_bool = false;
            p_h2o_l_bool = false;
            p_sop_bool = false;
            rate_sop_bool = false;
            t_battery_bool = false;
            t_oxygen_bool = false;
            t_water_bool = false;


    //HEART_BPM
    
            if ((sort_telemetry.heart_bpm_value < 40) || (sort_telemetry.heart_bpm_value > 140))
            {
                heart_bpm_bool = true;
                Debug.Log("HEART : " + sort_telemetry.heart_bpm_value);
            }

            
            //P_SUIT
            if ((sort_telemetry.p_suit_value < 2.0) || (sort_telemetry.p_suit_value > 4.0))
            {
                p_suit_bool = true;
                Debug.Log("PSUIT" + sort_telemetry.p_suit_value);
            }

            //P_SUB
            if ((sort_telemetry.p_sub_value < 2.0) || (sort_telemetry.p_sub_value > 4.0))
            {
                p_sub_bool = true;
                Debug.Log("P_SUB" + sort_telemetry.p_sub_value);
            }

            //T_SUB
            if ((sort_telemetry.t_sub_value < 0) || (sort_telemetry.t_sub_value > 80))
            {
                t_sub_bool = true;
                Debug.Log("T_SUB" + sort_telemetry.t_sub_value);
            }

            //V_FAN 
            if (sort_telemetry.v_fan_value > 40000)
            {
                v_fan_bool = true;
                Debug.Log("V_FAN" + sort_telemetry.v_fan_value);
            }

            //P_O2
            if ((sort_telemetry.p_o2_value < 750) || (sort_telemetry.p_o2_value > 950))
            {
                p_o2_bool = true;
                Debug.Log("P_O2" + sort_telemetry.p_o2_value);
            }

            //RATE_O2
            if ((sort_telemetry.rate_o2_value < .5) || (sort_telemetry.rate_o2_value > 1))
            {
                rate_o2_bool = true;
                Debug.Log("RATEO2" + sort_telemetry.rate_o2_value);
            }

            //CAP_BATTERY
            if ((sort_telemetry.cap_battery_value < 0) || (sort_telemetry.cap_battery_value > 30))
            {
                cap_battery_bool = true;
                Debug.Log("CAPBAT" + sort_telemetry.cap_battery_value);
            }

            //P_H2O_G
            if ((sort_telemetry.p_h2o_g_value < 14) || (sort_telemetry.p_h2o_g_value > 16))
            {
                p_h2o_g_bool = true;
                Debug.Log("P_H2O_G" + sort_telemetry.p_h2o_g_value);
            }

            //P_H2O_L
            if ((sort_telemetry.p_h2o_l_value < 14) || (sort_telemetry.p_h2o_l_value > 16))
            {
                p_h2o_l_bool = true;
                Debug.Log("P_H2O_L" + sort_telemetry.p_h2o_l_value);
            }

            //P_SOP
            if ((sort_telemetry.p_sop_value < 750) || (sort_telemetry.p_sop_value > 950))
            {
                p_sop_bool = true;
                Debug.Log("P_SOP" + sort_telemetry.p_sop_value);
            }

            //RATE_SOP
            if ((sort_telemetry.rate_sop_value < .5) || (sort_telemetry.rate_sop_value > 1))
            {
                rate_sop_bool = true;
                Debug.Log("RATE_SOP" + sort_telemetry.rate_sop_value);
            }

            

            //T_BATTERY
            //show always 10:59:59
            t_battery_text.text = sort_telemetry.t_battery_value;
            string bat_string = t_battery_text.text;
            int battery_find_hours = bat_string.IndexOf(":");
            string battery_hours_str = bat_string.Substring(0, battery_find_hours);
            int battery_hours = Int32.Parse(battery_hours_str, CultureInfo.InvariantCulture);
            //Debug.Log("BAT HOURS: "+battery_hours);
            //if less than hour remaining
            if (battery_hours < 10)
            {
                t_battery_text.color = Color.red;
                emergency_warning_text.text = emergency_warning_text.text + "BATTERY TIME LOW\n";
                Flash_Screen();
            }
            

            //T_OXYGEN
            //show always 10:59:59
            t_oxygen_text.text = sort_telemetry.t_oxygen_value;

            /*
            int oxygen_find_hours = sort_telemetry.t_oxygen_value.IndexOf(":");
            string oxygen_hours_str = sort_telemetry.t_oxygen_value.Substring(0, oxygen_find_hours);
            int oxygen_hours = Int32.Parse(oxygen_hours_str, CultureInfo.InvariantCulture);
            //if less than hour remaining
            if (oxygen_hours < 1)
            {
                t_oxygen_text.color = Color.red;
                emergency_warning_text.text = emergency_warning_text.text + "OXYGEN TIME LOW\n";
                //Flash_Screen();

            }
            */

            //T_WATER
            //show always 10:59:59
            t_water_text.text = sort_telemetry.t_water_value;
            /*
            int water_find_hours = sort_telemetry.t_water_value.IndexOf(":");
            string water_hours_str = sort_telemetry.t_water_value.Substring(0, water_find_hours);
            int water_hours = Int32.Parse(water_hours_str, CultureInfo.InvariantCulture);
            //if less than hour remaining
            if (water_hours < 1)
            {
                t_water_text.color = Color.red;
                emergency_warning_text.text = emergency_warning_text.text + "WATER TIME LOW\n";
                //Flash_Screen();

            }
            */

            //display emergency signals
            if (heart_bpm_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "HEART BPM WARNING\n";
            }
            
            if (p_suit_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "SUIT PRESSURE WARNING\n";
            }
            if (p_sub_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "ENVIRONMENT PRESSURE WARNING\n";
            }
            if (t_sub_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "ENVIRONMENT TEMPERATURE WARNING\n";
            }
            if (v_fan_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "COOLING FAN WARNING\n";
            }
            if (p_o2_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "OXYGEN PACK PRESSURE WARNING\n";
            }
            if (rate_o2_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "OXYGEN PACK RATE WARNING\n";
            }
            if (cap_battery_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "BATTERY CAPACITY WARNING\n";
            }
            if (p_h2o_g_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "H2O GAS PRESSURE WARNING\n";
            }
            if (p_h2o_l_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "H2O LIQUID PRESSURE WARNING\n";
            }
            if (p_sop_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "SECONDARY OXYGEN PACK PRESSURE WARNING\n";
            }
            if (rate_sop_bool)
            {
                emergency_warning_text.text = emergency_warning_text.text + "SECONDARY OXYGEN PACK FLOWRATE WARNING\n";
            }
            
        }

        
    }
    
    IEnumerator Flash_Screen()
    {
        Debug.Log("YEET");
        int yeet = 0;
        while (yeet < 3)
        {
            
            yield return new WaitForSeconds(.5f);
            Image img = GameObject.Find("warning_flash").GetComponent<Image>();
            img.color = new Color(1, 0, 0, .5f);
            yeet = yeet + 1;
        }
       
    }
    
}