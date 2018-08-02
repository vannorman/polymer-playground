using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wedge : MonoBehaviour {

	public Transform maleAttach;
	public Transform femaleAttach;
	public GameObject attachProtector;
	public Transform attacherDestinationFinal;
	public Transform attacherDestinationInitial;
	public bool fixedPosition = false; // if true, will never move towards another one, it's stuck to the wall
	float t = 0;
	void Update () {
		if (fixedPosition)
			return;

		float attachSpeed = 1f;
		float rotSpeed = 60f;
		switch (mode) {
		case AttachMode.Protected:
			break;
		case AttachMode.ReadyToReceive:
			break;
		case AttachMode.Receiving:
			break;
		case AttachMode.ReadyToAttach:
			t -= Time.deltaTime;
			if (t < 0) {
				t = Random.Range(0.3f,0.4f);
				foreach (Wedge w in FindObjectsOfType<Wedge>()) {
					if (w == this || w.mode != AttachMode.ReadyToReceive)
						continue;
					float d = (w.femaleAttach.position - maleAttach.position).sqrMagnitude;
					if (d < 1.7f) {
						InitAttach (this, w);
						return;
					}
				}
			}
			break;
		case AttachMode.Initial:
			transform.position = Vector3.MoveTowards (transform.position, attachTarget.attacherDestinationInitial.position, Time.deltaTime * attachSpeed);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, attachTarget.attacherDestinationInitial.rotation, Time.deltaTime * rotSpeed);
//			float d1 = (transform.position - attachTarget.attacherDestinationInitial.position).sqrMagnitude;
//			if (d1 < 0.05f) {

//			}
			float a1 = Quaternion.Angle (transform.rotation, attachTarget.attacherDestinationInitial.rotation);
			if (a1 < 1) {
				SetMode (AttachMode.Final); //
			}
//			Debug.Log ("mode:" + mode + ", d1:" + d1);
			break;
				

		case AttachMode.Final:
			transform.position = Vector3.MoveTowards (transform.position, attachTarget.attacherDestinationFinal.position, Time.deltaTime * attachSpeed);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, attachTarget.attacherDestinationFinal.rotation, Time.deltaTime * rotSpeed);
//			float d2 = (transform.position - attachTarget.attacherDestinationFinal.position).sqrMagnitude;
//			if (d2 < 0.05f) {

//			}
//			Debug.Log ("mode:" + mode + ", d2:" + d2);
			float a2 = Quaternion.Angle (transform.rotation, attachTarget.attacherDestinationFinal.rotation);
			if (a2 < 1) {
				transform.position = attachTarget.attacherDestinationFinal.position;
				transform.SetParent (attachTarget.transform);
				SetMode (AttachMode.Complete);
			}
			break;
		case AttachMode.Complete:
			SetMode (AttachMode.Protected);
//			Debug.Log ("mode"+mode);

			break;
		}
//			transform.position = Vector3.MoveTowards(

	}

	public void SetMode(AttachMode newMode){
//		Debug.Log ("set mode;" + newMode);
		mode = newMode;
	}

	Wedge attachTarget;
	void InitAttach(Wedge m, Wedge f){
//		Debug.Log ("init attach m:" + m.gameObject.name + ", f:" + f.gameObject.name + ". mode:" + m.mode);
		if (m.mode != AttachMode.ReadyToAttach || f.mode != AttachMode.ReadyToReceive) {
			return;
		}

		f.SetMode (AttachMode.Receiving);
		m.SetMode (AttachMode.Initial);
		m.attachTarget = f;

		if (GetComponent<ConstantForce> ()) {
			Destroy (GetComponent<ConstantForce> ());
		}

		if (GetComponent<Rigidbody> ()) {
			Destroy (GetComponent<Rigidbody> ());
		}

		if (GetComponent<SphereCollider> ()) {
			Destroy(GetComponent<SphereCollider>());
		}
		if (m.GetComponent<TimedObjectDestructor> ()) {
			
			Destroy (m.GetComponent<TimedObjectDestructor> ());
		}
		if (f.GetComponent<TimedObjectDestructor> ()) {
			Destroy (f.GetComponent<TimedObjectDestructor> ());
		}
		// orient and attract the male to the female until a connection is made

	}

	public enum AttachMode {
		Protected,
		ReadyToReceive,
		ReadyToAttach,
		Initial,
		Final,
		Complete,
		Receiving
	}

	public AttachMode mode = AttachMode.Protected;

	// triggered when an agent hits it
	public void EnableAttach(WedgeProtector wp, Vector3 vel){
		Destroy (wp);
		SetMode (AttachMode.ReadyToReceive);
		attachProtector.transform.SetParent (null);
		Rigidbody rb = attachProtector.AddComponent<Rigidbody> ();
		rb.AddForce (vel * 50);
		rb.useGravity = false;
		rb.AddTorque (Random.onUnitSphere * 50);
		attachProtector.AddComponent<SphereCollider> ();
		TimedObjectDestructor tod =  attachProtector.AddComponent<TimedObjectDestructor>();
		tod.DestroyNow (6);
	}

}
