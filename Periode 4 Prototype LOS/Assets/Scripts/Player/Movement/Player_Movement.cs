using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
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

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Jump();
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    //collision for jump reset
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Terrain" || c.gameObject.tag == "Car")
        {
            jumpCurrent = 0;
            mayJump = true;
        }
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
}
