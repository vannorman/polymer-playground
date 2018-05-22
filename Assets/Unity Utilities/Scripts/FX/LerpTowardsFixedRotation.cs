using UnityEngine;
using System.Collections;

public class LerpTowardsFixedRotation : MonoBehaviour {

//	Quaternion startRot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion rot = transform.rotation;
		rot.eulerAngles = new Vector3(0,rot.eulerAngles.y,0);
		float fixSpeed = 1;
		transform.rotation = Quaternion.Slerp(transform.rotation,rot,Time.deltaTime * fixSpeed);
	}
}
