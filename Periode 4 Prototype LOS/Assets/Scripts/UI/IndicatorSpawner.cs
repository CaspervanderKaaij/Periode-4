using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawner : MonoBehaviour
{

    public GameObject IndicatorPrefab;
    void Start()
    {
        LOS[] enemiesLOS = GameObject.FindObjectsOfType<LOS>();
        RectTransform canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        for (int i = 0; i < enemiesLOS.Length; i++)
        {
            GameObject los = Instantiate(IndicatorPrefab, canvas.position, canvas.rotation,canvas.transform);
            los.GetComponent<UIPosIndicator>().from = FindObjectOfType<PlayerController>().transform;
            los.GetComponent<UIPosIndicator>().target = enemiesLOS[i].transform;
        }
		Destroy(gameObject);
    }

}
