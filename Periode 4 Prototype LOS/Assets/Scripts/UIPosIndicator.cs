using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPosIndicator : MonoBehaviour {

RectTransform rect;
public Transform target;
public Transform from;

	void Start () {
		rect = transform.GetComponent<RectTransform>();
	}
	
	void Update () {
		//float x = target.position.x;
		//float y = Input.GetAxis("Vertical");
		float angle = Mathf.Atan2(-from.position.x - -target.position.x,from.position.z - target.position.z) * Mathf.Rad2Deg;
		rect.eulerAngles = new Vector3(0,0,angle + Camera.main.transform.eulerAngles.y + 180);
	}
}
