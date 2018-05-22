using UnityEngine;
using System.Collections;

public class FlipRotateOverTime : MonoBehaviour {

	
	// Update is called once per frame
	float timer = 7f;
	void Update () {
		timer-= Time.deltaTime;
		if (timer <= 0){
			timer = 7f;
			transform.Rotate(0,0,180);
		}
	}
}
