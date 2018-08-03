using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFollowSplineSpeed : VideoSequenceObject {

    public float newSpeed;
    public bool speeding = false;
    FollowSpline fs;

    void Start() { 
        fs = FindObjectOfType<FollowSpline>();
    }

    public override void Fire(){
        //Debug.Log("fire!");
        foreach (ChangeFollowSplineSpeed cfs in FindObjectsOfType<ChangeFollowSplineSpeed>()) {
            cfs.speeding = false; // clumsy way to avoid multiple instances competing for the same spline speed set
        }
        speeding = true;

    }

    void Update() {
        if (speeding)
        {
            float changeSpeedSpeed = 2f;
            fs.speed = Mathf.Lerp(fs.speed, newSpeed, Time.deltaTime * changeSpeedSpeed);
            if (Mathf.Abs(fs.speed - newSpeed) < .01f)
            {
                //speeding = false;
            }
            //Debug.Log("speeding. fs speed:" + fs.speed);
        }
        else { 
            //Debug.Log("not "+newSpeed+"; fs speed:" + fs.speed);
        }
    }
}
