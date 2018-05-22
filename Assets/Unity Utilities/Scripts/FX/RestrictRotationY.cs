using UnityEngine;
using System.Collections;

public class RestrictRotationY : MonoBehaviour {

	public float minY;
	public float maxY;
	Quaternion startRot;

	// Use this for initialization
	void Start () {
		startRot = transform.localRotation;
	}
	

	void LateUpdate () {
		Quaternion rot = transform.localRotation;
		float y = rot.eulerAngles.y%360;
		if (y > 180) y -= 360;
		rot.eulerAngles = new Vector3(rot.eulerAngles.x,Mathf.Clamp(y,minY,maxY),rot.eulerAngles.z);
		transform.localRotation = rot;
//		// commented Debug.Log("rot eulr y:"+rot.eulerAngles.y);
	}
}
