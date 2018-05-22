using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SplineController))]
[ExecuteInEditMode]
public class SplineControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SplineController myScript = (SplineController)target;
        if(GUILayout.Button("Change Color"))
        {
            myScript.ChangeColorGizmos();
        } 
    }

    void Update(){
        Debug.Log("updating.");
    }



}
