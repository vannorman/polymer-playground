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

    public enum State { 
        Shown,
        FadingOut,
        Hidden
    }
    public State state;

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
		AboveRight,
        AboveLeft,
        BelowRight,
        BelowLeft
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
    public void SetState(State newState) {
        state = newState;
    }
	public void Show(Vector3 p, string text, PopupType popType){
        popupType = popType;
        Utils2.SetAllMaterialsAndUI(transform, 1);
        SetState(State.Shown);
		timeout = 7f;
        infoText.GetComponent<SimpleTypewriter>().TypeText(text);
		
		this.gameObject.SetActive (true);
		worldHitPos = p;
		SetPopupPosition (); // make sure posiiton is set this frame.
		SetOffsetPosition();
	}


	float timeout = 0f;
	float offsetTimeout = 0f;
	void Update() {
        switch (state) { 
            case State.Shown:
                SetPopupPosition ();

                timeout -= Time.deltaTime;
                offsetTimeout -= Time.deltaTime;
                if (timeout < 0)
                {
                    SetState(State.FadingOut);
                }
                break;
            case State.FadingOut:
                bool finished = Utils2.FadeAllMaterialsAndUI(transform, 0, 3);
                if (finished) {
                    SetState(State.Hidden);
                }
                break;
            case State.Hidden:
                break;
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
		
            
        case PopupType.AboveRight:
            fromTopLeft.gameObject.SetActive (true);
            offset = new Vector2 (ow, oh);
            break;
        case PopupType.AboveLeft:
                
			fromTopRight.gameObject.SetActive (true);
			offset = new Vector2 (-ow, oh);
            break;

        case PopupType.BelowRight:
            fromBottomLeft.gameObject.SetActive(true);
            offset = new Vector2(ow, -oh);
            break;
        case PopupType.BelowLeft:

            fromBottomRight.gameObject.SetActive(true);
            offset = new Vector2(-ow, -oh);
            break;
		}
		group.anchoredPosition = offset; // + anchoredPositionFromScreenPoint;
		

	}


	public void Hide(){
        SetState(State.Hidden);
        //Debug.Log("hide;" + this.name);
		this.gameObject.SetActive (false);
        infoText.GetComponent<SimpleTypewriter>().SetState(SimpleTypewriter.State.FadingOut);
	}

}

