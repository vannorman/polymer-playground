using UnityEngine;
using System.Collections;

public class SinGlowEmission : MonoBehaviour {

	// Use this for initialization
	float freq = 2;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float sinColor = 0.5f + Mathf.Sin(Time.time*freq)/2f;
//		Debug.Log ("sin color: "+sinColor);
		GetComponent<Renderer>().material.SetColor("_Emission",new Color(sinColor,sinColor,sinColor,sinColor));
	}	
}
