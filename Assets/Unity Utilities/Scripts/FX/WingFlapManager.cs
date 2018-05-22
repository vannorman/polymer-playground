using UnityEngine;
using System.Collections;

public class WingFlapManager : MonoBehaviour {

	// Use this for initialization
	public Transform leftWing;
	public Transform rightWing;
	public bool flapSound = true;
	public float steadyTime = 3;
	public float frequency=10;
	public bool alwaysFaceUp = false;

	float randomOffset=1;
	void Start () {

		randomOffset = Random.Range(.1f,1f);
		
	}
	
	float timer = 5;
	bool flap=true;
	float amplitude=20;
	// Update is called once per frame
	bool firstTime=true; // for synching

	float flapTimer=.628f;
	void Update () {
		if (alwaysFaceUp) transform.up = Vector3.up;
		timer-=Time.deltaTime;
		
		// make wings not flap sometimes (glide effect)
		if (timer < steadyTime){
			flap=false;
			if (timer <0 ){
				timer = Random.Range(6,12);
				steadyTime = Random.Range(0.5f,1.5f);
				flap=true;
			
			}
			
		}
		
		if (flap){
			flapTimer-=Time.deltaTime;
			if (flapTimer<0){
				flapTimer=.5f;// frequency/(Mathf.PI*2);
				if (flapSound){
//					if (GameObject.FindWithTag("GlobalVars")) GlobalVars.inst.am.PlayWingFlap(transform.position);
//					else flapTimer=10000;
				}
			}

			Quaternion leftRot = Quaternion.identity;
			Quaternion rightRot = Quaternion.identity;
			
			float left = Mathf.Sin((Time.time+randomOffset)*frequency)*amplitude;
			leftRot.eulerAngles = new Vector3(270+left,270,90);
			float right = Mathf.Sin((Time.time+randomOffset+Mathf.PI)*frequency)*amplitude;
			rightRot.eulerAngles = new Vector3(270-right,270,90);

			
			leftWing.localRotation = leftRot;
			rightWing.localRotation=rightRot;
		}
		
		
	}

}
