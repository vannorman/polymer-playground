using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
//using System.Linq;
//using System;

#if UNITY_EDITOR

using UnityEditor;
#endif

//[CustomEditor (typeof(HexGrid))]

#if UNITY_EDITOR_OSX

public class MMTools : Editor {

    //	[MenuItem("Edit/Editor Tools/Rename Children")]
    //	public static void CombineChildren(){
    //		foreach(GameObject o in Selection.gameObjects){
    //			o.GetComponent<CombineChildren>().
    //		}
    //	}


    //	[MenuItem("Molecules/Spiral")]
    //	public static void Spiral()
    //	{
    //		Vector3[] pts = Utils2.Spiral ();
    //		for(int i=0;i<pts.Length;i++){
    //			GameObject s = GameObject.CreatePrimitive (PrimitiveType.Sphere);
    //			s.name = "debugg";
    //			s.transform.position = pts [i];
    //		}
    //	}


    //[MenuItem("Molecules/Pop Sequencer")]
    //public static void PopSequencer()
    //{
    //    //Selection.activeGameObject = FindObjectOfType<VideoSequencer>();
    //    //VideoSequencer vs = FindObjectOfType<VideoSequencer>(); //Selection.activeGameObject.GetComponent<VideoSequencer>();
    //    //vs.actions = new List<VideoSequencer.Action> (); //Dictionary<float, VideoSequenceObject>()[vs.transform.childCount];
    //    //float totalTime = 0;
    //    //for (int i = 0; i < vs.transform.childCount; i++){
    //    //    if (vs.transform.GetChild(i).gameObject.activeSelf)
    //    //    {
    //    //        VideoSequencer.Action act = new VideoSequencer.Action();
    //    //        act.actionObj = vs.transform.GetChild(i).GetComponent<VideoSequenceObject>();
    //    //        act.timeToFire = totalTime;
    //    //        //Debug.Log("act dur;" + act.actionObj.duration);
    //    //        totalTime += act.actionObj.duration;
    //    //        vs.actions.Add(act);
    //    //    }
    //    //}
    //}


	[MenuItem("Molecules/Extend #e")]
	public static void Extend()
	{
		// Save selection
		// Duplicate it
		// Select dupe
		// parent to orig
		// position at Vector3(-13,769,513)
		// rotation -52, 0, rand(360)
		GameObject sel = Selection.activeGameObject;
		GameObject dupe = (GameObject)Instantiate (sel);
		dupe.transform.SetParent (sel.transform);
		dupe.transform.localPosition = new Vector3 (0, 650, 418);
		dupe.transform.localRotation = Quaternion.Euler (-52, 0, Random.Range (0, 360));
		dupe.transform.localScale = Vector3.one;
		Selection.activeObject = dupe;

	}

    [MenuItem("Molecules/Build Membrane")]
    public static void BuildMembrane()
    {
        foreach (Membrane m in FindObjectsOfType<Membrane>())
        {
            m.BuildNow();
        }
    }


    [MenuItem("Molecules/Build Ring")]
    public static void BuildRing()
    {
        FindObjectOfType<Ring>().BuildNow();
    }

    [MenuItem("Molecules/Stick To Surface Above")]
    public static void StickToSurfaceAbove()
    {
        foreach (GameObject o in Selection.gameObjects)
        {
            RaycastHit h = new RaycastHit();
            Ray ray = new Ray(o.transform.position, Vector3.up);
            if (Physics.Raycast(ray, out h))
            {
                o.transform.position = h.point;
            }
        }
        FindObjectOfType<Ring>().BuildNow();
    }

	
	










}
#endif




