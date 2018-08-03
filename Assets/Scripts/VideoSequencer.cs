using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Linq;
using UnityEngine;

public class VideoSequencer : MonoBehaviour {

    [System.Serializable]
    public class Action {
        public float timeToFire;
        public VideoSequenceObject actionObj;
    }

    [SerializeField]
    public List<Action> actions = new List<Action>();


    void Start()
    {

        InitActions();
    }
    void InitActions(){ 
        actions = new List<Action>(); //Dictionary<float, VideoSequenceObject>()[vs.transform.childCount];
        float totalTime = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                Action act = new VideoSequencer.Action();
                act.actionObj = transform.GetChild(i).GetComponent<VideoSequenceObject>();
                act.timeToFire = totalTime;
                //Debug.Log("act dur;" + act.actionObj.duration);
                totalTime += act.actionObj.duration;
                actions.Add(act);
            }
        }
    }

    int index = 0;
    void Update(){
        //t += Time.deltaTime;
        if (index < actions.Count){
            if (Time.time > actions[index].timeToFire){
                actions[index].actionObj.Fire();
                index++;
            }
            
        }
    }

	/*
	 * 
	 * Cam polymer strands, backstory 
	 * Pop text backstory
	 * Cam rotation light receiver molecule
	 * Pop text light rotation
	 * Turn light on
	 * Cam polymer strands
	 * Pop text rotational energy mechanically stored
	 * Cam top lock mechanism
	 * Pop text locking mechanism
	 * Fade cam to bottom
	 * Pop text unlock mechanism release
	 * Cam rotation area
	 * Pop text rotational energy for later use by another MM
	 * */
	
	
}
