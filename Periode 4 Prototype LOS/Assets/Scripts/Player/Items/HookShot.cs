using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{

    private PlayerController plyr;
    public float speed = 100;
    public float range = 100;
    public Transform UITarget;
    public Renderer[] model;
    public Vector3[] dontedit;
    LineRenderer rope;
    void Start()
    {
        plyr = transform.parent.parent.GetComponent<PlayerController>();
        rope = model[1].GetComponent<LineRenderer>();
    }

    void Update()
    {

        if(plyr.curState == PlayerController.State.HookShot){
            model[0].enabled = false;
            model[1].enabled = true;
            dontedit[0] = model[1].transform.position;
            dontedit[1] = plyr.transform.position;
            rope.SetPositions(dontedit);
        } else {
            model[0].enabled = true;
            model[1].enabled = false;
            dontedit[0] = Vector3.zero;
            dontedit[1] = Vector3.zero;
            rope.SetPositions(dontedit);
        }

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
                UITarget.LookAt(plyr.transform.position);
                if (CheckState() == true)
                {
                    plyr.crosserHit = true;
                }

            }
        }
    }

    bool CheckState()
    {
        bool toReturn = false;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            if (Vector3.Distance(plyr.transform.position, hit.point) < range)
            {
                if (Vector3.Distance(transform.position, hit.point) > 2)
                {
                    if (plyr.curState == PlayerController.State.Normal)
                    {
                        if (plyr.hookShotJumping == false)
                        {
                            if (hit.point.y > transform.position.y)
                            {
                                toReturn = true;
                            }
                        }
                    }
                }
            }
        }
        return toReturn;
    }

    void RayStuff()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            if (CheckState() == true)
            {
                plyr.HookShotStart(hit.point, speed);
            }

        }
    }
}
