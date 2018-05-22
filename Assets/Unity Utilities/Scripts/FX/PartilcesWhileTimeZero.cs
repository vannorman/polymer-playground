using UnityEngine;
using System.Collections;

public class PartilcesWhileTimeZero : MonoBehaviour {

	ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	float lastTime = 0;
	void Update () {
		if (Time.timeScale == 0){
			
//			float dTime = Mathf.Clamp(Time.realtimeSinceStartup - lastTime,0,0.01f);
//			lastTime = Time.realtimeSinceStartup;
			ps.Simulate(Time.unscaledDeltaTime,true,false);
//			// commented Debug.Log("simulationg");

		}
	}
}
