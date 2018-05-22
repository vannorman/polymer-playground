using UnityEngine;
using System.Collections;

public class WavePlane : MonoBehaviour {

	int xDim;
	int yDim;
	Mesh m;
	Vector3[] verts;
	public float waveHeight=1;
	public float speed = 0.5f;
	MeshCollider[] mcs;
	public Transform childrenParent;

	void Start () {
//		mcs = new MeshCollider[childrenParent.childCount + 1];
//		for(int i=0;i<mcs.Length;i++){
//			if (i == mcs.Length -1) mcs[i] = GetComponent<MeshCollider>();
//			else mcs[i] = childrenParent.GetChild(i).GetComponent<MeshCollider>();
//		}

		m = GetComponent<MeshFilter>().sharedMesh;
		verts = m.vertices;
//		xDim = Mathf.Sqrt(verts.Length);
		xDim = 51;
		yDim = 52;
		// 51 x 52 verts. Y U NO SQUARE, KRAUT_PLANE?

	}
	
//	// Update is called once per frame
	void Update () {

		for (int i=0;i<verts.Length;i++){
			float yPos = Mathf.Sin (Time.time*speed + (i % 52))/30f; //*verts[v].y/xDim*amp;
			yPos *= waveHeight;
//			verts[i] = new Vector3(verts[i].x,yPos,verts[i].z);
			verts[i] = new Vector3(verts[i].x,yPos,verts[i].z);

//			52;i++){
//			for (int j=0;j<52;j++){
//				int v = Mathf.Clamp(i*j,0,verts.Length-1);
//				// commented Debug.Log ("pre post clamp:"+i*j+","+v);
//				// commented Debug.Log ("verts v 0:"+verts[v]);
//				verts[v] = new Vector3(verts[v].x,yPos,verts[v].z);
////				// commented Debug.Log ("verts v 1:"+verts[v]);
//			}
		}
		m.vertices = verts;
		m.RecalculateBounds();
		m.RecalculateNormals();
//		mf.sharedMesh = m;
//		foreach (MeshCollider mc in mcs){
//			mc.sharedMesh = null;
//			mc.sharedMesh = m;
//		}
	}


//	public float GetHeightAtXZPos(Vector3 xzPos){
//
////		float yPos = 
////		return FollowYPos;
//	}
}
