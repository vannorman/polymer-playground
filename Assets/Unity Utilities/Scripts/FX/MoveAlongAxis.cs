using UnityEngine;
using System.Collections;

public class MoveAlongAxis : MonoBehaviour {
	
	// a simple "move" script that is used for moving along Sine waves and possibly platforms.
	public float yieldStartSeconds = 0;
	public bool go=false; // ouch .. should not be public.. but awkward handling of whether or not to start automatcially when this script is added by another script.
	public bool useLocalDir = true;
	public Vector3 dir;
	public float speed;
	public bool autoDestruct=true;
	public float autoDestructInSeconds = 10f;
	public bool increaseSpeedOverTime = false;	
	public float increaseAmount = .1f;
	public float cutoffDistance=0;
	public bool useUnscaledTime = false;
	// Update is called once per frame



	float startPosY;
	void Start(){
		startPosY = transform.position.y;
		if (yieldStartSeconds != 0) {
			StartCoroutine(YieldStart());

		} else go=true;

	}

	IEnumerator YieldStart(){
		yield return new WaitForSeconds(yieldStartSeconds);
		go=true;
	}

	public void StartAfterSeconds(float s){
		go=false;
		yieldStartSeconds = s;
		StartCoroutine(YieldStart());
	}

	void Update () {
		if (go){
			float tt = Time.deltaTime;
			if (useUnscaledTime) tt = Mathf.Min(0.02f,Time.unscaledDeltaTime);
//			// commented Debug.Log("tt:"+tt);
			if (useLocalDir) transform.Translate(dir*speed*tt);
			else transform.Translate(transform.InverseTransformDirection(dir)*speed*tt);
			if (increaseSpeedOverTime) speed+=increaseAmount*tt;
			if (cutoffDistance != 0){
				if (Mathf.Abs(transform.position.y-startPosY)>cutoffDistance){

					Destroy(this); // stop
				}
			}
		}
	}
	
	
}
