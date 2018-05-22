using UnityEngine;
using System.Collections;

public class FadeColor : MonoBehaviour {

	public float fadeTime = 2.4f;
	float startTime;

	// Update is called once per frame
	Material m;
	void Start(){
		startTime = fadeTime;
		m = GetComponent<Renderer>().material;
	}

	void Update () {
		float tt = Mathf.Min(0.02f,Time.unscaledDeltaTime);
		fadeTime -= tt;
		float newA = fadeTime / startTime;
		Color newColor = new Color(m.color.r,m.color.g,m.color.b,newA);
		if (fadeTime < 0){
			newColor = new Color(m.color.r,m.color.g,m.color.b,0);
			Destroy(this);
		}
		m.SetColor("_Color",newColor);

	}
}
