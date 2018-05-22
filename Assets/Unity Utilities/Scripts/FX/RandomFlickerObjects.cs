using UnityEngine;
using System.Collections;

public class RandomFlickerObjects : MonoBehaviour {

	// Randomly flickers on and off (like a street light)

	public Transform[] flickerObjects;

	float t = 0;
	float t2 = 0;
	bool flickering = false;
	float flickerInterval = 5;
	int flickerTimes = 0;
	void Update () {
		t -= Time.deltaTime;
//		// commented Debug.Log("t:"+t+", flickering;"+flickering+",flckertimes:"+flickerTimes);
		if (t < 0){
			flickerInterval = Random.Range(40f,205f);
			t = flickerInterval;
			flickering = true;
			flickerTimes = Random.Range(2,10);
		}
		if (flickering){
			t2 -= Time.deltaTime;
			if (t2 < 0){
				t2 = Random.Range(0,.2f);
				ToggleOnOff();
				flickerTimes --;
			}
			if (flickerTimes < 0){
				flickering = false;
				TurnOn();
			}
		}
	}

	void ToggleOnOff(){
		bool f = flickerObjects[0].GetComponent<MeshRenderer>().enabled;
		foreach(Transform t in flickerObjects){
			t.GetComponent<MeshRenderer>().enabled = !f;
		}
		AudioSource a = GetComponent<AudioSource>();
		if (a){
			 a.Play();

		}
	}

	void TurnOn(){
		foreach(Transform t in flickerObjects){
			t.GetComponent<MeshRenderer>().enabled = true;
		}
	}
}
