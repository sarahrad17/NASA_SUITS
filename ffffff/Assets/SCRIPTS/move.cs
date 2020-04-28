using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public static float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //move model
    public static IEnumerator Move_Model(GameObject model, Vector3 pos_move, Vector3 rot_move, Vector3 sca_move)
    {
        //TIMING
        float step = 1f / (speed * 2); // How much to step by per second
        float endTime = Time.time + (speed / 2); // When to end the coroutine
        float t = 0; // how far we are. 0-1
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
            yield return 0;
        }
    }


    public static IEnumerator Move_Model2(GameObject model, Vector3 pos_move, Vector3 rot_move, Vector3 sca_move, GameObject model2, Vector3 pos_move2, Vector3 rot_move2, Vector3 sca_move2)
    {
        //TIMING
        float step = 1f / (speed * 2); // How much to step by per second
        float endTime = Time.time + (speed / 2); // When to end the coroutine
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
    public static IEnumerator Move_Model3(GameObject model, Vector3 pos_move, Vector3 rot_move, Vector3 sca_move, GameObject model2, Vector3 pos_move2, Vector3 rot_move2, Vector3 sca_move2, GameObject model3, Vector3 pos_move3, Vector3 rot_move3, Vector3 sca_move3)
    {
        //TIMING
        float step = 1f / (speed * 2); // How much to step by per second
        float endTime = Time.time + (speed / 2); // When to end the coroutine
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
