using UnityEngine;
using System.Collections;

public class MovingLaser : MonoBehaviour {

	LineRenderer opLaser;
	Transform from;
	Transform to;
	Vector3 sineDir;

	public void DrawMovingLaserFromTo(Transform f, Transform t, Color c, float widthScale=1){
		from = f;
		to = t;

		GameObject sineLaser = new GameObject();
		sineLaser.transform.parent=transform;
		sineLaser.name = "sinelaser";
		opLaser = sineLaser.AddComponent<LineRenderer>();
		opLaser.SetWidth(0.6f*widthScale,.2f*widthScale);
		opLaser.SetVertexCount(2);
		opLaser.material = new Material(Shader.Find("VertexLit"));
		opLaser.material.color = c;
		opLaser.SetColors(c,c);
	}

	void Update () {
		if (!opLaser || !from || !to || !from.gameObject.activeSelf || !to.gameObject.activeSelf) {
			Destroy (this.gameObject);
			return;
		}
		opLaser.SetPosition(0,from.position);
		opLaser.SetPosition(1,to.position);
	}
}
