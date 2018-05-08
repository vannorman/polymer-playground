using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

	public Vector2 scrollSpeed = new Vector2(0.5F,0);
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		Vector2 offset = Time.time * scrollSpeed;
		rend.material.SetTextureOffset("_MainTex", offset);
	}
}
