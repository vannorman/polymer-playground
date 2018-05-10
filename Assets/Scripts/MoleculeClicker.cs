using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleculeClicker : MonoBehaviour {

	public Image crosshair;
	public static MoleculeClicker inst;
	public void Start(){
		inst = this;
	}
	Collider selected;
	Vector3 screenPoint {
		get {
			Vector3 v = Camera.main.WorldToScreenPoint (selected.transform.position);
			//			Debug.Log ("V:" + v);
			return v;
		}
	}


	// Update is called once per frame
	Vector2 anchoredPosition = Vector2.zero;
	Vector2 startDragPos = Vector2.zero;
	Quaternion rotOnClick;
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			if (null == MoleculeConstructor.inst.molecule) {
//				Debug.Log ("root null, discontinue");
				return;

			}
			startDragPos = Input.mousePosition;
			rotOnClick = MoleculeConstructor.inst.molecule.atom.transform.rotation;
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
				
				Atom a = hit.collider.GetComponent<Atom> ();
				if (a) {
//					Debug.Log ("hit;" + a);
					ClickMolecule (a.gameObject);
					 
				} else {
//					Debug.Log ("hit:" + hit.collider);
				}
			}
		}
		crosshair.GetComponent<RectTransform> ().anchoredPosition = anchoredPosition;


		if (Input.GetMouseButton (0)) {
			if ( null != MoleculeConstructor.inst.molecule) {
				Vector2 delta = (Vector2)Input.mousePosition - startDragPos;
				MoleculeConstructor.inst.molecule.atom.transform.rotation = Quaternion.Euler (rotOnClick.eulerAngles + new Vector3 (delta.y, delta.x, 0));
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {

				Atom a = hit.collider.GetComponent<Atom> ();
				if (a) {
					MoleculeConstructor.inst.DestroyMolecule (a.gameObject);
				}
			}

		}
	}

	public void ClickMolecule(GameObject a){
		selected = a.GetComponent<Collider>();

		anchoredPosition = new Vector2 (screenPoint.x - Screen.width / 2, -Screen.height / 2 + screenPoint.y); // new Vector2((screenPoint.x - Screen.width/2f)/2f,(-screenPoint.z - Screen.height/2f)/2f);
		// Update the position of the popup every frame, in case camera was moving, so that the popup stays centered on the item.

		crosshair.GetComponent<SinPop> ().Pop ();
		MoleculeConstructor.inst.SelectMolecule (a);
	}
}
