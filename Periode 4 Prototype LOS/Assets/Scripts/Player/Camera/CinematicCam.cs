using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCam : MonoBehaviour {

	int curPos = 0;
	public Transform[] posses;
	public float[] speeds;
	PlayerController plyr;
	void Start(){
		plyr = FindObjectOfType<PlayerController>();
	}
	void Update () {
		plyr.curState = PlayerController.State.Cutscene;
		if(Vector3.Distance(transform.position,posses[curPos].position) > 0.1f){
			transform.position = Vector3.Lerp(transform.position,posses[curPos].position,Time.deltaTime * speeds[curPos]);
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles,posses[curPos].eulerAngles,Time.deltaTime * speeds[curPos]);
		} else if(curPos < posses.Length - 1){
			curPos++;
		} else {
			//Set back to normal state
			//curPos = 0;
			plyr.curState = PlayerController.State.Normal;
			Destroy(transform.parent.gameObject);
		}
	}
}
