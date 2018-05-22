using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public bool useGlobalAxis = true;
	public Transform centerPoint;
	public float rotateSpeed=40f;
	public Vector3 dir = new Vector3(0,1,0);
	public bool simpleRotate = false;
	public bool randomRotationOnStart = false;
	public bool continuousRandom = false;
	public float delayStart = 0;
	public bool useUnscaledTime = false;
	// Use this for initialization
	void Start () {
		if (null == centerPoint){
			centerPoint = transform;
		}
		if (randomRotationOnStart){

		}
	
	}


	void RandomizeRotation(){

		Quaternion newRot = transform.rotation;
		newRot.eulerAngles += new Vector3(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360));
//		transform.rotation = newRot;
		if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().AddTorque(newRot.eulerAngles*Random.Range(0,1f));
	}
	
	// Update is called once per frame
	float randTimer=0;
	void Update () {
		float tt = Time.deltaTime;
		if (useUnscaledTime) tt = Mathf.Min(0.02f,Time.unscaledDeltaTime);
		if(delayStart > 0) { delayStart -= tt; return; }
		if (continuousRandom){
			randTimer-=tt;
			if (randTimer < 0){
				randTimer=Random.Range(5,7);
				RandomizeRotation();

			}
		}
		if (!centerPoint) {
			Destroy(this);
		}
		if (!simpleRotate){
			if (!useGlobalAxis) {
				transform.Rotate(dir,rotateSpeed*tt,Space.Self);
//				dir = transform.InverseTransformDirection(dir);
			}
			else transform.RotateAround(centerPoint.position,dir,rotateSpeed*tt);
		 } else {
		 	transform.Rotate(dir);
		 }
		 
		
	}
}
