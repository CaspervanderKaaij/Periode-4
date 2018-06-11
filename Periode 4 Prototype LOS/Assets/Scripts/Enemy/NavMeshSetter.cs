﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSetter : MonoBehaviour
{
    //This is an enemy script only!!!

    public enum PathFindStrat
    {
        AToB,
        Random,
    }

    public PathFindStrat pathFindStrat = PathFindStrat.AToB;

     public enum TalkMode
    {
        InSelf,
        Silent,
        Conversation
    }

    public TalkMode talkMode = TalkMode.InSelf;

    public Transform[] target;
    private int curTarget = 0;
    private NavMeshAgent agent;
    public bool loop = true;
    EnemyLOS enemylos;
    private Vector3 lastTargetPos;
    public float speed = 3.5f;
    AudioSource audiosource;
    public AudioClip[] clips;
    private bool hasSeen = false;
    public bool walker = true;
    private float talkTimer = 0;
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        enemylos = transform.GetComponent<EnemyLOS>();
        audiosource = transform.GetComponent<AudioSource>();
        audiosource.pitch = Random.Range(0.9f, 1.3f);
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
                case EnemyLOS.State.Spotted:
                SeeBehaviour();
                break;
        }
    }

    void NormalBehaviour()
    {

        talkTimer += Time.deltaTime;
        if (talkTimer > 3)
        {
            talkTimer = 0;
            PlaySound(clips[Random.Range(2, 5)]);
        }

        if (enemylos.spotted == false)
        {
            agent.speed = speed;
        }
        else
        {
            agent.speed = 0;
        }
        lastTargetPos = Vector3.zero;
        if (walker == true)
        {
            agent.SetDestination(target[curTarget].position);


            if (Vector3.Distance(transform.position, target[curTarget].position) < 0.5f)
            {
                if (pathFindStrat == PathFindStrat.AToB)
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
                else
                {
                    curTarget = Random.Range(0,target.Length);
                }
            }
        }
        else
        {
            agent.SetDestination(target[0].position);
        }
    }

    void SeeBehaviour(){
        agent.speed = 0;
    }

    void SpotFollow()
    {
        if (agent.speed != speed * 2)
        {
            PlaySound(clips[0]);
        }
        if (enemylos.timer / enemylos.seeTime > 0.3f)
        {
            agent.speed = speed * 2;
        }
        else
        {
            agent.speed = 0;
        }
        lastTargetPos = enemylos.target.position;
        agent.SetDestination(lastTargetPos);
        hasSeen = true;
        // transform.LookAt(new Vector3(lastTargetPos.x, transform.position.y, lastTargetPos.z));
    }

    void LookAround()
    {
        if (agent.speed != speed * 1.01f)
        {
            if (hasSeen == false)
            {
                PlaySound(clips[1]);
                hasSeen = false;
            }
        }
        agent.speed = speed * 1.01f;
        if (lastTargetPos != Vector3.zero)
        {
            agent.SetDestination(lastTargetPos);
        }
    }

    void PlaySound(AudioClip clip)
    {
        audiosource.Stop();
        audiosource.clip = clip;
        audiosource.Play();
    }
}
