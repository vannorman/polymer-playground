using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SplineHelper))]
[ExecuteInEditMode]
public class SplineHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SplineHelper myScript = (SplineHelper)target;
        if(GUILayout.Button("Build Spline"))
        {
            myScript.BuildSpline();
        }
    }

    void Update(){
        Debug.Log("updating.");
    }
//
//    void OnSceneGUI()
//    {
//        SplineHelper myScript = (SplineHelper)target;
//
//        Event e = Event.current;
//        switch (e.type)
//        {
//            case EventType.keyDown:
//                {
//                    Debug.Log("key");
//                    if (Event.current.keyCode == (KeyCode.T))
//                    {
//                        myScript.curvyness += 0.5f;
//                    }
//                    else if (Event.current.keyCode == (KeyCode.Y))
//                    {
//                        myScript.curvyness -= 0.5f;
//                    }
//                }
//                break;
//            default:
//                break;
//        }
//    }


}
