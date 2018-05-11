using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folding : MonoBehaviour {

	[System.Serializable]
	public class Group {
		public Transform pivot;
		public Transform max;
		public Transform min;
		public Transform target;
		public bool folding = false;
		public float maxC = 60;
		public float minC = 0;
		public float targetC = 0;
	}

	[SerializeField]
	public Group[] groups;


	public void FoldIn(){
//		Debug.Log ("foldin");
		foreach (Group g in groups) {
			g.target = g.min;
			g.targetC = g.minC;
		}
	}


	public void FoldOut(){
		foreach (Group g in groups) {
			g.target = g.max;
			g.targetC = g.maxC;
		}
	}

	void Update(){
//		if (Input.GetKeyDown (KeyCode.A)) {
//			if (Input.GetKey (KeyCode.LeftShift)) {
//				FoldOut ();
//			} else {
//				FoldIn ();
//			}
//		}
		foreach (Group g in groups) {
//				if (g.target == null)
//					continue;
//			float delta = Vector3.Angle (g.pivot.transform.forward, g.target.transform.forward);
//			Debug.Log ("Delt:" + delta);
//			if (delta > 1f) {
//				g.folding = true;
//			} else {
//				g.folding = false;
//				return;
//			}
//			if (g.folding) {
			float rotSpeed = 0.8f;
			Quaternion targetRot = Quaternion.Euler (0, g.targetC, 0);
			g.pivot.rotation = Quaternion.Slerp (g.pivot.rotation, targetRot, Time.deltaTime * rotSpeed);
//			g.pivot.transform.rotation =  Quaternion.Slerp (g.pivot.transform.rotation, g.target.transform.rotation, Time.deltaTime * rotSpeed);
//				Quaternion.RotateTowards/
//				g.pivot.transform.forward = Vector3.MoveTowards(g.pivot.transform.forward, g.target.transform.forward, Time.deltaTime * rotSpeed);
//			}
		
		}
	}
}
