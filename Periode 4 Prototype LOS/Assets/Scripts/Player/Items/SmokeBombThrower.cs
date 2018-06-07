using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBombThrower : MonoBehaviour
{
    public GameObject smokeGrenade;
    public float throwingForce;

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Throw();
        }

    }

    void Throw()
    {
        //Instantiate object
        GameObject mySmoke = Instantiate(smokeGrenade, transform.position, Quaternion.identity);

        //Add force
        Rigidbody smokeRb = mySmoke.GetComponent<Rigidbody>();
        smokeRb.AddForce(transform.forward * throwingForce, ForceMode.VelocityChange);
    }
}
