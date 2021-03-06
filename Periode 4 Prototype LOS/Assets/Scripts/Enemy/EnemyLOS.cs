﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class EnemyLOS : LOS
{
    public enum State
    {
        Normal,
        InSight,
        OutSight,
        Spotted,
        Nothing
    }
    [HideInInspector]
    public State curState = State.Normal;
    [HideInInspector]
    public float timer = 0;

    public GameObject hitBox;
    public float seeTime = 1;
    public float loseTime = 5;
    public bool ifCloseYouDie = false;
    public float closedeathDistance = 0.33f;
    [HideInInspector]
    public bool detectionBarBool = false;


    void Update()
    {
        if (curState != State.Nothing)
        {
            if (Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, target.position.y, 0)) < 5)
            {
                Look();
            }
            else
            {
                spotted = false;
            }
        }
        InOutSightActivator();
        IsClose();
        IsSpotted();
        Die();
    }

    void IsClose()
    {
        if (Vector3.Distance(transform.position, target.position) < closedeathDistance / 3)
        {
            if (ifCloseYouDie == true)
            {
                curState = State.Spotted;
            }
        }

    }

    void InOutSightActivator()
    {
        if (spotted == true)
        {
            IsInSight();
        }
        else
        {
            IsOutSight();
        }

    }

    void IsSpotted()
    {
        if (curState == State.Spotted)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            detectionBarBool = true;
            
        }
        else
        {
            detectionBarBool = false;
        }

    }

    void Die()
    {
        if (hitBox == null)
        {
            Destroy(transform.parent.gameObject);
        }

    }

    public void IsInSight()
    {
        switch (curState)
        {
            case State.Normal:
                timer = 0;
                curState = State.InSight;
                break;

            case State.InSight:
                timer += Time.deltaTime / (Vector3.Distance(transform.position, target.position) / 6);
                if (timer > seeTime)
                {
                    curState = State.Spotted;
                    timer = 0;
                }
                break;
            case State.OutSight:
                timer = 0;
                curState = State.InSight;
                break;
        }
    }

    public void IsOutSight()
    {
        switch (curState)
        {

            // case State.Normal:
            //uhm niets eigenlijk.. lol waarom plaats ik dit?
            //break;
            case State.InSight:
                timer = loseTime * (timer / seeTime);
                curState = State.OutSight;
                break;
            case State.OutSight:
                timer = Mathf.MoveTowards(timer, 0, Time.deltaTime);
                if (timer == 0)
                {
                    curState = State.Nothing;
                    StartCoroutine(NothingTime());
                }
                break;
            case State.Spotted:
                timer = loseTime;
                curState = State.OutSight;
                break;
        }
    }

    public IEnumerator NothingTime()
    {
        yield return new WaitForSeconds(5.5f);
        curState = State.Normal;
    }
}
