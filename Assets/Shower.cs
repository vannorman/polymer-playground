using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	bool showering = false;
	// Update is called once per frame
	float t =0;
	public float speed = 100f;
	public float spray = 0.1f;
	public float interval = 0.3f;

	void Update () {
//		if (Input.GetKeyDown (KeyCode.S)) {
//			showering = !showering;
//		}
		if (showering) {
			t -= Time.deltaTime;
			if (t < 0) {
				t = interval;
				GameObject w = Prefabs.wedge;
				Vector3 p = transform.position;
				int count = 40;
				while (Physics.CheckSphere (p,.1f) && count > 0) {
					count--;
					p = transform.position + Random.onUnitSphere;
				}
				if (Physics.CheckSphere (p,.1f))
					return;
				w.transform.position = p;
//				Vector3 bounds = new Vector3 (6, 0.5f, 0.5f);
//				w.transform.position = transform.position + Random.onUnitSphere; // + transform.right * Random.Range (-bounds.x, bounds.x);
				Rigidbody rb = w.AddComponent<Rigidbody> ();
				rb.AddForce((transform.forward + transform.right * Random.Range(-spray,spray) + transform.up*Random.Range(-spray,spray)) * Random.Range(speed/2f,speed*1.5f));
				rb.useGravity = false;
				ConstantForce cf = w.AddComponent<ConstantForce> ();
				cf.force = Vector3.down;
				w.AddComponent<SphereCollider> ();
				TimedObjectDestructor tod = w.AddComponent<TimedObjectDestructor> ();
				tod.DestroyNow (10);
				w.GetComponent<Wedge> ().SetMode (Wedge.AttachMode.ReadyToAttach);
			}
		}
	}


	public void TurnOnForSeconds(){
		StopAllCoroutines ();
		StartCoroutine (TurnOnForSecondsE ());
	}

	IEnumerator TurnOnForSecondsE(){
		showering = true;
		yield return new WaitForSeconds (3);
		showering = false;
	}
}
