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

        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
        {
            print("You hit: " + hit.transform.name);

            if (hit.transform.GetComponent<Target>())
            {
                Target t = hit.transform.GetComponent<Target>();
                t.Damage(hit, damage);
                ShootEffects(hit);
            }
            else
            {
                print(hit.transform.name + " doesn't have a target script.");
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
            print("shootAudioSource or shootAudioClip is null.");
        }


        if (shootAnimationPlayback && shootAnimationClip != null)
        {   
            shootAnimationPlayback.clip = shootAnimationClip;
            shootAnimationPlayback.Play();
        }
        else
        {
            print("shootAnimationPlayback or shootAnimationClip is null.");
        }
    }

    void Reload()
    {

    }

    void Stun()
    {
        
    }
}
