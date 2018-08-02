using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleMover : MonoBehaviour {

	ParticleSystem cachedPs;
	float t = 0;
	ParticleSystem ps {
		get { 
			
			if (t < 0) {
				
//				Debug.Log ("seekin. t:"+t+", cached;"+cachedPs);
				t = 1;


//				Transform []  ts =  FindObjectsOfType<ParticleSystem> ().Select (ps => ps.transform).ToArray ();
//				ts.OrderBy(function(t) { return (this.transform.position - t.position).sqrMagnitude; }).First();
//				Vector3 position = transform.position;
				cachedPs = FindObjectsOfType<ParticleSystem>().OrderBy(o => (o.transform.position - this.transform.position).sqrMagnitude).FirstOrDefault();


//				Utils2.GetClosestObjectOfType<ParticleSystem> (this.transform); // FindObjectsOfType<ParticleSystem>().OrderBy(ps => ps.transform.position).OrderBy(transform.position - ps.transform.position).sqrMagnitude).First();
//				cachedPs = nearest;
			}
			return cachedPs;
		}
	}
	public Transform tube;
	bool moving = false;
	public float speed = 4;
	public float enterSpeed = 4;
	public Transform entrance;
	public GameObject blockers;
	List<ParticleSystem.Particle> enteredParticles = new List<ParticleSystem.Particle>();
	void Update(){
		t -= Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.A)) {
			moving = !moving;
			FindObjectOfType<Flashlight> ().ToggleState (moving);
			blockers.SetActive (!moving);
		}
		if (moving) {

//			foreach (ParticleSystem.Particle p in enteredParticles) {
////				if (p == null)
////					continue;
//				p.velocity = ps.transform.InverseTransformVector(this.transform.forward * speed);
//			}

			foreach (Paddle p in tube.GetComponentsInChildren<Paddle>()) {
				p.transform.Rotate (new Vector3 (90, 0, 0) * Time.deltaTime, Space.Self);
			}

			InitializeIfNeeded();

			// GetParticles is allocation free because we reuse the m_Particles buffer between updates
			int numParticlesAlive = ps.GetParticles(m_Particles);

			// Change only the particles that are alive
			for (int i = 0; i < numParticlesAlive; i++)
			{
//				if (enteredParticles.Contains (m_Particles [i])) {
//					continue;
//				}
				Vector3 pos = ps.transform.TransformPoint( m_Particles [i]. position);
				if (this.GetComponent<Renderer> ().bounds.Contains (pos)) {
					if (pos.z > entrance.position.z) {
						m_Particles [i].velocity = ps.transform.InverseTransformVector (this.transform.forward * speed);
					} else {
						float d = (entrance.position - pos).sqrMagnitude;
						float entranceSpeed = Mathf.Min(10,enterSpeed / d);
						m_Particles [i].velocity = Vector3.Normalize(ps.transform.InverseTransformVector (entrance.position - pos)) * entranceSpeed; //Vector3.MoveTowards (ps.transform.InverseTransformPoint (m_Particles [i].position), entrance.transform.position, Time.deltaTime * entranceSpeed);
					
					}


				}
			}

			// Apply the particle changes to the particle system
			ps.SetParticles(m_Particles, numParticlesAlive);
		}


	}


	ParticleSystem.Particle[] m_Particles;




	void InitializeIfNeeded()
	{
		
		if (m_Particles == null || m_Particles.Length < ps.main.maxParticles)
			m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
	}
}
