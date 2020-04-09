using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prioritize_telemetry : MonoBehaviour
{
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
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //check values

        //HEART_BPM
        if ((sort_telemetry.heart_bpm_value < 40)||(sort_telemetry.heart_bpm_value > 140))
        {

        }
        
        //P_SUIT
        if((sort_telemetry.p_suit_value < 2.0)||(sort_telemetry.p_suit_value > 4.0))
        {

        }

        //P_SUB
        if((sort_telemetry.p_sub_value < 2.0)||(sort_telemetry.p_sub_value > 4.0))
        {

        }

        //T_SUB
        if((sort_telemetry.t_sub_value < 0)||(sort_telemetry.t_sub_value > 80))
        {

        }

        //V_FAN 
        if(sort_telemetry.v_fan_value > 40000)
        {

        }

        //P_O2
        if((sort_telemetry.p_o2_value < 750) || (sort_telemetry.p_o2_value > 950))
        {

        }

        //RATE_O2
        if((sort_telemetry.rate_o2_value < .5)||(sort_telemetry.rate_o2_value > 1))
        {

        }

        //CAP_BATTERY
        if((sort_telemetry.cap_battery_value < 0)||(sort_telemetry.cap_battery_value > 30))
        {

        }

        //P_H2O_G
        if((sort_telemetry.p_h2o_g_value < 14)||(sort_telemetry.p_h2o_g_value > 16))
        {

        }

        //P_H2O_L
        if((sort_telemetry.p_h2o_l_value < 14)||(sort_telemetry.p_h2o_l_value > 16))
        {

        }

        //P_SOP
        if((sort_telemetry.p_sop_value < 750)||(sort_telemetry.p_sop_value > 950))
        {

        }

        //RATE_SOP
        if((sort_telemetry.rate_sop_value < .5)||(sort_telemetry.rate_sop_value> 1))
        {

        }

        //T_BATTERY
        //show always

        //T_OXYGEN
        //show always

        //T_WATER
        //show always


    }
}
