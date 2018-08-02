using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eOrientationMode { NODE = 0, TANGENT }

[AddComponentMenu("Splines/Spline Controller")]
[RequireComponent(typeof(SplineInterpolator))]
public class SplineController : MonoBehaviour
{
	public GameObject SplineRoot;
	public float Duration = 10;
	public eOrientationMode OrientationMode = eOrientationMode.NODE;
	public eWrapMode WrapMode = eWrapMode.ONCE;
	public bool AutoStart = true;
	public bool AutoClose = true;
	public bool HideOnExecute = true;


	SplineInterpolator mSplineInterp;
	Transform[] mTransforms;
    public static bool drawSplineGizmos = true;

	public Color lineColor = Color.yellow;

	public void ChangeColorGizmos(){
		lineColor = Utils2.RandomColor ();
	}


    #if UNITY_EDITOR
	void OnDrawGizmos()
    {
        if (!drawSplineGizmos)
            return;
        Transform[] trans = GetTransforms();
        if (trans.Length < 2)
            return;

        SplineInterpolator interp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;
        SetupSplineInterpolator(interp, trans);
        interp.StartInterpolation(null, false, WrapMode);


        Vector3 prevPos = trans[0].position;
        for (int c = 1; c <= 100; c++)
        {
            float currTime = c * Duration / 100;
            Vector3 currPos = interp.GetHermiteAtTime(currTime);
//            float mag = (currPos - prevPos).magnitude * 2;
            float camDist = (currPos - UnityEditor.SceneView.GetAllSceneCameras()[0].transform.position).magnitude;
            float mag = (camDist - 75)/20f;
//            redColor *= mag;
			Gizmos.color = lineColor;
            Gizmos.DrawLine(prevPos, currPos);
            prevPos = currPos;

        }

        foreach (Transform t in trans)
        {
            Gizmos.DrawSphere(t.position,.2f);
        }
	}
    #endif

	void Start()
	{

		mSplineInterp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;

		mTransforms = GetTransforms();

		if (HideOnExecute)
			DisableTransforms();

		if (AutoStart)
			FollowSpline();
	}

	public void SetupSplineInterpolator(SplineInterpolator interp, Transform[] trans)
    {
        interp.Reset();

        float step = (AutoClose) ? Duration / trans.Length :
			Duration / (trans.Length - 1);

        int c;


        // Distance between nodes varies, so modify the time per node here. More distant nodes have a higher delta time value vs. the previous node.
//        float totalDist = 0;
//        for (c = 1; c < trans.Length; c++)
//        {
//            totalDist += (trans[c].position - trans[c-1].position).magnitude;
//        }
//        float avgDist = totalDist / (float)trans.Length;
//
//        float normalizedTimeFactor = 1;



        for (c = 0; c < trans.Length; c++)
        {
            if (OrientationMode == eOrientationMode.NODE)
            {
//                if (c > 0)
//                {
//                    float lastDist = (trans[c].position - trans[c-1].position).magnitude;
//
//                    normalizedTimeFactor = lastDist / avgDist;
//                }
//				interp.AddPoint(trans[c].position, trans[c].rotation, step * c * normalizedTimeFactor, new Vector2(0, 1));
                interp.AddPoint(trans[c].position, trans[c].rotation, step * c, new Vector2(0, 1));

			}
			else if (OrientationMode == eOrientationMode.TANGENT)
			{
				Quaternion rot;
				if (c != trans.Length - 1)
					rot = Quaternion.LookRotation(trans[c + 1].position - trans[c].position, trans[c].up);
				else if (AutoClose)
					rot = Quaternion.LookRotation(trans[0].position - trans[c].position, trans[c].up);
				else
					rot = trans[c].rotation;

				interp.AddPoint(trans[c].position, rot, step * c, new Vector2(0, 1));
			}
		}

		if (AutoClose)
			interp.SetAutoCloseMode(step * c);
	}


	/// <summary>
	/// Returns children transforms, sorted by name.
	/// </summary>
	public Transform[] GetTransforms()
	{
		if (SplineRoot != null)
		{
			List<Component> components = new List<Component>(SplineRoot.GetComponentsInChildren(typeof(Transform)));

			List<Transform> transforms = new List<Transform>();
			foreach(Component c in components){
				transforms.Add (c.transform);
			}
//			components.ConvertAll(c => (Transform)c);

			transforms.Remove(SplineRoot.transform);
//			transforms.Sort(delegate(Transform a, Transform b)
//			{
//				return a.name.CompareTo(b.name);
//			});
//			// commented Debug.Log ("transforms: "+transforms[0]);
			return transforms.ToArray();
		}
		// commented Debug.Log ("no");
		return null;
	}

	/// <summary>
	/// Disables the spline objects, we don't need them outside design-time.
	/// </summary>
	void DisableTransforms()
	{
		if (SplineRoot != null)
		{
			foreach(Transform t in SplineRoot.transform){
				t.gameObject.SetActive(false);
			} //SMW_GF.inst.SetActiveRecursively(SplineRoot,false);
		}
	}


	/// <summary>
	/// Starts the interpolation
	/// </summary>
	public void FollowSpline()
	{
		if (mTransforms.Length > 0)
		{
//			gameObject.SendMessage("Init"); //.Init (); // get sibling init
			// commented Debug.Log ("followspline");
			SetupSplineInterpolator(mSplineInterp, mTransforms);
			mSplineInterp.StartInterpolation(null, true, WrapMode);
		}
	}
}