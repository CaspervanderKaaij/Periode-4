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
    EnemyLOS enemylos;
    private Vector3 lastTargetPos;
    public float speed = 3.5f;
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        enemylos = transform.GetComponent<EnemyLOS>();
    }

    void Update()
    {
        switch (enemylos.curState)
        {

            case EnemyLOS.State.Normal:
                NormalBehaviour();
                break;
            case EnemyLOS.State.InSight:
                if (enemylos.timer / enemylos.seeTime > 0.3f)
                {
                    SpotFollow();
                }
                else
                {
                    NormalBehaviour();
                }
                break;
            case EnemyLOS.State.OutSight:
                LookAround();
                break;
        }
    }

    void NormalBehaviour()
    {
        agent.speed = speed;
        agent.SetDestination(target[curTarget].position);

        if (Vector3.Distance(transform.position, target[curTarget].position) < 0.5f)
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

    void SpotFollow()
    {
        agent.speed = 0;
        lastTargetPos = enemylos.target.position;
        agent.SetDestination(lastTargetPos);
    }

    void LookAround()
    {
        agent.speed = speed / 3;
        agent.SetDestination(lastTargetPos);
    }

    void BackToRoute()
    {

    }
}
