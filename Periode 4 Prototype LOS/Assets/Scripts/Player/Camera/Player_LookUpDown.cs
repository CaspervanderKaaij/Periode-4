using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LookUpDown : MonoBehaviour
{
    private Vector3 r;
    [SerializeField] private float rotSpeed;
    //plyr stuff door Casper
    PlayerController plyr;


    void Start()
    {
        plyr = FindObjectOfType<PlayerController>();

    }

    void Update()
    {
        if (plyr.curState != PlayerController.State.Cutscene)
        {
            Look();
        }

    }

    private void Look()
    {
        r.x += -Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;//Time.deltaTime toegevoegt door Casper
        r.x = Mathf.Clamp(r.x, -50.0f, 50.0f);
        transform.eulerAngles = (new Vector3(r.x, transform.eulerAngles.y, 0.0f));
    }

    
}
