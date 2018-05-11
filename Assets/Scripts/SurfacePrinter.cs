using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfacePrinter : MonoBehaviour {



	public Transform materialLocation;
	public Transform xArm;
	public Transform zArm;
	public Transform printHead;
	Vector3 printHeadAnchor;
	public Transform printerSurface;
	Vector3[] printLocations;
	int printLocationIndex = 0;
	public int size=3;
	public float scale = 3f;
	void InitPrintingSurface(){
		printLocations = Utils2.HexGrid (size, scale);

		for (int i = 0; i < printLocations.Length; i++) {
			printLocations [i] = printerSurface.position + printLocations [i];
			GameObject o = GameObject.CreatePrimitive (PrimitiveType.Cube);
			o.transform.localScale = Vector3.one * 0.2f;
			o.transform.position = printLocations [i];
		}
	}

	public Transform[] tests;

	void Start(){
		printHeadAnchor = printHead.position;
//		Debug.Log ("Mat loc y:" + materialLocation.position.y);
		InitPrintingSurface ();
	}

	enum State {
		Ready,
		Printing
	}
	State state;

	enum MotionState {
		GettingMaterial,
		MovingWithMaterial,
		SettingMaterial,
		Retracting,
		Return
	}
	MotionState motionState;
	void SetMotionState(MotionState newState){
//		Debug.Log ("set motion state;" + newState);
		motionState = newState;
	}
	float t = 0;
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			state = State.Printing;
			motionState = MotionState.Return;
		}
		if (Input.GetKey (KeyCode.Alpha1)) {
			MovePrinterXZ (tests [0].position);
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			MovePrinterXZ (tests [1].position);
		}
		switch (state) {
		case State.Ready:
			break;
		case State.Printing:
			switch (motionState) {
			case MotionState.Return:
				float d = MovePrinterXZ (materialLocation.position);
				if (d < 0.001f) {
					SetMotionState (MotionState.GettingMaterial);
				}
				break;	
			case MotionState.GettingMaterial:
//				Debug.Log ("getting mat. material location y:" + materialLocation.position.y);
				float d2 = MovePrinterY (materialLocation.position);
				if (d2 < 0.001f) {
					GetCurrentMaterial ();
					SetMotionState (MotionState.MovingWithMaterial);
				}
				break;
			case MotionState.MovingWithMaterial:
				float d3 = MovePrinterXZ (printLocations [printLocationIndex]);
				if (d3 < 0.001f) {
					SetMotionState (MotionState.SettingMaterial);
				}
				break;
			case MotionState.SettingMaterial:
				float d4 = MovePrinterY (printLocations [printLocationIndex]);
				if (d4 < 0.001f) {
					SetMaterial ();
					OnCompletedSetMaterial ();
					SetMotionState (MotionState.Retracting);

				}
				break;
			case MotionState.Retracting:
				float d5 = MovePrinterY (printHeadAnchor);
				if (d5 < .001f) {
					SetMotionState (MotionState.Return);
				}
				break;
			}
			break;
		}
	}

	public GameObject[] materialOrder;
	int materialIndex=0;
	GameObject carriedMaterial;
	void GetCurrentMaterial(){
		GameObject o = (GameObject)Instantiate (materialOrder [materialIndex]);
		o.transform.position = printHead.position;
		o.transform.SetParent (printHead);
		carriedMaterial = o;
	}

	float MovePrinterXZ(Vector3 dest){
		float moveSpeed = 10f;
		xArm.position = Vector3.MoveTowards (xArm.position, new Vector3 (dest.x, xArm.position.y, xArm.position.z), Time.deltaTime * moveSpeed);
		zArm.position = Vector3.MoveTowards (zArm.position, new Vector3 (zArm.position.x, zArm.position.y, dest.z), Time.deltaTime * moveSpeed);
		float d = Mathf.Abs (xArm.position.x - dest.x) + Mathf.Abs (zArm.position.z - dest.z);
		return d;
	}

	float MovePrinterY(Vector3 dest){
		float headMoveSpeed = 3f;
//		Debug.Log ("moving from:" + printHead.transform.position.y + " to " + dest.y);
		printHead.transform.position = Vector3.MoveTowards (printHead.transform.position, new Vector3 (printHead.transform.position.x, dest.y, printHead.position.z), headMoveSpeed * Time.deltaTime);
		float d = Mathf.Abs (printHead.position.y - dest.y);
		return d;
	}

	void OnCompletedSetMaterial(){
		printLocationIndex++;
		Debug.Log ("print loc ind:" + printLocationIndex+": "+printLocations[printLocationIndex] );
		if (printLocationIndex >= printLocations.Length) {
			materialIndex++;
			if (materialIndex >= materialOrder.Length) {
				Finished ();
			}
			printLocationIndex = 0;
		}

	}

	void SetMaterial(){
		GameObject placedMaterial = (GameObject)Instantiate (materialOrder [materialIndex], printHead.transform.position + Vector3.up * 0.2f, Quaternion.identity);
	}

	void Finished(){
		state = State.Ready;
	}
}
