using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLOS : LOS
{
    private enum State
    {
        Normal,
        InSight,
        OutSight,
        Spotted
    }
    private State curState = State.Normal;
    [HideInInspector]
    public float timer = 0;

    public GameObject hitBox;
    public float seeTime = 1;
    public float loseTime = 5;


    void Update()
    {
        Look();
        InOutSightActivator();
        IsClose();
        IsSpotted();
        Die();
    }

    void IsClose(){
        if (Vector3.Distance(transform.position, target.position) < sightRange / 3)
        {
            curState = State.Spotted;
        }

    }

    void InOutSightActivator(){
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    void Die(){
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
                timer += Time.deltaTime * seeTime;
                if (timer > 1)
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
                    curState = State.Normal;
                }
                break;
            case State.Spotted:
                timer = 5;
                curState = State.OutSight;
                break;
        }
    }
}
