using UnityEngine;
using System.Collections;

public class SinTransparency : MonoBehaviour {

	
	public float frequency = 1;
	public float amplitude = 1;
	// Use this for initialization
	float offset=0;
	void Start () {
		offset=Random.Range(0.1f,4f); 
	}
	
	// Update is called once per frame
	void Update () {
		float alphaColor = 0.5f * amplitude * Mathf.Sin((offset+Time.time)*frequency);
//		Debug.Log("alphacolor:"+alphaColor);
		Color newColor = new Color(gameObject.GetComponent<Renderer>().material.color.r,gameObject.GetComponent<Renderer>().material.color.g,gameObject.GetComponent<Renderer>().material.color.b,alphaColor);
		GetComponent<Renderer>().material.color=newColor;// SetColor("_MainTex",newColor);
	}
}
