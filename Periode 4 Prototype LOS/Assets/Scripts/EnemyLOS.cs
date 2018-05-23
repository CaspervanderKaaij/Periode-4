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
    private float timer = 0;

    public GameObject hitBox;


    void Update()
    {
//        Debug.Log(spotted);
        Look();

        if (spotted == true)
        {
            IsInSight();
        }
        else
        {
            IsOutSight();
        }
        if (Vector3.Distance(transform.position, target.position) < sightRange / 3)
        {
            curState = State.Spotted;
        }

        if (curState == State.Spotted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //hardcoded omdat geen tijd ;)

        if(hitBox == null){
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
                timer += Time.deltaTime;
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
        // if(curState == State.Normal){
        // timer = 0;
        // curState = State.InSight;
        //}

    }

    public void IsOutSight()
    {
        switch (curState)
        {

            // case State.Normal:
            //uhm niets eigenlijk.. lol waarom plaats ik dit?
            //break;
            case State.InSight:
                timer = 5;
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
