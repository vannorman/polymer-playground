using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Twist {}

public class TwistController : MonoBehaviour {



	bool twisting = false;
	bool reverse = false;
	float twistAmount = 0f;
	float speed = 0.33f;
	float maxTwists = 15f;
	float minTwists = 0.5f;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			if (!twisting) {
				TurnOnMotor ();
			} else {
				TurnOffMotor ();
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			reverse = !reverse;	
		}


		if (twisting) {
			int reverseInt = reverse ? -1 : 1;
			twistAmount = Mathf.Min(maxTwists,Mathf.Max(minTwists,twistAmount + Time.deltaTime * speed * reverseInt));
			PolymerTwist.inst.Spiral (twistAmount);	
			FindObjectOfType<LightRotor> ().transform.localRotation = Quaternion.Euler (0, twistAmount % 15 * 360, 0);
		}

	}

	void TurnOnMotor(){
		twisting = true;
		FindObjectOfType<Flashlight> ().TurnOn ();
	}

	void TurnOffMotor(){
		twisting = false;
		FindObjectOfType<Flashlight> ().TurnOff ();
	}
}
