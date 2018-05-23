using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LookUpDown : MonoBehaviour {
    private Vector3 r;
    [SerializeField] private float rotSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look()
    {
        r.x += -Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;//Time.deltaTime toegevoegt door Casper
        r.x = Mathf.Clamp(r.x, -50.0f, 50.0f);
        transform.eulerAngles = (new Vector3(r.x, transform.eulerAngles.y, 0.0f));
    }
}
