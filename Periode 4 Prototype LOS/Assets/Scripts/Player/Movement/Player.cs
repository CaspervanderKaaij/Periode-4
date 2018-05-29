using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Move
{

    public enum State
    {
        Normal,
        HookShot
    }
    public GameObject mainCam;
    public float speed;
    public State curState = State.Normal;
    private Vector3 hookShotGoal = Vector3.zero;
    private float hookShotSpeed = 1;
    void Start()
    {
        RealStart();
        mainCam = FindObjectOfType<Camera>().gameObject;
    }

    void Update()
    {
        switch (curState)
        {

            case State.Normal:
                RealWalk();
                //if(Input.GetButtonDown("Fire2")){
                    //zorg voor de schreeuw dingen.
               // }
                break;
            case State.HookShot:
                HookShoting();
                break;
        }
    }

    void RealWalk()
    {
        Vector3 camEuler = mainCam.transform.eulerAngles;
        mainCam.transform.eulerAngles = new Vector3(0, mainCam.transform.eulerAngles.y, 0);
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float anal = Vector2.SqrMagnitude(new Vector2(inputs.x, inputs.y));
        anal = Mathf.Min(anal, 1);
        Walk(false, mainCam.transform.TransformDirection(new Vector3(inputs.x, 0, inputs.y) * anal * speed));
        mainCam.transform.eulerAngles = camEuler;
    }

    public void HookShotStart(Vector3 goal,float speed){
        curState = State.HookShot;
        hookShotGoal = goal + new Vector3(0,1.33f,0);
        hookShotSpeed = speed;
    }

    void HookShoting(){
        if(transform.position != hookShotGoal){
            transform.position = Vector3.MoveTowards(transform.position,hookShotGoal,hookShotSpeed * Time.deltaTime);
        } else {
            curState = State.Normal;
        }
    }
}
