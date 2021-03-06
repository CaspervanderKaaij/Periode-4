﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    public enum State
    {
        Normal,
        HookShot,
        HookShotEnd,
        Cutscene
    }
    //CODE VAN CASPER
    [Header("Door Casper")]
    [Range(0, 3)]
    public float hookShotJumpStrength = 1;
    [HideInInspector]
    public State curState = State.Normal;
    [HideInInspector]
    public bool hookShotJumping = false;
    public float headBob = 1;
    public Image[] uiCrosser;
    public bool crosserHit = true;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        curState = State.Normal;
    }

    void SetCrosser()
    {

        if (crosserHit == true)
        {
            for (int i = 0; i < uiCrosser.Length; i++)
            {
                uiCrosser[i].color = Color.Lerp(uiCrosser[i].color, Color.green, Time.deltaTime * 30);
            }
        }
        else
        {
            for (int i = 0; i < uiCrosser.Length; i++)
            {
                uiCrosser[i].color = Color.Lerp(uiCrosser[i].color, Color.black, Time.deltaTime * 30);
            }
        }
    }

    private Vector3 hookShotGoal = Vector3.zero;
    private float hookShotSpeed = 1;

    public void HookShotStart(Vector3 goal, float speed)
    {
        curState = State.HookShot;
        hookShotGoal = goal + new Vector3(0, 1, 0);
        hookShotSpeed = speed;
    }

    void HookShoting()
    {
        mayJump = false;
        //jumpCurrent = 0;
        // if (transform.position != hookShotGoal)
        if (Vector3.Distance(transform.position, hookShotGoal) > 2)
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
        body.velocity = velocity * hookShotJumpStrength + transform.TransformDirection(0,0,-3);
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
    /*
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
     */


    //Casper variant
    void CheckFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2))
        {
            mayJump = true;
        }
        else
        {
            mayJump = false;
        }
    }

    //movement back-front & left-right
    float curAcc = 0.3f;//door Casper :D
    private void Move()
    {
        v.x = Input.GetAxis("Horizontal");
        v.z = Input.GetAxis("Vertical");

        if (Vector2.SqrMagnitude(new Vector2(v.x, v.z)) > 0)
        {
            curAcc = Mathf.Lerp(curAcc, 1, Time.deltaTime * 3);
        }
        else
        {
            curAcc = 0.3f;
        }

        transform.Translate(v * curAcc * speed * Time.deltaTime);


        //Casper again. Zorgt voor het op neer effect tijdens lopen
        if (Vector2.SqrMagnitude(new Vector2(v.x, v.z)) > 0)
        {
            if (mayJump == true)
            {
                transform.GetChild(0).transform.localPosition = new Vector3(0, 0.381f + (Mathf.PingPong(Time.time / 1.4f, 0.1f) * curAcc * headBob), 0);
            } else {
                body.velocity = new Vector3(velocity.x,body.velocity.y,velocity.z);
            }
        }
    }

    //jumping
    private void Jump()
    {
        CheckFloor();
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
        //Fastfall door Casper
        if (mayJump == false)
        {
            if (body.velocity.y > 0)
            {
                if (Input.GetAxis("Jump") == 0)
                {
                    if (hookShotJumping == false)
                    {
                        body.velocity = new Vector3(velocity.x, Mathf.Lerp(body.velocity.y, 0, Time.deltaTime * 10), velocity.z);
                    }
                }
            }
            else if (hookShotJumping == false)
            {
                body.velocity = new Vector3(velocity.x, Mathf.Lerp(body.velocity.y, -9.81f, Time.deltaTime * 3), velocity.z);
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
        SetCrosser();
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
