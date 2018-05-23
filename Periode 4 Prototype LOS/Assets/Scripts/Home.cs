using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour {

public GameObject winScreen;
	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			if(FindObjectOfType<Manager>().curObjective == Manager.Objective.GoHome){
				winScreen.SetActive(true);
				//Time.timeScale = 0;
				//Cursor.lockState = CursorLockMode.None;
				FindObjectOfType<Manager>().MissionComplete();
			}
		}
	}
}
