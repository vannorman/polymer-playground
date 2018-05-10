using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollider : MonoBehaviour
{
	public ParticleSystem part;
	public List<ParticleCollisionEvent> collisionEvents;

	void Start()
	{
		part = GetComponent<ParticleSystem>();
		collisionEvents = new List<ParticleCollisionEvent>();
	}

	public void Update(){
//		if (Input.GetKeyDown (KeyCode.C)) {
//			part.enableEmission = !part.enableEmission;
//		}
	}

	public void TurnOnForSeconds(){
		StopAllCoroutines ();
		StartCoroutine (TurnOnForSecondsE ());
	}

	IEnumerator TurnOnForSecondsE(){
		part.enableEmission = true;
		yield return new WaitForSeconds (1.8f);
		part.enableEmission = false;
	}
	void OnParticleCollision(GameObject other)
	{
		int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
//		Debug.Log ("hit;" + other.name);
		WedgeProtector wp = other.GetComponent<WedgeProtector> ();
		if (wp) {
			wp.parentWedge.EnableAttach (wp,collisionEvents[0].velocity);

		}
////		Rigidbody rb = other.GetComponent<Rigidbody>();
//		int i = 0;
//
//		while (i < numCollisionEvents)
//		{
//			
////			if (rb)
////			{
////				Vector3 pos = collisionEvents[i].intersection;
////				Vector3 force = collisionEvents[i].velocity * 10;
////				rb.AddForce(force);
////
////			}
//			i++;
//		}
	}
}