using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

	public GameObject lightCone;
	public void TurnOn(){
		
		lightCone.SetActive (true);
	}
	public void TurnOff(){
		lightCone.SetActive (false);
	}
}
