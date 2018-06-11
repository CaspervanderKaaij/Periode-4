using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionBar : MonoBehaviour
{

    public List<bool> enemySeers;
    public List<EnemyLOS> enemies;
    int seers = 0;
    private RaycastHit hit;
    public float detectionLevel;
    public Text percentage;

    //the bar in de ui
    public Slider detectSlider;

    // Use this for initialization
    void Start()
    {
        detectionLevel = 0;
        enemies.AddRange(FindObjectsOfType<EnemyLOS>());
        for (int i = 0; i < enemies.Count; i++)
        {
            enemySeers.Add(false);
        }
    }

    /*
    void Update () {
        //raycast voor detection
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50.0f))
        {
            if (hit.transform.tag == "Player")
            {
                //check of detection nog niet max is
                if(detectionLevel < 100.0f)
                {
                    //detection level omhoog
                    print("detecting");
                    detectionLevel += Time.deltaTime * 10;
                    if (detectionLevel > 100)
                    {
                        detectionLevel = 100;
                    }
                }
                // als detection max is, game over
                else
                {
                    print("detected, game over");
                }
            }
            // als je niet detect word, detection level omlaag
            else
            {
                detectionLevel -= Time.deltaTime * 5;
                if(detectionLevel < 0){
                    detectionLevel = 0;
                }
            }
        }
        //update UI
        detectSlider.value = detectionLevel;
        percentage.text = "Detection percentage: " + detectionLevel.ToString("f0");
    }
	 */

    void Update()
    {
        SetGeneralStuff();
        UpdateText();
    }

    void UpdateText()
    {
        detectSlider.value = detectionLevel;
        percentage.text = "Detection percentage: " + detectionLevel.ToString("f0");
    }

    void SetGeneralStuff()
    {
        seers = 0;
        for (int i = 0; i < enemySeers.Count; i++)
        {

            enemySeers[i] = enemies[i].detectionBarBool;
            if (enemies[i] != null)
            {
                if (enemySeers[i] == true)
                {
                    seers++;
                }
            }
            else
            {
                enemySeers[i] = false;
            }
        }

        if (seers == 0)
        {
            OffDetect();
        }
        else
        {
            OnDetect();
        }

    }

    public void OnDetect()
    {
        if (detectionLevel < 100.0f)
        {
            //detection level omhoog
            // print("detecting");
            detectionLevel += Time.deltaTime * 10 * seers;
            if (detectionLevel > 100)
            {
                detectionLevel = 100;
            }
        }
        // als detection max is, game over
        else
        {
            //  print("detected, game over");
            FindObjectOfType<Manager>().Dead();
        }
    }

    public void OffDetect()
    {
        //detectionLevel -= Time.deltaTime * 5;
        if (detectionLevel < 0)
        {
            detectionLevel = 0;
        }
    }
}
