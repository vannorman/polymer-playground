using UnityEngine;
using System.Collections;

public class HideMeshAtDistance : MonoBehaviour {

//	// Update is called once per frame
//	float t = 0;
//	public int cutoffDist = 250;
//	float sqrCutoffDist; // 150 sqr
//
//
//	void Start () {
//		sqrCutoffDist = Mathf.Pow(cutoffDist,2);
//	}
//
//	bool hiding = false;
//	void Update () {
//		t -= Time.deltaTime;
//		if (t < 0){
//			t = Random.Range(1f,2f);
//			if (LevelBuilder.inst.levelBuilderIsShowing){
//				Show();
//				return;
//			}
//			float sqrDistToPlayer = Vector3.SqrMagnitude(Player.inst.transform.position-transform.position);
////			// commented Debug.Log("sqrdisttoplayer:"+sqrDistToPlayer);
//			if (sqrDistToPlayer > sqrCutoffDist){
//				if (!hiding) {
//					if (!FramerateTester.inst.testing){
//						Hide();
//					}
//
//				}
//			} else {
//				if (hiding){
//					Show();
//				}
//			}
//		}
//	}
//
//	void Hide(){
//		hiding = true;
//		foreach(Renderer r in GetComponentsInChildren<Renderer>()){
////			// commented Debug.Log("deactive;"+r);
//			r.enabled = false;
//		}
//	}
//
//	public void Show(){
//		hiding = false;
//		foreach(Renderer r in GetComponentsInChildren<Renderer>()){
////			// commented Debug.Log("Active;"+r);
//			r.enabled = true;
//		}
////		// commented Debug.Log("hiding false");
//	}
}
