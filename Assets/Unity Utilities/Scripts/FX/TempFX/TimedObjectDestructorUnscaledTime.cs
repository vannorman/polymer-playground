using UnityEngine;
using System.Collections;

public class TimedObjectDestructorUnscaledTime : MonoBehaviour {
	
	public float autoDestructInSeconds = 2;

	void Update(){
		autoDestructInSeconds -= Mathf.Min(0.02f,Time.unscaledDeltaTime);;

		if (autoDestructInSeconds < 0){
			Destroy(gameObject);
		}
	}
}