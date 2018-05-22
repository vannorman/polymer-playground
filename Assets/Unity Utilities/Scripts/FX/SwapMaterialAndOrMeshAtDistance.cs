using UnityEngine;
using System.Collections;

public class SwapMaterialAndOrMeshAtDistance : MonoBehaviour {
	
	int checkTimer = 0;
	public int checkFrameInterval=10;
	
	public Material newMat = null;
	public Mesh newMesh = null;
	
	Material oldMat = null;
	Mesh oldMesh = null;
	
	public float distance = 90;
	
	bool swapped = false;
	
	// Use this for initialization
	void Start () {
		oldMat = GetComponent<Renderer>().sharedMaterial;
		oldMesh = GetComponent<MeshFilter>().sharedMesh;
	}
	
	// Update is called once per frame
	void Update () {
		checkTimer--;
		if(checkTimer <= 0) {
			checkTimer = checkFrameInterval;
			
			float distSq = (transform.position - Camera.main.transform.position).sqrMagnitude;
			if(!swapped && distSq >= distance*distance) {
				swapped = true;
				if(newMat) { GetComponent<Renderer>().sharedMaterial = newMat; }
				if(newMesh) { GetComponent<MeshFilter>().sharedMesh = newMesh; }
			}
			else if (swapped && distSq < distance * distance) {
				swapped = false;	
				if(newMat) { GetComponent<Renderer>().sharedMaterial = oldMat; }
				if(newMesh) { GetComponent<MeshFilter>().sharedMesh = oldMesh; }				
			}
		}
	}
}
