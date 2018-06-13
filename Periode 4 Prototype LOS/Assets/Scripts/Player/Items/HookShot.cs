using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{

    private PlayerController plyr;
    public float speed = 100;
    public float range = 100;
    public Transform UITarget;
    void Start()
    {
        plyr = transform.parent.parent.GetComponent<PlayerController>();
    }

    void Update()
    {
        //Debug.DrawRay(transform.parent.position,transform.parent.forward,Color.red,0.001f);
        plyr.crosserHit = false;
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
                if (Vector3.Distance(plyr.transform.position, hit.point) < range)
                {
                    if (Vector3.Distance(transform.position, hit.point) > 2)
                    {
                        if (plyr.curState != PlayerController.State.HookShot)
                        {
                            if (plyr.hookShotJumping == false)
                            {
                                if (hit.point.y > transform.position.y)
                                {
                                    plyr.crosserHit = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void RayStuff()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            if (Vector3.Distance(plyr.transform.position, hit.point) < range)
            {
                if (Vector3.Distance(transform.position, hit.point) > 2)
                {
                    if (plyr.curState != PlayerController.State.HookShot)
                    {
                        if (plyr.hookShotJumping == false)
                        {
                            if (hit.point.y > transform.position.y)
                            {
                                plyr.HookShotStart(hit.point, speed);
                            }
                        }
                    }
                }
            }
        }
    }
}
