using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowSpline))]
public class EditorSpliner : Editor {

    //public GameObject slider;
    //public Transform right;
    //public Transform left;
    float totalSeconds = 10;
    // Update is called once per frame
    float x = 0;
    public override void OnInspectorGUI()
    {

        FollowSpline myTarget = (FollowSpline)target;


        myTarget.speed = EditorGUILayout.FloatField("Speed", myTarget.speed );
        myTarget.speed = EditorGUILayout.FloatField("Speed", myTarget.speed);

        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());


        //slider.transform.position = 
        if (EditorApplication.isPlaying)
        {
            return;
        }
        x = GUILayout.HorizontalSlider(x, 0, 1);
        //Debug.Log("ex:" + x);
        FollowSpline fs = FindObjectOfType<FollowSpline>();
        fs.SetLastPosT(x * totalSeconds);
        fs.SetPositionOnSpline();
	}
}
