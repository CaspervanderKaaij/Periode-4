using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			Destroy(gameObject);
		}
	}
}
