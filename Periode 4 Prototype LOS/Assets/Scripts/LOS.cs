using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{

    public Transform target;
    public float sightRange = 10;
    [HideInInspector]
    public bool spotted = false;
    [Range(30, 90)]
    public float sightAngle = 130;


    void Update()
    {
        Look();
    }

    public void Look()
    {
         //Debug.DrawRay(transform.position,(target.position - transform.position));           // Activeer deze lijn code om te zien hoe de LOS werkt in raycast vorm!
        //Debug.Log(Vector3.Dot(target.position - transform.position, transform.forward) / Vector3.Distance(transform.position,target.position));
        //Debug.Log(Vector3.Dot(target.position - transform.position, transform.forward) + "Distance >" + Vector3.Distance(transform.position,target.position));
        float losAngle = Vector3.Dot(target.position - transform.position, transform.forward) / Vector3.Distance(transform.position, target.position);
        losAngle = Vector3.Distance(new Vector3(losAngle,0,0), new Vector3(1,0,0));
        if (Vector3.Distance(transform.position, target.position) < sightRange)
        {
            if (losAngle * 180 < sightAngle)
            {
                if (Vector3.Dot(target.position - transform.position, transform.forward) > 1)//    Dit checked of de target voor de enemy staat. 
                {
                    if (Physics.Raycast(transform.position, (target.position - transform.position), Vector3.Distance(transform.position, target.position)) == false)
                    {
                        spotted = true;
                    }
                    else
                    {
                        spotted = false;
                    }
                }
                else
                {
                    spotted = false;
                }

            }
        }
        else
        {
            spotted = false;
        }
    }
}
