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
    AudioSource audiosource;
    public AudioClip[] clips;
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        enemylos = transform.GetComponent<EnemyLOS>();
        audiosource = transform.GetComponent<AudioSource>();
        audiosource.pitch = Random.Range(0.9f,1.3f);
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
        if (agent.speed != speed * 2)
        {
            PlaySound(clips[0]);
        }
        agent.speed = speed * 2;
        lastTargetPos = enemylos.target.position;
        agent.SetDestination(lastTargetPos);
        transform.LookAt(new Vector3(lastTargetPos.x,transform.position.y,lastTargetPos.z));
    }

    void LookAround()
    {
        if(agent.speed != speed * 1.01f){
            if(enemylos.timer / enemylos.loseTime < 0.3f ){
                PlaySound(clips[1]);
            }
        }
        agent.speed = speed * 1.01f;
        agent.SetDestination(lastTargetPos);
    }

    void PlaySound(AudioClip clip)
    {
        audiosource.clip = clip;
        audiosource.Play();
    }
}
