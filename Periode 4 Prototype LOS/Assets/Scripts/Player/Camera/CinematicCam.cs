using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCam : MonoBehaviour {

	int curPos = 0;
	public Transform[] posses;
	void Update () {
		if(Vector3.Distance(transform.position,posses[curPos].position) > 0.1f){
			transform.position = Vector3.Lerp(transform.position,posses[curPos].position,Time.deltaTime);
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles,posses[curPos].eulerAngles,Time.deltaTime);
		} else if(curPos < posses.Length - 1){
			curPos++;
		} else {
			//Set back to normal state
			//curPos = 0;
			Destroy(transform.parent.gameObject);
		}
	}
}
