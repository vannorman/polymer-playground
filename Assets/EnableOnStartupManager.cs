using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnStartupManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (EnableOnStartup en in FindObjectsOfTypeAll(typeof(EnableOnStartup))) {
			en.gameObject.SetActive (true);
		}
	}	
	
	// Update is called once per frame
	void Update () {
		
	}
}
