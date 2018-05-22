using UnityEngine;
using System.Collections;

public class ParticlesBasedOnSpeed : MonoBehaviour {

	RecordPosition rp;
	ParticleSystem ps;
	public float particlesPerSpeed = 1;
	public float minSpeedForParticles = 5;
	public float minEmissionRate = 10;
	void Start(){
		if (!gameObject.GetComponent<RecordPosition>()){
			gameObject.AddComponent<RecordPosition>();
		}
		rp = gameObject.GetComponent<RecordPosition>();
		ps = gameObject.GetComponent<ParticleSystem>();
	}

	void Update(){
		float nowSpeed = Vector3.Magnitude(rp.nowPosition - rp.lastPosition);
		if (nowSpeed / Time.deltaTime > minSpeedForParticles){
			float numParticles = nowSpeed * particlesPerSpeed;
			ps.emissionRate = minEmissionRate + numParticles;
		} else {
			ps.emissionRate = minEmissionRate;
		}
	}


}
