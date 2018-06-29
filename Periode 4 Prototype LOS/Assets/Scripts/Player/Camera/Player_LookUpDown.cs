using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LookUpDown : MonoBehaviour
{
    private Vector3 r;
    [SerializeField] private float rotSpeed;
    //plyr stuff door Casper
    PlayerController plyr;

    //Screenshake door Casper
    bool shaking = true;
    Transform toShake;
    Vector3 oldPos;
    public float shakeAmount = 0.3f;

    void Start()
    {
        plyr = FindObjectOfType<PlayerController>();
        shaking = false;
        toShake = Camera.main.transform;
        oldPos = toShake.localPosition;
    }

    void Update()
    {
        if (plyr.curState != PlayerController.State.Cutscene)
        {
            Look();
        }

        //screenshake door Casper

        toShake.localPosition = oldPos;
        // if (Input.GetButtonDown("Fire1"))
        //  {
        if (plyr.curState == PlayerController.State.HookShotEnd)
        {
            StartShake(0.2f);
        }
        // }
        if (shaking == true)
        {
            if (Time.timeScale != 1)
            {
                Shake();
            }
        }
        else
        {
            oldPos = toShake.localPosition;
        }
    }

    private void Look()
    {
        r.x += -Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;//Time.deltaTime toegevoegt door Casper
        r.x = Mathf.Clamp(r.x, -50.0f, 50.0f);
        transform.eulerAngles = (new Vector3(r.x, transform.eulerAngles.y, 0.0f));
    }

    void Shake()
    {
        oldPos = toShake.localPosition;
        toShake.localPosition += new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount));
    }

    public void StartShake(float time)
    {
        shaking = true;
        StopAllCoroutines();
        StartCoroutine(StopShake(time));
    }

    IEnumerator StopShake(float time)
    {
        yield return new WaitForSeconds(time);
        shaking = false;
    }
}
