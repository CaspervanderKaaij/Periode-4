using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public enum State
    {
        Normal,
        HookShot,
        HookShotEnd
    }
    //CODE VAN CASPER
    [Header("Door Casper")]
    [Range(0,3)]
    public float hookShotJumpStrength = 1;
    [HideInInspector]
    public State curState = State.Normal;
    [HideInInspector]
    public bool hookShotJumping = false;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }


    private Vector3 hookShotGoal = Vector3.zero;
    private float hookShotSpeed = 1;

    public void HookShotStart(Vector3 goal, float speed)
    {
        if (goal.y > transform.position.y)
        {
            curState = State.HookShot;
            hookShotGoal = goal + new Vector3(0, 1, 0);
            hookShotSpeed = speed;
        }
    }

    void HookShoting()
    {
        mayJump = false;
        //jumpCurrent = 0;
        // if (transform.position != hookShotGoal)
        if (Vector3.Distance(transform.position, hookShotGoal) > 1)
        {
            if (hookShotJumping == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, hookShotGoal, hookShotSpeed * Time.deltaTime);
            }
            else
            {
                curState = State.Normal;
            }
        }
        else
        {
            curState = State.HookShotEnd;
        }
    }

    void HookShotJump()
    {
        body.velocity = velocity * hookShotJumpStrength;
        hookShotJumping = true;
        speed /= 2;
        FindObjectOfType<Manager>().TimeStop(0.1f, 0f);
        curState = State.Normal;
    }

    //CODE VAN DANIEL
    [Header("Door Daniel")]


    [Header("Movement")]
    public float speed;
    public float rotSpeed;

    [Header("Jumping")]
    public Vector3 velocity;
    public int jumpCurrent;
    public int jumpMax;

    private bool mayJump;
    private Rigidbody body;
    private Vector3 v;
    private Vector3 r;
    private RaycastHit hit;
    void DanielMove()
    {
        Look();
        Jump();
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    // Use this for initialization

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (curState == State.Normal)
        {//Toegevoegt door Casper
            Move();
        }
    }

    //collision for jump reset
    private void OnCollisionEnter(Collision c)
    {
        // if (curState == State.Normal)
        // {//Toegevoegt door Casper.. en ook weer weg gedaan.. het is buggy anders
        if (c.gameObject.tag == "Terrain" || c.gameObject.tag == "Car")
        {
            jumpCurrent = 0;
            mayJump = true;
        }
        //}
    }

    //movement back-front & left-right
    private void Move()
    {
        v.x = Input.GetAxis("Horizontal");
        v.z = Input.GetAxis("Vertical");
        transform.Translate(v * speed * Time.deltaTime);
    }

    //jumping
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (mayJump == true)
            {
                body.velocity = velocity;
                jumpCurrent++;
                if (jumpCurrent >= jumpMax)
                {
                    mayJump = false;
                }
            }
        }
    }

    //look left and right
    private void Look()
    {
        r.y = Input.GetAxis("Mouse X");
        transform.Rotate(r * rotSpeed * Time.deltaTime);
    }

    //environment interaction
    private void Interact()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5.0f))
        {
            if (hit.transform.tag == "Door")
            {
                print("open");
                Destroy(hit.transform.gameObject, 0.1f);
            }
        }
    }

    //CODE UPDATE STAAT HIER
    void Update()
    {

        if (hookShotJumping == true)
        {
            if (mayJump == true)
            {
                hookShotJumping = false;
                speed *= 2;
            }
        }
        switch (curState)
        {

            case State.Normal:
                DanielMove();
                break;

            case State.HookShot:
                HookShoting();
                body.velocity = Vector3.zero;
                Look();
                break;
            case State.HookShotEnd:
                HookShotJump();
                break;
        }
    }
}
