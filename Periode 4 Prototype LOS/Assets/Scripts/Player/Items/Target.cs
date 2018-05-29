using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyColliders
{
    public string name;
    public float multiplier;
    public Collider collider;
}

public class Target : MonoBehaviour
{
    public float mainHealth;
    private float healthLeft;
    public List<MyColliders> colliders = new List<MyColliders>();

    void Start()
    {
        healthLeft = mainHealth;
    }

    public void Damage(RaycastHit hit, float damage)
    {
        foreach (MyColliders hitCollider in colliders)
        {
            if (hit.collider == hitCollider.collider)
            {
                damage *= hitCollider.multiplier;
                print("You hit the collider " + hitCollider.name + " and it multiplied the damage by " + hitCollider.multiplier.ToString());
                break;
            }
        }

        healthLeft -= damage;

        if (healthLeft <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
