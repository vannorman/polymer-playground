using UnityEngine;
using System.Collections;

public class ParticlesOnBounce : MonoBehaviour {


//	public GameObject particles;
//	GameObject parts;
	// Use this for initialization
	void Start () {
//		parts = (GameObject)GameObject.Instantiate(particles, Vector3.zero, Quaternion.identity);
//		parts.GetComponentInChildren<ParticleSystem>()
	}
	
	// Update is called once per frame
	float vel=0;
	float timer = 0;
	float throwTimeout = 0;
	void Update () {
		throwTimeout -= Time.deltaTime;
		if (throwTimeout > 0) {
			return;
		}
		timer -= Time.deltaTime;
		if (timer < 0){
			if (!GetComponent<Rigidbody>()) {
				Destroy (this);
				return;
			}
			timer = Random.Range (0.1f,0.5f);
			vel = GetComponent<Rigidbody>().velocity.magnitude;
			if (Grounded()){
				if (GoingFastEnoughToDust()){
					BounceParticle(transform.position + Vector3.down * transform.localScale.x/2f);
				}
			} 
		}
	}

	bool GoingFastEnoughToDust(){
		if (vel > 10){
			float randomChance = Random.Range (-20,vel);
			if (randomChance > 0){
				return true;

			}
		}
		return false;
	}

	bool Grounded(){
		float maxDistToGroundForDust = transform.localScale.x/2f+1f;
		if (Physics.Raycast(transform.position,Vector3.down,maxDistToGroundForDust)){
			return true;
		}
		return false;
	}
	
	float minCollision = 1;
	Quaternion lastRotation = new Quaternion();
	Vector3 lastForward;
	float totalDegRotated=0;
	void OnCollisionEnter(Collision c) {

//		if (!parts) return; 
		if (!GetComponent<Rigidbody>()) {
			Destroy (this);
			return;
		}
//		parts.transform.position=c.contacts[0].point;
//		parts.transform.rotation= Quaternion.LookRotation(c.contacts[0].normal);
		float bounceAmount = 0;
		if (c.contacts.Length > 0){
			bounceAmount = Mathf.Abs(Vector3.Dot(c.relativeVelocity.normalized, c.contacts[0].normal)) * c.relativeVelocity.magnitude;
		}
//		float vel = GetComponent<Rigidbody>().velocity.magnitude;
		if(bounceAmount > minCollision) {
			//parts.particleSystem.startColor = new Color(1,1,1,Mathf.Min(c.relativeVelocity.magnitude * 0.01f,1));
			int particleCount = (int)Mathf.Min(c.relativeVelocity.magnitude / 3.0f, 10);
			for(int i=0;i<particleCount;i++){
				float upOffset = Random.Range(.1f,1f);
				BounceParticle(c.contacts[0].point+Vector3.up*upOffset);
			}
		} else {

		}

//		// commented Debug.Log ("vel :"+vel);
//		// commented Debug.Log ("total deg rot:"+totalDegRotated);
	}

	void BounceParticle(Vector3 pos){

		Vector3 speed = Vector3.zero;
		float size = Random.Range (1.5f,3.2f);
		float lifetime = 1;
//		EffectsManager.inst.EmitParticlesForBounce(pos,speed,size,lifetime,Color.white);
		//				// commented Debug.Log ("particles here? I am:"+name+", my location:"+transform.position);
	}

	void OnPlayerThrow(){
//		// commented Debug.Log("on player throw");
		throwTimeout = .5f;
	}
}
