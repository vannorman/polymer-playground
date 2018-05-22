using UnityEngine;
using System.Collections;

public class SinColor : MonoBehaviour {

	public int materialIndex;
	public float interval = 1;
	public bool changeMain=true;
	public Color low;
	public Color high;
	public bool changeEmission=false;
	public Color lowEmission;
	public Color highEmission;
	public bool changeTint=false;
	public Color lowTint;
	public Color highTint;

	Material[] mats;
	float timer=0;

	void Start(){
		timer=interval;
		mats = GetComponent<Renderer>().materials;
	}

	float nowTime = 0;
	float dTime = .01f;
	void Update () {
		
		timer -= Time.unscaledDeltaTime;
		if (timer < 0) timer = interval;

		if (timer > interval/2f){
			if (changeMain) LerpColor("_Color",low);
			if (changeEmission) LerpColor("_Emission",lowEmission);
			if (changeTint) LerpColor("_TintColor",lowTint);
		} else {
			if (changeMain) LerpColor("_Color",high);
			if (changeEmission) LerpColor("_Emission",highEmission);
			if (changeTint) LerpColor("_TintColor",highTint);
		}


		GetComponent<Renderer>().materials = mats;

	}

	void LerpColor(string s, Color to){
		if (!mats[materialIndex].HasProperty(s)) return;
		Color c = Color.Lerp(mats[materialIndex].GetColor(s),to,Time.unscaledDeltaTime/interval);
		mats[materialIndex].SetColor(s,c);
	}
}
