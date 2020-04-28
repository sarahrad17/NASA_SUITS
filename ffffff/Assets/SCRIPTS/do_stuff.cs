using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit.OpenVR.Headers;


//model.transform.rotation = Quaternion.Euler(rotation);

public class do_stuff : MonoBehaviour
{
    //standard reference values
    public static float[] reference_pos = new[] { 0f, 0f, 0f };
    public static float[] reference_rot = new[] { 0f, 0f, 0f };
    public static float[] reference_sca = new[] { 1f, 1f, 1f };

    //current reference values
    public static float[] curr_reference_pos = new[] { 0f, 0f, 0f };
    public static float[] curr_reference_rot = new[] { 0f, 0f, 0f };
    public static float[] curr_reference_sca = new[] { .75f, .75f, .75f };

    static public do_stuff instance;
    public static float speed = 10f;

    //scale vectors based upon a reference model
    public static Vector3[] Scale_Vectors(float[] start_pos, float[] start_rot, float[] start_sca)
    {
        float[] pos = new float[3];
        float[] rot = new float[3];
        float[] sca = new float[3];
        Vector3[] v = new Vector3[3];


        pos[0] = (curr_reference_sca[0] * start_pos[0]) / reference_sca[0];
        pos[1] = (curr_reference_sca[1] * start_pos[1]) / reference_sca[1];
        pos[2] = (curr_reference_sca[2] * start_pos[2]) / reference_sca[2];

        pos[0] = pos[0] + (curr_reference_pos[0] - reference_pos[0]);
        pos[1] = pos[1] + (curr_reference_pos[1] - reference_pos[1]);
        pos[2] = pos[2] + (curr_reference_pos[2] - reference_pos[2]);

        v[0] = new Vector3(pos[0], pos[1], pos[2]);

        rot[0] = (curr_reference_sca[0] * start_rot[0]) / reference_sca[0];
        rot[1] = (curr_reference_sca[1] * start_rot[1]) / reference_sca[1];
        rot[2] = (curr_reference_sca[2] * start_rot[2]) / reference_sca[2];

        rot[0] = rot[0] + (curr_reference_rot[0] - reference_pos[0]);
        rot[1] = rot[1] + (curr_reference_rot[1] - reference_rot[1]);
        rot[2] = rot[2] + (curr_reference_rot[2] - reference_rot[2]);

        v[1] = new Vector3(rot[0], rot[1], rot[2]);

        sca[0] = (curr_reference_sca[0] * start_sca[0]) / reference_sca[0];
        sca[1] = (curr_reference_sca[1] * start_sca[1]) / reference_sca[1];
        sca[2] = (curr_reference_sca[2] * start_sca[2]) / reference_sca[2];

        v[2] = new Vector3(Convert.ToSingle(sca[0]), Convert.ToSingle(sca[1]), Convert.ToSingle(sca[2]));

        return v;
    }


    public static void Move_Overview(GameObject model, float[] pos, float[] rot, float[] sca, float[] pos_move, float[] rot_move, float[] sca_move, GameObject model2, float[] pos2, float[] rot2, float[] sca2, float[] pos_move2, float[] rot_move2, float[] sca_move2, GameObject model3, float[] pos3, float[] rot3, float[] sca3, float[] pos_move3, float[] rot_move3, float[] sca_move3)
    //public static void Move_Overview(GameObject model, float[] pos, float[] rot, float[] sca, float[] pos_move, float[] rot_move, float[] sca_move)
    {

        //if all 3 models moving 
        if ((model != null) && (model2 != null) && (model3 != null))
        {
            //scale start vectors
            Vector3[] v = Scale_Vectors(pos, rot, sca);
            Vector3[] v2 = Scale_Vectors(pos2, rot2, sca2);
            Vector3[] v3 = Scale_Vectors(pos3, rot3, sca3);
            //set models to scale
            model.transform.position = v[0];
            model.transform.rotation = Quaternion.Euler(v[1]);
            model.transform.localScale = v[2];
            model2.transform.position = v2[0];
            model2.transform.rotation = Quaternion.Euler(v2[1]);
            model2.transform.localScale = v2[2];
            model3.transform.position = v3[0];
            model3.transform.rotation = Quaternion.Euler(v3[1]);
            model3.transform.localScale = v3[2];
            //scale end vectors 
            Vector3[] v_end = Scale_Vectors(pos_move, rot_move, sca_move);
            Vector3[] v_end2 = Scale_Vectors(pos_move2, rot_move2, sca_move2);
            Vector3[] v_end3 = Scale_Vectors(pos_move3, rot_move3, sca_move3);
            //start coroutine
            var tempgameObject = new GameObject();
            do_stuff d = tempgameObject.AddComponent<do_stuff>();
            d.StartCoroutine(Move_Model3(model, v_end[0], v_end[1], v_end[2], model2, v_end2[0], v_end2[1], v_end[2], model3, v_end3[0], v_end3[1], v_end3[2]));


        }
        //else if 2 models moving 
        else if ((model != null) && (model2 != null) && (model3 == null))
        {
            //scale start vectors
            Vector3[] v = Scale_Vectors(pos, rot, sca);
            Vector3[] v2 = Scale_Vectors(pos2, rot2, sca2);
            //set models to scale
            model.transform.position = v[0];
            model.transform.rotation = Quaternion.Euler(v[1]);
            model.transform.localScale = v[2];
            model2.transform.position = v2[0];
            model2.transform.rotation = Quaternion.Euler(v2[1]);
            model2.transform.localScale = v2[2];
            //scale end vectors 
            Vector3[] v_end = Scale_Vectors(pos_move, rot_move, sca_move);
            Vector3[] v_end2 = Scale_Vectors(pos_move2, rot_move2, sca_move2);
            //start coroutine
            var tempgameObject = new GameObject();
            do_stuff d = tempgameObject.AddComponent<do_stuff>();
            d.StartCoroutine(Move_Model2(model, v_end[0], v_end[1], v_end[2], model2, v_end2[0], v_end2[1], v_end[2]));
        }
        
        //else if 1 model moving
        else if ((model != null) && (model2 == null) && (model3 == null))
        {
            //scale start vectors
            Vector3[] v = Scale_Vectors(pos, rot, sca);
            //set models to scale
            model.SetActive(true);
            model.transform.position = v[0];
            //model.transform.position = new Vector3(0, 0, 0);

            model.transform.rotation = Quaternion.Euler(v[1]);

            model.transform.localScale = v[2];
            //scale end vectors 
            Vector3[] v_end = Scale_Vectors(pos_move, rot_move, sca_move);
            //start coroutine
            var tempgameObject = new GameObject();
            do_stuff d = tempgameObject.AddComponent<do_stuff>();
            d.StartCoroutine(Move_Model(model, v_end[0], v_end[1], v_end[2]));

        }

    }







    //move model
    static IEnumerator Move_Model(GameObject model, Vector3 pos_move, Vector3 rot_move, Vector3 sca_move)
    {
        //TIMING
        float step = 1f / (speed); // How much to step by per second
        float endTime = Time.time + (speed); // When to end the coroutine
        float t = 0; // how far we are. 0-1
        //POSITION
        var fromPos = model.transform.position;
        var targetPos = pos_move;
        //ROTATION
        Vector3 rotAmount = rot_move;
        var fromAngle = model.transform.eulerAngles; // start rotation
        var targetRot = rot_move; // where we want to be at the end
        //SCALE
        var localScale = model.transform.localScale;
        var targetScale = sca_move;

        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            //transform position
            if (pos_move.x != 0 || pos_move.y != 0 || pos_move.z != 0)
            {
                model.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            }
            //transform rotation
            if (rot_move.x != 0 || rot_move.y != 0 || rot_move.z != 0)
            {
                model.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            }
            //transform scale
            if (sca_move.x != 0 || sca_move.y != 0 || sca_move.z != 0)
            {
                model.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            }
            else
            {
                print("this is bad");
            }
            yield return 0;
        }
    }


    static IEnumerator Move_Model2(GameObject model, Vector3 pos_move, Vector3 rot_move, Vector3 sca_move, GameObject model2, Vector3 pos_move2, Vector3 rot_move2, Vector3 sca_move2)
    {
        //TIMING
        float step = 1f / (speed); // How much to step by per second
        float endTime = Time.time + (speed); // When to end the coroutine
        float t = 0; // how far we are. 0-1
        //MODEL 1
        //POSITION
        var fromPos = model.transform.position;
        var targetPos = model.transform.position + pos_move;
        //ROTATION
        Vector3 rotAmount = rot_move;
        var fromAngle = model.transform.eulerAngles; // start rotation
        var targetRot = model.transform.eulerAngles + rotAmount; // where we want to be at the end
        //SCALE
        var localScale = model.transform.localScale;
        var targetScale = model.transform.localScale + sca_move;

        //MODEL2
        //POSITION
        var fromPos2 = model2.transform.position;
        var targetPos2 = model2.transform.position + pos_move2;
        //ROTATION
        Vector3 rotAmount2 = rot_move2;
        var fromAngle2 = model2.transform.eulerAngles; // start rotation
        var targetRot2 = model2.transform.eulerAngles + rotAmount2; // where we want to be at the end
        //SCALE
        var localScale2 = model2.transform.localScale;
        var targetScale2 = model2.transform.localScale + sca_move2;


        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            //MODEL1
            //transform position
            if (pos_move.x != 0 || pos_move.y != 0 || pos_move.z != 0)
            {
                model.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            }
            //transform rotation
            if (rot_move.x != 0 || rot_move.y != 0 || rot_move.z != 0)
            {
                model.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            }
            //transform scale
            if (sca_move.x != 0 || sca_move.y != 0 || sca_move.z != 0)
            {
                model.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            }
            //MODEL2
            //transform position
            if (pos_move2.x != 0 || pos_move2.y != 0 || pos_move2.z != 0)
            {
                model2.transform.position = Vector3.Lerp(fromPos2, targetPos2, t);
            }
            //transform rotation
            if (rot_move2.x != 0 || rot_move2.y != 0 || rot_move2.z != 0)
            {
                model2.transform.eulerAngles = Vector3.Lerp(fromAngle2, targetRot2, t);
            }
            //transform scale
            if (sca_move2.x != 0 || sca_move2.y != 0 || sca_move2.z != 0)
            {
                model2.transform.localScale = Vector3.Lerp(localScale2, targetScale2, t);
            }
            yield return 0;
        }
    }
    static IEnumerator Move_Model3(GameObject model, Vector3 pos_move, Vector3 rot_move, Vector3 sca_move, GameObject model2, Vector3 pos_move2, Vector3 rot_move2, Vector3 sca_move2, GameObject model3, Vector3 pos_move3, Vector3 rot_move3, Vector3 sca_move3)
    {
        //TIMING
        float step = 1f / (speed); // How much to step by per second
        float endTime = Time.time + (speed); // When to end the coroutine
        float t = 0; // how far we are. 0-1
        //MODEL 1
        //POSITION
        var fromPos = model.transform.position;
        var targetPos = model.transform.position + pos_move;
        //ROTATION
        Vector3 rotAmount = rot_move;
        var fromAngle = model.transform.eulerAngles; // start rotation
        var targetRot = model.transform.eulerAngles + rotAmount; // where we want to be at the end
        //SCALE
        var localScale = model.transform.localScale;
        var targetScale = model.transform.localScale + sca_move;

        //MODEL2
        //POSITION
        var fromPos2 = model2.transform.position;
        var targetPos2 = model2.transform.position + pos_move2;
        //ROTATION
        Vector3 rotAmount2 = rot_move2;
        var fromAngle2 = model2.transform.eulerAngles; // start rotation
        var targetRot2 = model2.transform.eulerAngles + rotAmount2; // where we want to be at the end
        //SCALE
        var localScale2 = model2.transform.localScale;
        var targetScale2 = model2.transform.localScale + sca_move2;

        //MODEL 3
        //POSITION
        var fromPos3 = model3.transform.position;
        var targetPos3 = model3.transform.position + pos_move3;
        //ROTATION
        Vector3 rotAmount3 = rot_move3;
        var fromAngle3 = model3.transform.eulerAngles; // start rotation
        var targetRot3 = model3.transform.eulerAngles + rotAmount3; // where we want to be at the end
        //SCALE
        var localScale3 = model3.transform.localScale;
        var targetScale3 = model3.transform.localScale + sca_move3;

        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            //MODEL1
            //transform position
            if (pos_move.x != 0 || pos_move.y != 0 || pos_move.z != 0)
            {
                model.transform.position = Vector3.Lerp(fromPos, targetPos, t);
            }
            //transform rotation
            if (rot_move.x != 0 || rot_move.y != 0 || rot_move.z != 0)
            {
                model.transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            }
            //transform scale
            if (sca_move.x != 0 || sca_move.y != 0 || sca_move.z != 0)
            {
                model.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            }
            //MODEL2
            //transform position
            if (pos_move2.x != 0 || pos_move2.y != 0 || pos_move2.z != 0)
            {
                model2.transform.position = Vector3.Lerp(fromPos2, targetPos2, t);
            }
            //transform rotation
            if (rot_move2.x != 0 || rot_move2.y != 0 || rot_move2.z != 0)
            {
                model2.transform.eulerAngles = Vector3.Lerp(fromAngle2, targetRot2, t);
            }
            //transform scale
            if (sca_move2.x != 0 || sca_move2.y != 0 || sca_move2.z != 0)
            {
                model2.transform.localScale = Vector3.Lerp(localScale2, targetScale2, t);
            }

            //MODEL3
            //transform position
            if (pos_move3.x != 0 || pos_move3.y != 0 || pos_move3.z != 0)
            {
                model3.transform.position = Vector3.Lerp(fromPos3, targetPos3, t);
            }
            //transform rotation
            if (rot_move3.x != 0 || rot_move3.y != 0 || rot_move3.z != 0)
            {
                model3.transform.eulerAngles = Vector3.Lerp(fromAngle3, targetRot3, t);
            }
            //transform scale
            if (sca_move3.x != 0 || sca_move3.y != 0 || sca_move3.z != 0)
            {
                model3.transform.localScale = Vector3.Lerp(localScale3, targetScale3, t);
            }
            yield return 0;
        }
    }


}
