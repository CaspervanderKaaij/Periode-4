using System.Collections;
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
    public Animator anim;
    public enum Animate
    {
        Idle,
        Walk,
        Death,
        Cough,
        Confused
    }
    public Animate curAnim = Animate.Idle;

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
        SetAnim();
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

        TalkStuff();

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
                    // curTarget = Random.Range(0, target.Length);
                    if (agent.isStopped == false)
                    {
                        StartCoroutine(NextDestination());
                    }
                    agent.Stop();
                }
            }
        }
        else
        {
            agent.SetDestination(target[0].position);
        }
    }

    IEnumerator NextDestination()
    {
        yield return new WaitForSeconds(1);
        curTarget = Random.Range(0, target.Length);
        agent.Resume();
    }

    void TalkStuff()
    {
        switch (talkMode)
        {

            case TalkMode.InSelf:
                talkTimer += Time.deltaTime;
                if (talkTimer > 3)
                {
                    talkTimer = 0;
                    PlaySound(clips[Random.Range(2, 5)]);
                }
                break;
            case TalkMode.Conversation:
                talkTimer += Time.deltaTime;
                if (talkTimer > 3)
                {
                    talkTimer = 0;
                    PlaySound(clips[2]);
                }
                break;
        }
    }

    void SeeBehaviour()
    {
        agent.speed = 0;
        // agent.SetDestination(target[0].position);
        // Debug.Log("poop");
        // agent.enabled = false;
        Vector3 playerPos = enemylos.target.position;
        transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
        // transform.eulerAngles += new Vector3(0, 180, 0);
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

    void SetAnim()
    {
        switch (enemylos.curState)
        {
            case EnemyLOS.State.Normal:
                if (agent.isStopped == true)
                {
                    curAnim = Animate.Idle;
                }
                else
                {
                    curAnim = Animate.Walk;
                }
                break;
            case EnemyLOS.State.Nothing:
                if (curAnim != Animate.Cough)
                {
                    curAnim = Animate.Confused;
                }
                break;
            case EnemyLOS.State.Spotted:
                curAnim = Animate.Idle;
                break;
            case EnemyLOS.State.InSight:
                if (agent.isStopped == true)
                {
                    curAnim = Animate.Idle;
                }
                else
                {
                    curAnim = Animate.Walk;
                }
                break;
            case EnemyLOS.State.OutSight:
                if (agent.isStopped == true)
                {
                    curAnim = Animate.Idle;
                }
                else
                {
                    curAnim = Animate.Walk;
                }
                break;

        }
        switch (curAnim)
        {

            case Animate.Idle:
                anim.SetInteger("curAnim", 0);
                break;
            case Animate.Confused:
                anim.SetInteger("curAnim", 1);
                break;
            case Animate.Cough:
                anim.SetInteger("curAnim", 2);
                break;
            case Animate.Walk:
                anim.SetInteger("curAnim", 3);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Smoke")
        {
            enemylos.curState = EnemyLOS.State.Nothing;
            StartCoroutine(enemylos.NothingTime());
            curAnim = Animate.Cough;
            other.tag = "Untagged";
        }
    }
}
