using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSmokeBomb : MonoBehaviour
{
    public float timeToExplode;
    private float countdownToExplode;

    public float timeToDestroySmoke;
    private float countdownToDestroySmoke;

    public float timeToDestroyBomb;
    private float countdownToDestroyBomb;

    public float smokeRadius;

    public GameObject smokeSphere;
    public GameObject smokeParticles;

    private bool hasExploded;

    void Start()
    {
        smokeSphere.transform.localScale *= smokeRadius;

        countdownToExplode = timeToExplode;
        countdownToDestroySmoke = timeToDestroySmoke;
        countdownToDestroyBomb = timeToDestroyBomb;

        smokeSphere.SetActive(false);
        smokeParticles.SetActive(false);
    }

    void Update()
    {
        countdownToExplode -= Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (countdownToExplode <= 0 && hasExploded == false)
        {
            Explode();
        }

        countdownToDestroyBomb -= Time.deltaTime;

        if (countdownToDestroyBomb <= 0)
        {
            Destroy(gameObject);
        }

        if (hasExploded == true)
        {
            countdownToDestroySmoke -= Time.deltaTime;

            if (countdownToDestroySmoke <= 0)
            {
                smokeSphere.SetActive(false);
                smokeParticles.SetActive(false);
            }
        }
    }

    void Explode()
    {
        smokeSphere.SetActive(true);
        hasExploded = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Rigidbody smokeRigidbody = transform.GetComponent<Rigidbody>();
        smokeRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        smokeRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        smokeParticles.SetActive(true);
    }

    //
}
