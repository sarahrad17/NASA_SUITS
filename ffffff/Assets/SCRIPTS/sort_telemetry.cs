using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;


public class sort_telemetry : MonoBehaviour
{
    bool get;
    string telemetry;

    public static int heart_bpm_value;
    public static float p_suit_value;
    public static float p_sub_value;
    public static int t_sub_value;
    public static int v_fan_value;
    public static int p_o2_value;
    public static float rate_o2_value;
    public static int cap_battery_value;
    public static int p_h2o_g_value;
    public static int p_h2o_l_value;
    public static int p_sop_value;
    public static float rate_sop_value;
    public static string t_battery_value;
    public static string t_oxygen_value;
    public static string t_water_value;

    // Start is called before the first frame update

    void Start()
    {
        get = true;
        StartCoroutine(Sort_telem());
    }
    // Update is called once per frame
    IEnumerator Sort_telem()
    {
        //yield return new WaitForSeconds(5);
        while (get == true)
        {
            yield return new WaitForSeconds(3);

            //retrieve telemetry 
            telemetry = File.ReadAllText(@"telemetry.txt");
            int telemetry_len = telemetry.Length;

            //if telemetry not empty
            if (telemetry_len > 0)
            {
                //parse for values:


                //HEART_BPM
                int heart_bpm_start = telemetry.IndexOf("\"heart_bpm\":\"");
                string heart_bpm_substring = telemetry.Substring(heart_bpm_start + 13);
                int heart_bpm_stop = heart_bpm_substring.IndexOf("\"");
                string heart_bpm = telemetry.Substring(heart_bpm_start + 13, heart_bpm_stop);
                heart_bpm_value = Int32.Parse(heart_bpm);
                //Debug.Log("SORT BPM : "+heart_bpm_value);

                //P_SUIT
                int p_suit_start = telemetry.IndexOf("\"p_suit\":\"");
                string p_suit_substring = telemetry.Substring(p_suit_start + 10);
                int p_suit_stop = p_suit_substring.IndexOf("\"");
                string p_suit = telemetry.Substring(p_suit_start + 10, p_suit_stop);
                p_suit_value = float.Parse(p_suit, CultureInfo.InvariantCulture);
                //Debug.Log("AAAAAAAAAAAAAAAAAAAAAA : " + p_suit_value);

                //P_SUB
                int p_sub_start = telemetry.IndexOf("\"p_sub\":\"");
                string p_sub_substring = telemetry.Substring(p_sub_start + 9);
                int p_sub_stop = p_sub_substring.IndexOf("\"");
                string p_sub = telemetry.Substring(p_sub_start + 9, p_sub_stop);
                p_sub_value = float.Parse(p_sub, CultureInfo.InvariantCulture);
                //Debug.Log(p_sub_value);

                //T_SUB
                int t_sub_start = telemetry.IndexOf("\"t_sub\":\"");
                string t_sub_substring = telemetry.Substring(t_sub_start + 9);
                int t_sub_stop = t_sub_substring.IndexOf("\"");
                string t_sub = telemetry.Substring(t_sub_start + 9, t_sub_stop);
                t_sub_value = Int32.Parse(t_sub, CultureInfo.InvariantCulture);
                //Debug.Log(t_sub_value);

                //V_FAN
                int v_fan_start = telemetry.IndexOf("\"v_fan\":\"");
                string v_fan_substring = telemetry.Substring(v_fan_start + 9);
                int v_fan_stop = v_fan_substring.IndexOf("\"");
                string v_fan = telemetry.Substring(v_fan_start + 9, v_fan_stop);
                v_fan_value = Int32.Parse(v_fan, CultureInfo.InvariantCulture);
                //Debug.Log(v_fan_value);

                //P_O2
                int p_o2_start = telemetry.IndexOf("\"p_o2\":\"");
                string p_o2_substring = telemetry.Substring(p_o2_start + 8);
                int p_o2_stop = p_o2_substring.IndexOf("\"");
                string p_o2 = telemetry.Substring(p_o2_start + 8, p_o2_stop);
                p_o2_value = Int32.Parse(p_o2, CultureInfo.InvariantCulture);
                //Debug.Log(p_o2_value);

                //RATE_O2
                int rate_o2_start = telemetry.IndexOf("\"rate_o2\":\"");
                string rate_o2_substring = telemetry.Substring(rate_o2_start +11);
                int rate_o2_stop = rate_o2_substring.IndexOf("\"");
                string rate_o2 = telemetry.Substring(rate_o2_start + 11, rate_o2_stop);
                rate_o2_value = float.Parse(rate_o2, CultureInfo.InvariantCulture);
                //Debug.Log(rate_o2_value);


                //CAP_BATTERY
                int cap_battery_start = telemetry.IndexOf("\"cap_battery\":\"");
                string cap_battery_substring = telemetry.Substring(cap_battery_start + 15);
                int cap_battery_stop = cap_battery_substring.IndexOf("\"");
                string cap_battery = telemetry.Substring(cap_battery_start + 15, cap_battery_stop);
                cap_battery_value = Int32.Parse(cap_battery, CultureInfo.InvariantCulture);
                //Debug.Log(cap_battery_value);

                //P_H2O_G
                int p_h2o_g_start = telemetry.IndexOf("\"p_h2o_g\":\"");
                string p_h2o_g_substring = telemetry.Substring(p_h2o_g_start + 11);
                int p_h2o_g_stop = p_h2o_g_substring.IndexOf("\"");
                string p_h2o_g = telemetry.Substring(p_h2o_g_start + 11, p_h2o_g_stop);
                p_h2o_g_value = Int32.Parse(p_h2o_g, CultureInfo.InvariantCulture);
                //Debug.Log(p_h2o_g_value);

                //P_H2O_L
                int p_h2o_l_start = telemetry.IndexOf("\"p_h2o_l\":\"");
                string p_h2o_l_substring = telemetry.Substring(p_h2o_l_start + 11);
                int p_h2o_l_stop = p_h2o_l_substring.IndexOf("\"");
                string p_h2o_l = telemetry.Substring(p_h2o_l_start + 11, p_h2o_l_stop);
                p_h2o_l_value = Int32.Parse(p_h2o_l, CultureInfo.InvariantCulture);
                //Debug.Log(p_h2o_l_value);

                //P_SOP
                int p_sop_start = telemetry.IndexOf("\"p_sop\":\"");
                string p_sop_substring = telemetry.Substring(p_sop_start + 9);
                int p_sop_stop = p_sop_substring.IndexOf("\"");
                string p_sop = telemetry.Substring(p_sop_start + 9, p_sop_stop);
                p_sop_value = Int32.Parse(p_sop, CultureInfo.InvariantCulture);
                //Debug.Log(p_sop_value);

                //RATE_SOP
                int rate_sop_start = telemetry.IndexOf("\"rate_sop\":\"");
                string rate_sop_substring = telemetry.Substring(rate_sop_start + 12);
                int rate_sop_stop = rate_sop_substring.IndexOf("\"");
                string rate_sop = telemetry.Substring(rate_sop_start + 12, rate_sop_stop);
                rate_sop_value = float.Parse(rate_sop, CultureInfo.InvariantCulture);
                //Debug.Log(rate_sop_value);

                //T_BATTERY
                int t_battery_start = telemetry.IndexOf("\"t_battery\":\"");
                string t_battery_substring = telemetry.Substring(t_battery_start + 13);
                int t_battery_stop = t_battery_substring.IndexOf("\"");
                t_battery_value = telemetry.Substring(t_battery_start + 13, t_battery_stop);
                Debug.Log("BAT:"+t_battery_value);

                //T_OXYGEN
                int t_oxygen_start = telemetry.IndexOf("\"t_oxygen\":\"");
                string t_oxygen_substring = telemetry.Substring(t_oxygen_start + 12);
                int t_oxygen_stop = t_oxygen_substring.IndexOf("\"");
                t_oxygen_value = telemetry.Substring(t_oxygen_start + 12, t_oxygen_stop);
                Debug.Log("OX"+t_oxygen_value);

                //T_WATER
                int t_water_start = telemetry.IndexOf("\"t_water\":\"");
                string t_water_substring = telemetry.Substring(t_water_start + 11);
                int t_water_stop = t_water_substring.IndexOf("\"");
                t_water_value = telemetry.Substring(t_water_start + 11, t_water_stop);
                Debug.Log("WAT"+t_water_value);

            }
        }
        

        
    }


}
