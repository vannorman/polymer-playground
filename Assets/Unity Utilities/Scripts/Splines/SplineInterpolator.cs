using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eEndPointsMode { AUTO, AUTOCLOSED, EXPLICIT }
public enum eWrapMode { ONCE, LOOP }
public delegate void OnEndCallback();

public class SplineInterpolator : MonoBehaviour
{
	
	public AnimationCurve speedCurve;
	GameObject savedCamPos;
	float mRealTime=0f;
	public float speedFactor = 1;
	eEndPointsMode mEndPointsMode = eEndPointsMode.AUTO;

	public bool swapCameras;

	void Start(){

		Init ();
	}




	internal class SplineNode
	{
		internal Vector3 Point;
		internal Quaternion Rot;
		internal float Time;
		internal Vector2 EaseIO;

		internal SplineNode(Vector3 p, Quaternion q, float t, Vector2 io) { Point = p; Rot = q; Time = t; EaseIO = io; }
		internal SplineNode(SplineNode o) { Point = o.Point; Rot = o.Rot; Time = o.Time; EaseIO = o.EaseIO; }
	}

	List<SplineNode> mNodes = new List<SplineNode>();
	string mState = "";
	bool mRotations;

    public Transform GetFirstNode(){
        return GetComponent<SplineController>().SplineRoot.transform.GetChild(0);
    }

    public List<Vector3> GetNodes()
    {
        List<Vector3> retList = new List<Vector3>();
        foreach (SplineNode node in mNodes)
        {
            retList.Add(node.Point);
        }
        return retList;
    }

	public int GetSplineNodeCount(){
		return mNodes.Count;
	}

	public Vector3 ReturnNodeAtIndex(int i){
		return mNodes[i].Point;
	}

	OnEndCallback mOnEndCallback;






	IEnumerator SetCanpauseAfterSeconds(float s){
		yield return new WaitForSeconds(s);
		transform.parent.gameObject.SetActive(false); 
	}

	bool exiting = false;
	public void Exit(){
		exiting = true;
		// Debug.Log ("[SPLINE] exiting..");

	}

    public float GetAvgNodeDist(){

        float avgNodeDist = GetTotalDist() / (float)mNodes.Count;
        return avgNodeDist;
    }

    public float GetTotalDist(){
        // calc avg spline node dist
        float totalDist = 0;
        List<Vector3> trans =  GetNodes();
        for (int c = 1; c < trans.Count; c++)
        {
            totalDist += (trans[c] - trans[c-1]).magnitude;
        }
        return totalDist;
    }
	
	
	public void StartInterpolation(OnEndCallback endCallback, bool bRotations, eWrapMode mode)
	{

		if (mState != "Reset")
			throw new System.Exception("First reset, add points and then call here");
		// commented Debug.Log("StarT");
		mState = mode == eWrapMode.ONCE ? "Once" : "Loop";
		mRotations = bRotations;
		mOnEndCallback = endCallback;

		SetInput();
	}



	public void Init(string s = "default"){
		if (exiting) return; // race for init / exit if exit is called from another script

		mRealTime=0;
	}

	public void Reset()
	{
//			// commented Debug.Log ("reset");
		mNodes.Clear();
		mState = "Reset";
		mCurrentIdx = 1;
		mCurrentTime = 0;
		mRotations = false;
		mEndPointsMode = eEndPointsMode.AUTO;
	}

	public void AddPoint(Vector3 pos, Quaternion quat, float timeInSeconds, Vector2 easeInOut)
	{
		if (mState != "Reset")
			throw new System.Exception("Cannot add points after start");
		mNodes.Add(new SplineNode(pos, quat, timeInSeconds, easeInOut));
	}


	void SetInput()
	{
		if (mNodes.Count < 2)
			throw new System.Exception("Invalid number of points");

		if (mRotations)
		{
			for (int c = 1; c < mNodes.Count; c++)
			{
				SplineNode node = mNodes[c];
				SplineNode prevNode = mNodes[c - 1];

				// Always interpolate using the shortest path -> Selective negation
				if (Quaternion.Dot(node.Rot, prevNode.Rot) < 0)
				{
					node.Rot.x = -node.Rot.x;
					node.Rot.y = -node.Rot.y;
					node.Rot.z = -node.Rot.z;
					node.Rot.w = -node.Rot.w;
				}
			}
		}

		if (mEndPointsMode == eEndPointsMode.AUTO)
		{
			mNodes.Insert(0, mNodes[0]);
			mNodes.Add(mNodes[mNodes.Count - 1]);
		}
		else if (mEndPointsMode == eEndPointsMode.EXPLICIT && (mNodes.Count < 4))
			throw new System.Exception("Invalid number of points");
	}

	void SetExplicitMode()
	{
		if (mState != "Reset")
			throw new System.Exception("Cannot change mode after start");

		mEndPointsMode = eEndPointsMode.EXPLICIT;
	}

	public void SetAutoCloseMode(float joiningPointTime)
	{
		if (mState != "Reset")
			throw new System.Exception("Cannot change mode after start");

		mEndPointsMode = eEndPointsMode.AUTOCLOSED;

		mNodes.Add(new SplineNode(mNodes[0] as SplineNode));
		mNodes[mNodes.Count - 1].Time = joiningPointTime;

		Vector3 vInitDir = (mNodes[1].Point - mNodes[0].Point).normalized;
		Vector3 vEndDir = (mNodes[mNodes.Count - 2].Point - mNodes[mNodes.Count - 1].Point).normalized;
		float firstLength = (mNodes[1].Point - mNodes[0].Point).magnitude;
		float lastLength = (mNodes[mNodes.Count - 2].Point - mNodes[mNodes.Count - 1].Point).magnitude;

		SplineNode firstNode = new SplineNode(mNodes[0] as SplineNode);
		firstNode.Point = mNodes[0].Point + vEndDir * firstLength;

		SplineNode lastNode = new SplineNode(mNodes[mNodes.Count - 1] as SplineNode);
		lastNode.Point = mNodes[0].Point + vInitDir * lastLength;

		mNodes.Insert(0, firstNode);
		mNodes.Add(lastNode);
	}

	float mCurrentTime;
	int mCurrentIdx = 1;
	
	float escapeTime=0;
	float freezeTimer=0;



    int GetCurrentNodeIndex(float mCurrentTime)
    {
        int mCurrentIdx = 1;
        while (mCurrentTime >= mNodes[mCurrentIdx + 1].Time)
        {
            
            mCurrentIdx++;
            if (mCurrentIdx > mNodes.Count - 2) return -1;
       

        }
        return mCurrentIdx;
    }

    public float GetCurrentNodeDist(float mCurrentTime)
    {
        int mCurrentIdx = GetCurrentNodeIndex(mCurrentTime);
        if (mCurrentIdx == -1)
            return 1;
        float mag = (mNodes[mCurrentIdx+1].Point - mNodes[mCurrentIdx].Point).magnitude;
        return mag;
    }

    public Vector3 GetPositionAtTime(float mCurrentTime)
    {
//		return Vector3.one;

        int mCurrentIdx = GetCurrentNodeIndex(mCurrentTime);
		if (mCurrentIdx == -1) {
			return Vector3.zero; //shruge
		}
        float param = (mCurrentTime - mNodes[mCurrentIdx].Time) / (mNodes[mCurrentIdx + 1].Time - mNodes[mCurrentIdx].Time);

        // Smooth the param
        param = MathUtils.Ease(param, mNodes[mCurrentIdx].EaseIO.x, mNodes[mCurrentIdx].EaseIO.y);
        return GetHermiteInternal(mCurrentIdx, param);

    }

	public Quaternion GetRotationAtTime(float mCurrentTime)
	{


		int mCurrentIdx = GetCurrentNodeIndex(mCurrentTime);
		if (mCurrentIdx == -1)
			return Quaternion.identity;
		float param = (mCurrentTime - mNodes[mCurrentIdx].Time) / (mNodes[mCurrentIdx + 1].Time - mNodes[mCurrentIdx].Time);


		// Smooth the param
		param = MathUtils.Ease(param, mNodes[mCurrentIdx].EaseIO.x, mNodes[mCurrentIdx].EaseIO.y);
		//        Debug.Log("getherm:"+mCurrentIdx+",param:"+param);
		return GetSquad(mCurrentIdx,param);
		//        return GetHermiteAtTime(mCurrentTime);

	}


	public float GetNextTimeStepKeepConstantSpeed(Vector3 startPos, float lastPosT, float speed){
		
		float maxDistDelta = speed * Time.deltaTime;
		Vector3 p1 = startPos;
		Vector3 p2 = startPos;
		int maxSteps = 50;
		float timeDelta = 0f;
		float newPosT = lastPosT;
		while ((p1 - p2).magnitude < maxDistDelta && maxSteps > 0) {
			float splineStep = 0.001f; // increase for better performance but more coarseness
			newPosT += splineStep;
			p2 = GetPositionAtTime (newPosT);
			maxSteps--;
		}

		int currentNodeIndex = GetCurrentNodeIndex (newPosT);
		if (currentNodeIndex == -1) {
			newPosT = 0; //loop
		}


		lastPosT = newPosT;
		return newPosT;
	}



	void Update()
	{
//		if (lockEscapeTimer < escapeTime && !skipButtonVisible){
//			skipButtonVisible = true;
//			if (skipButton) skipButton.SetActive(true);
//		}
//		if (Input.GetKeyDown(KeyCode.Escape)){
//			AttemptEscape();
//		}
//		freezeTimer += Time.deltaTime; 
//		if (freezeAfterSeconds != 0 && freezeTimer > freezeAfterSeconds) {
////			// commented Debug.Log ("return");
//			return;
//		}
//		escapeTime+= Time.deltaTime;
//		if (attemptEscape && lockEscapeTimer < escapeTime && allowEscToExit){
//			Exit();
////			// commented Debug.Log ("Exit");
//		}

		if (mState == "Reset" || mState == "Stopped" || mNodes.Count < 4) 
			return;


//		// commented Debug.Log("splining");
		
		
		
		mRealTime += Time.deltaTime;
		mCurrentTime += Time.deltaTime*speedCurve.Evaluate(mRealTime/GetComponent<SplineController>().Duration);

		// We advance to next point in the path
		if (mCurrentTime >= mNodes[mCurrentIdx + 1].Time)
		{
			if (mCurrentIdx < mNodes.Count - 6)
			{
				mCurrentIdx++;
			}
			else
			{
				if (mState != "Loop")
				{
					Exit();

					mState = "Stopped";

					// We stop right in the end point
//					// commented Debug.Log("tp:");
					transform.position = mNodes[mNodes.Count - 2].Point;
					


					if (mRotations)
						transform.rotation = mNodes[mNodes.Count - 2].Rot;


				}
				else
				{
					mCurrentIdx = 1;
					mCurrentTime = 0;
				}
			}
		}

		if (mState != "Stopped")
		{
			// Calculates the t param between 0 and 1
			float param = (mCurrentTime - mNodes[mCurrentIdx].Time) / (mNodes[mCurrentIdx + 1].Time - mNodes[mCurrentIdx].Time);

			// slow the param if first or last node


			// Smooth the param
			param = MathUtils.Ease(param, mNodes[mCurrentIdx].EaseIO.x, mNodes[mCurrentIdx].EaseIO.y);
//			// commented Debug.Log("getherm:"+mCurrentIdx+",param:"+param);
			transform.position = GetHermiteInternal(mCurrentIdx, param);

			if (mRotations)
			{
				transform.rotation = GetSquad(mCurrentIdx, param);
			}
		}
	}

	Quaternion GetSquad(int idxFirstPoint, float t)
	{
		Quaternion Q0 = mNodes[idxFirstPoint - 1].Rot;
		Quaternion Q1 = mNodes[idxFirstPoint].Rot;
		Quaternion Q2 = mNodes[idxFirstPoint + 1].Rot;
		Quaternion Q3 = mNodes[idxFirstPoint + 2].Rot;

		Quaternion T1 = MathUtils.GetSquadIntermediate(Q0, Q1, Q2);
		Quaternion T2 = MathUtils.GetSquadIntermediate(Q1, Q2, Q3);

		return MathUtils.GetQuatSquad(t, Q1, Q2, T1, T2);
	}



	public Vector3 GetHermiteInternal(int idxFirstPoint, float t, bool debug=false)
	{

		float t2 = t * t;
		float t3 = t2 * t;

		Vector3 P0 = mNodes[idxFirstPoint - 1].Point;
		Vector3 P1 = mNodes[idxFirstPoint].Point;
		Vector3 P2 = mNodes[idxFirstPoint + 1].Point;
		Vector3 P3 = mNodes[idxFirstPoint + 2].Point;

		float tension = 0.5f;	// 0.5 equivale a catmull-rom

		Vector3 T1 = tension * (P2 - P0);
		Vector3 T2 = tension * (P3 - P1);

		float Blend1 = 2 * t3 - 3 * t2 + 1;
		float Blend2 = -2 * t3 + 3 * t2;
		float Blend3 = t3 - 2 * t2 + t;
		float Blend4 = t3 - t2;
		Vector3 blends = Blend1 * P1 + Blend2 * P2 + Blend3 * T1 + Blend4 * T2;
//		if (debug) // commented Debug.Log ("hrm:"+idxFirstPoint+", t:"+t+"; blends;" +blends);
		return blends;
	}


	public Vector3 GetHermiteAtTime(float timeParam)
	{
		if (timeParam >= mNodes[mNodes.Count - 2].Time)
			return mNodes[mNodes.Count - 2].Point;

		int c;
		for (c = 1; c < mNodes.Count - 2; c++)
		{
			if (mNodes[c].Time > timeParam)
				break;
		}

		int idx = c - 1;
		float param = (timeParam - mNodes[idx].Time) / (mNodes[idx + 1].Time - mNodes[idx].Time);
		param = MathUtils.Ease(param, mNodes[idx].EaseIO.x, mNodes[idx].EaseIO.y);

		return GetHermiteInternal(idx, param);
	}
}