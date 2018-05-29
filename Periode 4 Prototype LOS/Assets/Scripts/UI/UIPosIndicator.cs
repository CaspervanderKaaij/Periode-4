using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPosIndicator : MonoBehaviour
{

    RectTransform rect;
    public Transform target;
    public Transform from;
    [Range(0, 1)]
    public float fill = 0;
    public List<RectTransform> fillBars;
    private Vector3 barStartScale;
    EnemyLOS enemyLOS;
    public GameObject[] visibleObjects;

    void Start()
    {
        rect = transform.GetComponent<RectTransform>();
        barStartScale = fillBars[0].localScale;
        enemyLOS = target.GetComponent<EnemyLOS>();
    }

    void Update()
    {
        Kill();
        SetAngle();
        SetFill();
        SetBar();
        SetVisible();
    }

    void Kill()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    void SetFill()
    {
        if (enemyLOS != null)
        {
            if (enemyLOS.spotted == true)
            {
                fill = enemyLOS.timer / enemyLOS.seeTime;
            }
            else
            {
                fill = enemyLOS.timer / enemyLOS.loseTime;
            }
        }
    }

    void SetBar()
    {
        for (int i = 0; i < fillBars.Count; i++)
        {
            fillBars[i].localScale = new Vector3(barStartScale.x * fill, barStartScale.y, barStartScale.z);
        }
    }
    void SetAngle()
    {
        if (target != null)
        {
            float angle = Mathf.Atan2(-from.position.x - -target.position.x, from.position.z - target.position.z) * Mathf.Rad2Deg;
            rect.eulerAngles = new Vector3(0, 0, angle + Camera.main.transform.eulerAngles.y + 180);
        }
    }
    void SetVisible()
    {
        if (fill == 0)
        {
            for (int i = 0; i < visibleObjects.Length; i++)
            {
                visibleObjects[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < visibleObjects.Length; i++)
            {
                visibleObjects[i].SetActive(true);
            }
        }
    }
}
