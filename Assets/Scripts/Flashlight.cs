using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

	public GameObject lightCone;
    public GameObject switchParent;
    public Transform switchOffRot;
    public Transform switchOnRot;

	public void TurnOn(){
		
		lightCone.SetActive (true);
        switchParent.transform.rotation = switchOnRot.rotation;
	}
	public void TurnOff(){
		lightCone.SetActive (false);
        switchParent.transform.rotation = switchOffRot.rotation;
	}

	public void ToggleState(bool flag){
		
		if (flag)
			TurnOn ();
		else
			TurnOff ();
	}
}
