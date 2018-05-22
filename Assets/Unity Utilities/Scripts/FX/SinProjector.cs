using UnityEngine;
using System.Collections;

public class SinProjector : MonoBehaviour {


	public Color low;
	public Color high;
	public float interval = 1f;
	float timer=0;
	Material mat;

	void Start(){
//		// commented Debug.Log("star");
		mat = GetComponent<Projector>().material;
		timer=interval;
	}

	float nowTime = 0;
	float dTime = .01f;
	void Update () {
//		// commented Debug.Log("upd");

		timer -= Time.unscaledDeltaTime;
		if (timer < 0) timer = interval;

		if (timer > interval/2f){
			LerpColor(low);
		} else {
			LerpColor(high);
		}


		GetComponent<Projector>().material = mat;

	}

	void LerpColor(Color to){
//		// commented Debug.Log("lerping:" +to+", mat color:"+mat.color);
//		if (!mat.HasProperty(s)) return;
		Color c = Color.Lerp(mat.color,to,Time.unscaledDeltaTime/interval);
		mat.color = c;
	}
}
