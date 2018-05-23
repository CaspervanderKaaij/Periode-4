using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : Move {

public float speed = 10;
	void Start () {
		//Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
		Look(transform.eulerAngles + (new Vector3(-Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0) * speed * Time.deltaTime));
	}
}
