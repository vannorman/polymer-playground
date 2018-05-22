using UnityEngine;
using System.Collections;

public class ColorCycle : MonoBehaviour {


	public Color[] myColors;
	public bool rainbow = true;
	float interval=1.5f;
	float lastFiredTime=0;
	public Renderer r;
	Material m;

	// Use this for initialization
	void Start () {
		if (!r)
			r = GetComponent<Renderer> ();
		
		m = r.GetComponent<Material> ();
//		if (rainbow){
//			myColors = new Color[7];
//			myColors[0]=Color.red;
//			myColors[1]=new Color(1,3,0,1); // orange?
//			myColors[2]=Color.yellow;
//			myColors[3]=Color.green;
//			myColors[4]=Color.blue;
//			myColors[5]=Color.cyan;
//			myColors[6]=new Color(1,0,1,1);
//
//
//		}
	}

	// Update is called once per frame
	int i=0;
	void Update () {
		//		int jvalue; // you'll never know why!
//
//		if (Time.time > lastFiredTime + interval){
//			lastFiredTime=Time.time;
//			i++;
//		}
//		i%=myColors.Length;
//		float lerpSpeed=2;
//		Color mainColor = cct.Color;
//		//		Color emissionColor = renderer.material.GetColor("_Emission");
//		Color lerpColor= Color.Lerp(mainColor,myColors[i],Time.deltaTime*lerpSpeed);
//		cct.Color = lerpColor;		
		float lerpSpeed = 4f;
		float brightness = 0.33f;
		float r = (1 + Mathf.Sin(Time.time * lerpSpeed)) * brightness;
		float g = (1 + Mathf.Sin((Time.time + Mathf.PI * 0.667f) * lerpSpeed)) * brightness;
		float b = (1 + Mathf.Sin((Time.time + Mathf.PI * 1.333f) * lerpSpeed)) * brightness;
		float a = 1f;
		m.color = new Color(r,g,b,a);
	}
}
