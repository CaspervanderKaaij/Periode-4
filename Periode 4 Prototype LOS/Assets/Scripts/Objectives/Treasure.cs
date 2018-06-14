using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

public enum State
{
	Collectable,
	Held,
	Both	
}
public State curState = State.Collectable;
public Transform parent;
	void Start () {
		
	}
	
	void Update () {
		if(curState != State.Collectable){
			if(parent != null){
				transform.position = parent.position;
			} else {
				curState = State.Collectable;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			if(curState != State.Held){
				Destroy(gameObject);
			}
		}
	}
}
