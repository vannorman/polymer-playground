using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Twist {}

public class TwistController : MonoBehaviour {


    public PolymerTwist pt;
	bool twisting = false;
	bool reverse = false;
	float twistAmount = 0f;
	public float speed = 0.20f;
	float maxTwists = 15f;
	float minTwists = 0.5f;
    public LightRotor lightRotor;
    public Flashlight flashlight;
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown (KeyCode.A)) {
		//	if (!twisting) {
		//		TurnOnMotor ();
		//	} else {
		//		TurnOffMotor ();
		//	}
		//}
		if (Input.GetKeyDown (KeyCode.R)) {
			reverse = !reverse;	
		}

        if (Input.GetKeyDown(KeyCode.F))
        {
            pt.Flip();
        }


		if (twisting) {
			int reverseInt = reverse ? -1 : 1;
			twistAmount = Mathf.Min(maxTwists,Mathf.Max(minTwists,twistAmount + Time.deltaTime * speed * reverseInt));
			pt.Spiral (twistAmount);	
            if (lightRotor) {
                lightRotor.transform.localRotation = Quaternion.Euler (0, twistAmount % 15 * 360, 0);
            }
		}

	}

	public void TurnOnMotor(){
		twisting = true;
        if (flashlight){
            
            flashlight.TurnOn ();
        }
	}

	public void TurnOffMotor(){
		twisting = false;
        if (flashlight){
            
            flashlight.TurnOff ();
        }
	}
}
