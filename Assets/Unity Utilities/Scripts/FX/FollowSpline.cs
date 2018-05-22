﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpline : MonoBehaviour {

	public float speed = 1;
	public SplineInterpolator spline;
    float avgNodeDist = 0;
    
    public float splineOffset = 0f;
    

    void Start(){
        SetSpline();
        


    }
    public void SetSpline(){
        SetSpline(spline, speed);
    }
    bool following = true;
    float normalizedSpeedForDist = 1;

    public void SetSplinePretime(float preTime){
        t = preTime;
    }



    public void SetSpline(SplineInterpolator s, float sp)
    {
        if (s != null)
        {
            spline = s;
        }
        spline = s;
        speed = sp;

        avgNodeDist = spline.GetAvgNodeDist();
		Debug.Log ("avg node dist:" + avgNodeDist);
        float totalSplineDist = spline.GetTotalDist();
        normalizedSpeedForDist = totalSplineDist / 20f;
        following = true;
        origSpeed = speed;
	}


    float origSpeed = 10f;
	public float t = 0;
    float adjustedTimer = 1;
    
    void Update()
    {


        float curdist = spline.GetCurrentNodeDist(t);
//        float normalizedTime = avgNodeDist / curdist;

		t += Time.deltaTime * speed ;

        
        
		transform.position = spline.GetPositionAtTime(t);
		transform.rotation = spline.GetRotationAtTime (t);
        
	}
}
