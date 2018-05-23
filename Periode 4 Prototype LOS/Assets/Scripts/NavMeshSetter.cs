using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSetter : MonoBehaviour
{

    public Transform[] target;
    private int curTarget = 0;
    private NavMeshAgent agent;
    public bool loop = true;
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target[curTarget].position);

        //if(agent.isStopped){

        //}
        //if(transform.position == target[curTarget].position){
        if (Vector3.Distance(transform.position,target[curTarget].position) < 0.5f)
        {
            if (curTarget < target.Length - 1)
            {
                curTarget++;
            }
            else if (loop == true)
            {
                curTarget = 0;
            }
        }

    }
}
