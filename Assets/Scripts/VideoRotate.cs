using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoRotate : VideoSequenceObject {

    bool rotating = false;
    public Space space;
    public Vector3 dir = Vector3.up;
    bool fading = false;
    Renderer r;
    public float rotationDuration = 3.5f;

    public float fadeSpeed = 3;

    void Start(){
        r = GetComponent<Renderer>();
    }

    override public void Fire(){
        rotating = true;
        FadeIn();
        Invoke("FadeOut",rotationDuration);
    }

    Color targetColor;
    public void FadeIn(){
        fading = true;
        ColorUtility.TryParseHtmlString("#FFF869FF", out targetColor);
    }

    void FadeOut(){
        fading = true;
        ColorUtility.TryParseHtmlString("#FFF86900", out targetColor);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)){
            Fire();

        }
        if (fading){
            r.material.color = Color.Lerp(r.material.color, targetColor, Time.deltaTime * fadeSpeed);
            if (Mathf.Abs(r.material.color.a-targetColor.a) < .01f){
                fading = false;   
            }
        }
        if (rotating){
            transform.Rotate(dir, space);
        }
	}
}
