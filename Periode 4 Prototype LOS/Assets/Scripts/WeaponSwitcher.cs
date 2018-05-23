using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

PlayerController plyr;
public GameObject[] weapons;
private int curWeapon = 0;

	void Start () {
		plyr = transform.GetComponent<PlayerController>();
	}
	
	void Update () {
		if(plyr.curState == PlayerController.State.Normal){
			if(Input.GetAxis("Mouse ScrollWheel") != 0){

				//Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
				curWeapon += Mathf.RoundToInt(10 * Input.GetAxis("Mouse ScrollWheel"));
			}
		}

		if(curWeapon < 0){
			curWeapon = weapons.Length - 1;
		} else if(curWeapon > weapons.Length - 1){
			curWeapon = 0;
		}

		for (int i = 0; i < weapons.Length; i++)
		{
			if(curWeapon == i){
				weapons[i].SetActive(true);
			} else {
				weapons[i].SetActive(false);
			}
		}
		//Debug.Log(curWeapon);
	}
}
