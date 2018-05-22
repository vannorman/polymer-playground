using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudParticles : MonoBehaviour {

	public ParticleSystem ps;
	List<Vector3> verts = new List<Vector3>();
	float minSize = 5;
	float maxSize = 50;
	// Use this for initialization
	void Start () {
		Init();
	}
	public void Init(){
		ps.Clear();
		verts.Clear();
		foreach(Transform t in transform){
			if (t.GetComponent<MeshFilter>()){
				foreach(Vector3 p in t.GetComponent<MeshFilter>().mesh.vertices){
					Vector3 pp = t.TransformPoint(p);
	//				// commented Debug.Log("vert p:"+p+", world:"+pp);
					verts.Add(pp);
	//				if (Random.Range(0,100) > 95) EmitOnce();
				}
			}
		}
//		for (int i=0;i<200;i++){
//			EmitOnce();
//		}
	}
	
	// Update is called once per frame
	float timer = 0;
	void Update () {
		timer -= Time.unscaledDeltaTime;
		if (timer < 0){
			timer = Random.Range(.1f,.2f);
			EmitOnce();

		}
	}

//	Dictionary<Particle,bool> movedParticles = new Dictionary<Particle,bool>();
//	List<Particle> touchedParticles = new List<Particle>();
	void EmitOnce(){
		ps.transform.position = verts[Random.Range(0,verts.Count-1)];
		ps.Emit(1);
//		ParticleSystem.EmitParams pars = new ParticleSystem.EmitParams();
////		ParticleSystem.EmitParams pars2 =
//		pars.position = verts[Random.Range(0,verts.Count)];
//		pars.startSize = Random.Range(30,40);
//		pars.startLifetime = Random.Range(30,40);
//		pars.startColor = Color.white;
//


//		// commented Debug.Log("emitting. particles:"+ps.particleCount);
////		ParticleSystem.EmitParams pars = new ParticleSystem.EmitParams();
//		List<Particle> toRemove = new List<Particle>();
////		foreach(KeyValuePair<Particle,bool> kvp in movedParticles){
////			if (kvp.Key == null){
////				toRemove.Add(kvp.Key);
////			}
////		}
////		foreach(Particle p in toRemove){
////			movedParticles.Remove(p);
////		}
////		toRemove.Clear();
////		foreach(Particle p in ps.GetParticles()){
////			if (movedParticles.ContainsKey(p)){
////				if (movedParticles[p] == false){
////					p.position = verts[Random.Range(0,verts.Count)];
////					movedParticles[p] = true;
////				}
////			}
////		}
//		foreach(Particle p in touchedParticles){
//			if (p == null) toRemove.Add(p);
//		}
//		foreach(Particle p in toRemove){
//			touchedParticles.Remove(p);
//		}
//		foreach(Particle p in ps.GetParticles()){
//			if (touchedParticles.Contains(p)) continue;
//			else {
//				touchedParticles.Add(p);
//				p.position = verts[Random.Range(0,verts.Count)];
//			}
//		}
//		//			pars.startSize = Random.Range(minSize,maxSize);
//		ps.Emit(1);
	}
}
