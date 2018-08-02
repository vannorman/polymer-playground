using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SplineHelper : MonoBehaviour {

    public enum Direction
    {
        Left,
        Right
    }
    public float radiusStart = 0;
    public float radiusIncrease;
    public float stretchFactor;
    public int numNodes;
    public float curvyness = 4f;
    public SplineController spline;
    public Direction direction = Direction.Left;
    List<GameObject> toDestroy = new List<GameObject>();
    Vector3 offset = Vector3.zero;
    public void BuildSpline(){

        float radius = radiusStart;
                   
        foreach (Transform t in spline.SplineRoot.transform)
        {
            toDestroy.Add(t.gameObject);
        }
        Invoke("DestroyChilds", .1f);

        for (int i = 0; i < numNodes; i++)
        {
            Transform t = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            t.name = ("spline node " + i);
            t.SetParent(spline.SplineRoot.transform);
            int dir = direction == Direction.Left ? 1 : -1;
            Vector3 pos = new Vector3(Mathf.Sin(i/curvyness) * radius * dir, Mathf.Cos(i/curvyness) * radius * dir, stretchFactor * i);
            if (i == 0)
            {
                Debug.Log("pos:" + pos);
                offset = -pos;
            }
            t.localPosition = pos + offset;
            radius += radiusIncrease;
        }
    }

   
   
    void DestroyChilds(){
        foreach(GameObject o in toDestroy)
        {
            if (o)
            {

                DestroyImmediate(o);
            }
        }
        toDestroy.Clear();
    }
}
//[UnityEditor.InitializeOnLoad]
//public static class EditorHotkeysTracker
//{
//    static EditorHotkeysTracker()
//    {
//        UnityEditor.SceneView.onSceneGUIDelegate += view =>
//            {
//                var e = Event.current;
//                if (e != null && e.keyCode != KeyCode.None)
//                if (Event.current.keyCode == (KeyCode.T))
//                {
//                    GameObject.FindObjectOfType<SplineHelper>().curvyness += 0.1f;
//                    GameObject.FindObjectOfType<SplineHelper>().BuildSpline();
//
//                }
//                else if (Event.current.keyCode == (KeyCode.Y))
//                {
//                    GameObject.FindObjectOfType<SplineHelper>().curvyness -= 0.1f;
//                    GameObject.FindObjectOfType<SplineHelper>().BuildSpline();
//                }
//            };
//    }
//}
