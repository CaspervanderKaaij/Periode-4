using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapons : MonoBehaviour
{
    [Header("Main variables")]
    public float damage;
    public float range;
    public float mainAmmo;
    public float fireRateInSeconds;
    public Transform playerCam;

    [Header("Effects")]
    public GameObject hitParticles;
    public GameObject gunParticles;
    public AudioSource shootAudioSource;
    public AudioClip shootAudioClip;
    public Animation shootAnimationPlayback;
    public AnimationClip shootAnimationClip;

    [Header("Keep on 0")]
    public float fireRateTimeLeft;
    public float ammoLeft;

    void Start()
    {
        ammoLeft = mainAmmo;
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && fireRateTimeLeft == 0 && ammoLeft > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        //Door Casper:       (ik pak manager niet in start want dit is maar 1 frame actief. Daarom maakt dat niet zo veel uit)      Also deze code in clean asf omg!
        FindObjectOfType<Manager>().TimeStop(0.1f,0);

        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
        {
            //print("You hit: " + hit.transform.name); // Casper - Deed ze uit omdat ze irritant waren.

            if (hit.transform.GetComponent<Target>())
            {
                Target t = hit.transform.GetComponent<Target>();
                t.Damage(hit, damage);
                ShootEffects(hit);
            }
            else
            {
               // print(hit.transform.name + " doesn't have a target script."); // Casper - Deed ze uit omdat ze irritant waren.
            }

            ammoLeft -= 1;
        }
    }

    void ShootEffects(RaycastHit h)
    {
        //Instantiate particles
        Instantiate(hitParticles, h.point, Quaternion.Euler(h.normal));

        if (shootAudioSource && shootAudioClip != null)
        {
            shootAudioSource.clip = shootAudioClip;
            shootAudioSource.Play();
        } else
        {
           // print("shootAudioSource or shootAudioClip is null."); // Casper - Deed ze uit omdat ze irritant waren.
        }


        if (shootAnimationPlayback && shootAnimationClip != null)
        {   
            shootAnimationPlayback.clip = shootAnimationClip;
            shootAnimationPlayback.Play();
        }
        else
        {
            //print("shootAnimationPlayback or shootAnimationClip is null."); // Casper - Deed ze uit omdat ze irritant waren.
        }
    }

    void Reload()
    {

    }

    void Stun()
    {
        
    }
}
