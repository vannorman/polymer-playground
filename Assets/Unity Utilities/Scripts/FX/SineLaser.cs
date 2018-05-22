using UnityEngine;
using System.Collections;

public class SineLaser : MonoBehaviour {
	
	LineRenderer opLaser;
	Transform from;
	Transform to;
	Vector3 sineDir;
	
	public void DrawSineLaserFromTo(Transform f, Transform t, Color c, float widthScale=1){
		from = f;
		to = t;
		
		GameObject sineLaser = new GameObject();
		sineLaser.transform.parent=transform;
		sineLaser.name = "sinelaser";
		opLaser = sineLaser.AddComponent<LineRenderer>();
//		// commented Debug.Log ("gameob :"+gameObject.name);
//		// commented Debug.Log ("oplas:" +opLaser);
		opLaser.SetWidth(0.5f*widthScale,.5f*widthScale);
		opLaser.SetVertexCount(1000);
		//		opLaser.renderer.material.color = GlobalVars.inst.poi.opColor;
//		opLaser.material = new Material(Shader.Find("Toon/Lighted Outline"));
//		opLaser.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
		opLaser.material = new Material(Shader.Find("VertexLit"));
		opLaser.material.color = c;
		opLaser.SetColors(c,c);
		
		// get a direction for the amplitude of the sine wave to be increased in (e.g. not a global direction which would "Skew" the sine wave)

//		sineDir = throwaway.transform.up;
//		vector = Quaternion.AngleAxis(-45, Vector3.up) * vector;
		sineDir = Vector3.Normalize(new Vector3(f.position.x-t.position.x,t.position.y-f.position.y,t.position.x-f.position.y));
//		sineDir = Quaternion.LookRotation(Vector3.Normalize(Camera.main.transform.position-transform.position));
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!opLaser || !from || !to || !from.gameObject.activeSelf || !to.gameObject.activeSelf) {
			Destroy (this.gameObject);
			return;
		}
		for (int i=0;i<1000;i++){
//			if (!from || !to) {
//				Destroy (this);
//				return;
//			}
			float mag = Vector3.Distance(from.position,to.position);
			Vector3 dir = Vector3.Normalize(to.position-from.position);
			float sineSpeed = -20;
			float sineAmp = .5f;
			float freq = 200; // inverse ..
			Vector3 sineOffset = sineDir * Mathf.Sin ((Time.time - i/freq) *sineSpeed)*sineAmp;
			Vector3 pos = from.position + dir * (i/1000f)*mag + sineOffset;
			opLaser.SetPosition(i,pos);
		}

	}
}