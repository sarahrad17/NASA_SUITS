using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prioritize_telemetry : MonoBehaviour
{
    public Text t_battery_text;
    public Text t_oxygen_text;
    public Text t_water_text;
    public Text emergency_warning_text;

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

    void Start()
    {
        bool yes = true;
        StartCoroutine(Update_Telemetry());
    }

    // Update is called once per frame
    IEnumerator Update_Telemetry()
    {
        while (yes == true)
        {
            yield return new WaitForSeconds(3);

            //check values
            emergency_warning_text = "";

            //HEART_BPM
            if ((sort_telemetry.heart_bpm_value < 40) || (sort_telemetry.heart_bpm_value > 140))
            {
                heart_bpm_bool = true;
            }

            //P_SUIT
            if ((sort_telemetry.p_suit_value < 2.0) || (sort_telemetry.p_suit_value > 4.0))
            {
                p_suit_bool = true;
            }

            //P_SUB
            if ((sort_telemetry.p_sub_value < 2.0) || (sort_telemetry.p_sub_value > 4.0))
            {
                p_sub_bool = true;
            }

            //T_SUB
            if ((sort_telemetry.t_sub_value < 0) || (sort_telemetry.t_sub_value > 80))
            {
                t_sub_bool = true;
            }

            //V_FAN 
            if (sort_telemetry.v_fan_value > 40000)
            {
                v_fan_bool = true;
            }

            //P_O2
            if ((sort_telemetry.p_o2_value < 750) || (sort_telemetry.p_o2_value > 950))
            {
                p_o2_bool = true;
            }

            //RATE_O2
            if ((sort_telemetry.rate_o2_value < .5) || (sort_telemetry.rate_o2_value > 1))
            {
                rate_o2_bool = true;
            }

            //CAP_BATTERY
            if ((sort_telemetry.cap_battery_value < 0) || (sort_telemetry.cap_battery_value > 30))
            {
                cap_battery_bool = true;
            }

            //P_H2O_G
            if ((sort_telemetry.p_h2o_g_value < 14) || (sort_telemetry.p_h2o_g_value > 16))
            {
                p_h2o_g_bool = true;
            }

            //P_H2O_L
            if ((sort_telemetry.p_h2o_l_value < 14) || (sort_telemetry.p_h2o_l_value > 16))
            {
                p_h2o_l_bool = true;
            }

            //P_SOP
            if ((sort_telemetry.p_sop_value < 750) || (sort_telemetry.p_sop_value > 950))
            {
                p_sop_bool = true;
            }

            //RATE_SOP
            if ((sort_telemetry.rate_sop_value < .5) || (sort_telemetry.rate_sop_value > 1))
            {
                rate_sop_bool = true;
            }

            //T_BATTERY
            //show always
            t_battery_text = sort_telemetry.t_battery;

            //T_OXYGEN
            //show always
            t_oxygen_text = sort_telemetry.t_oxygen;

            //T_WATER
            //show always
            t_water_text = sort_telemetry.t_water;


            //display emergency signals
            if (heart_bpm_bool)
            {
                emergency_warning_text = emergency_warning_text + "HEART BPM WARNING\n";
            }
            if (p_suit_bool)
            {
                emergency_warning_text = emergency_warning_text + "SUIT PRESSURE WARNING\n";
            }
            if (p_sub_bool)
            {
                emergency_warning_text = emergency_warning_text + "ENVIRONMENT PRESSURE WARNING\n";
            }
            if (t_sub_bool)
            {
                emergency_warning_text = emergency_warning_text + "ENVIRONMENT TEMPERATURE WARNING\n";
            }
            if (v_fan_bool)
            {
                emergency_warning_text = emergency_warning_text + "COOLING FAN WARNING\n";
            }
            if (p_o2_bool)
            {
                emergency_warning_text = emergency_warning_text + "OXYGEN PACK PRESSURE WARNING\n";
            }
            if (rate_o2_bool)
            {
                emergency_warning_text = emergency_warning_text + "OXYGEN PACK RATE WARNING\n";
            }
            if (cap_battery_bool)
            {
                emergency_warning_text = emergency_warning_text + "BATTERY CAPACITY WARNING\n";
            }
            if (p_h2o_g_bool)
            {
                emergency_warning_text = emergency_warning_text + "H2O GAS PRESSURE WARNING\n";
            }
            if (p_h2o_l_bool)
            {
                emergency_warning_text = emergency_warning_text + "H2O LIQUID PRESSURE WARNING\n";
            }
            if (p_sop_bool)
            {
                emergency_warning_text = emergency_warning_text + "SECONDARY OXYGEN PACK PRESSURE WARNING\n";
            }
            if (rate_sop_bool)
            {
                emergency_warning_text = emergency_warning_text + "SECONDARY OXYGEN PACK FLOWRATE WARNING\n";
            }
        }
     

}
}