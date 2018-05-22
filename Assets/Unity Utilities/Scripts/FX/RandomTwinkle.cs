using UnityEngine;
using System.Collections;

public class RandomTwinkle : MonoBehaviour {

	void OnEnable(){
		twinkleTimer = Random.Range(2.1f,3.3f);
	}



	float twinkleTimer=0;
	void Update(){
		twinkleTimer-=Time.deltaTime;
		if (twinkleTimer < 2.2f){
			if (twinkleTimer > 1.15f){
//				// commented Debug.Log("up");
				GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity,.6f,Time.deltaTime*1);
			} else {
//				// commented Debug.Log("down");
				GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity,0,Time.deltaTime*2);
			}
		} else {
//			// commented Debug.Log("cool");
		}
		if (twinkleTimer < 0){
			twinkleTimer = Random.Range(5.5f,13f);
		}
	}
}
