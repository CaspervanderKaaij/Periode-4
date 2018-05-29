using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{

    private PlayerController plyr;
    public float speed = 100;

    public Transform UITarget;
    void Start()
    {
        plyr = transform.parent.parent.GetComponent<PlayerController>();
    }

    void Update()
    {
        //Debug.DrawRay(transform.parent.position,transform.parent.forward,Color.red,0.001f);
        if (Input.GetButtonDown("Fire1"))
        {
            RayStuff();
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 100))
            {
                UITarget.position = hit.point;
            }
        }
    }

    void RayStuff()
    {
        RaycastHit hit;
        //Camera.main.ScreenPointToRay(Vector3.zero);
        //if(Physics.Raycast(transform.parent.position,transform.parent.forward,out hit,100)){
        //if(Physics.Raycast(Camera.main.ScreenPointToRay(Vector3.forward),out hit,100)){
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            if (plyr.curState != PlayerController.State.HookShot)
            {
                // transform.parent.parent.position = hit.point + new Vector3(0, 1.33f, 0);
                if (plyr.hookShotJumping == false)
                {
                    plyr.HookShotStart(hit.point, speed);
                }
            }
        }
    }
}
