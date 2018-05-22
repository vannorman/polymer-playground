using UnityEngine;
using System.Collections;

public class SinRotate : MonoBehaviour {

	public Vector3 axisToRotate = Vector3.right;
	public float rotateAmount = 45;
	public float rotationInterval = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	void Update () {
		float currentRotation = rotateAmount * Mathf.Sin(Time.time * (Mathf.PI * 2) / rotationInterval);
		Quaternion rot = transform.localRotation;
		rot.eulerAngles = axisToRotate * currentRotation;
		transform.localRotation = rot;
	}
}
