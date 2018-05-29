using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransform : MonoBehaviour {

	public Vector3 moveV3 = Vector3.zero;
	public Vector3 rotateV3 = Vector3.zero;
	public Vector3 scaleV3 = Vector3.zero;

	
	void Update () {
		transform.position += moveV3 * Time.deltaTime;
		transform.eulerAngles += rotateV3 * Time.deltaTime;
		transform.localScale += scaleV3 * Time.deltaTime;
	}
}
