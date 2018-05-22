using UnityEngine;
using System.Collections;

public class FlickerLightAnd : MonoBehaviour {


//	Color newColor = new Color(

	float flickerInterval = .04f;
	
	float minSize = 1f;
	float maxSize = 4f;
	// Use this for initialization
	void Start () {
		StartCoroutine(RecursiveFlicker(flickerInterval));

	}
	
	
	IEnumerator RecursiveFlicker(float waitTime){
		
		gameObject.GetComponent<Light>().range = Random.Range(minSize,maxSize);
		
		
		yield return new WaitForSeconds(waitTime);
		StartCoroutine(RecursiveFlicker(Random.Range(0f,flickerInterval)));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
