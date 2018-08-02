using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour {

	// This is the popup gameobject which will be shown when you click on an item

	// Notes:
	// Screen space goes from 0 - width, 0 -height, 0,0 from bottom left
	// canvas space goes from -width/2 to width/2, -height/2 to height/2, with bottom left being -w/2 , -h/2
	// Convert from screen to canvas
	//void Start(){
	//	Initialize ();
	//	Hide ();
	//}


	public static ItemPopup inst;
	public void Initialize(){
		inst = this;
	}

	public Image fromBottomLeft;
	public Image fromBottomRight;
	public Image fromTopRight;
	public Image fromTopLeft;
	public RectTransform group;

	public enum PopupType{
		AllDirections,
		AboveOnly
	}

	public PopupType popupType;

	Vector3 worldHitPos;
	Vector3 screenPoint {
		get {
			Vector3 v = Camera.main.WorldToScreenPoint (worldHitPos);
			return v; //Camera.main.WorldToScreenPoint (worldHitPos);
		}
	}

	public Text infoText;
	Vector2 anchoredPosition;
	Vector2 offset = Vector2.zero; // Which direction (quadrant) will the popup be shown from the click?
	public void Show(Vector3 p, string text){
		timeout = 7f;
		infoText.text = text;
		this.gameObject.SetActive (true);
		worldHitPos = p;
		SetPopupPosition (); // make sure posiiton is set this frame.
		SetOffsetPosition();
	}


	float timeout = 0f;
	float offsetTimeout = 0f;
	void Update() {
		SetPopupPosition ();
		timeout -= Time.deltaTime;
		offsetTimeout -= Time.deltaTime;
		if (timeout < 0) {
			Hide ();
		}
	}

	Vector2 anchoredPositionFromScreenPoint {
		get {
			return new Vector2(screenPoint.x - Screen.width /2, -Screen.height/2 + screenPoint.y); // new Vector2((screenPoint.x - Screen.width/2f)/2f,(-screenPoint.z - Screen.height/2f)/2f);
		}
	}


	void SetPopupPosition() {		
		// Update the position of the popup every frame, in case camera was moving, so that the popup stays centered on the item.
		GetComponent<RectTransform> ().anchoredPosition = anchoredPositionFromScreenPoint;

	}

	public int ow = 100;
	public int oh = 100;
	void SetOffsetPosition(){
		
		if (offsetTimeout > 0) {
			return;
		}
		offsetTimeout = 1f; // prevent rapid switching.
		Vector2 mp = anchoredPositionFromScreenPoint;
		fromTopRight.gameObject.SetActive(false);
		fromTopLeft.gameObject.SetActive(false);
		fromBottomLeft.gameObject.SetActive(false);
		fromBottomRight.gameObject.SetActive(false);

		switch (popupType) {
		case PopupType.AllDirections:
			// Wrong values! Need to swap up/down/left/right
//			if (mp.x > 0 && mp.y > 0) {
//				// Top right
//				fromTopRight.gameObject.SetActive (true);
//				offset = new Vector2 (-ow, -oh);
//			} else if (mp.x < 0 && mp.y > 0) {
//				// top left
//				fromTopLeft.gameObject.SetActive (true);
//				offset = new Vector2 (ow, -oh);
//			} else if (mp.x < 0 && mp.y < 0) {
//				// bottom left
//				fromBottomLeft.gameObject.SetActive (true);
//				offset = new Vector2 (ow, oh);
//			} else if (mp.x > 0 && mp.y < 0) {
//				// bottom right
//				fromBottomRight.gameObject.SetActive (true);
//				offset = new Vector2 (-ow, oh);
//			}
			break;
		case PopupType.AboveOnly:
			if (mp.x > 0) {
				fromTopLeft.gameObject.SetActive (true);
				offset = new Vector2 (ow, oh);
			} else if (mp.x < 0) {
				fromTopRight.gameObject.SetActive (true);
				offset = new Vector2 (-ow, oh);
			}

			break;
		}
		group.anchoredPosition = offset; // + anchoredPositionFromScreenPoint;
		// Based on which "quadrant" of the screen the user clicked, make sure offset puts the label towards the center of the screen.
		// Assumes centered anchor.

		// assumes bottom left anchor (same as for mousepos) and inverted coords (for input.mousepos)
		//		if (mp.x > Screen.width / 2 && mp.y > Screen.height / 2) {
		//			// Top right
		//			fromTopRight.gameObject.SetActive(true);
		//			offset = new Vector2(-ow,-oh);
		//		} else if (mp.x < Screen.width / 2 && mp.y > Screen.height / 2) {
		//			// top left
		//			fromTopLeft.gameObject.SetActive(true);
		//			offset = new Vector2(ow,-oh);
		//		} else if (mp.x < Screen.width / 2 && mp.y < Screen.height / 2) {
		//			// bottom left
		//			fromBottomLeft.gameObject.SetActive(true);
		//			offset = new Vector2(ow,oh);
		//		} else if (mp.x > Screen.width / 2 && mp.y < Screen.height / 2) {
		//			// bottom right
		//			fromBottomRight.gameObject.SetActive(true);
		//			offset = new Vector2(-ow,oh);
		//		}

		//		Debug.Log ("setting! screenpt:" + screenPoint);

	}


	public void Hide(){
		this.gameObject.SetActive (false);	
	}

}

