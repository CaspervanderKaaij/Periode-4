using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
[HideInInspector]
    public CharacterController cc;
	public float gravity = 9.81f;

    public void RealStart()
    {
		cc = transform.GetComponent<CharacterController>();
    }

	public void Walk(bool teleport,Vector3 vel){
		if(teleport == false){
			cc.Move(new Vector3(vel.x,-gravity,vel.z) * Time.deltaTime);
		} else {
			transform.position = vel;
		}
	}

	public void Look(Vector3 euler){
		transform.eulerAngles = euler;
	}

}
