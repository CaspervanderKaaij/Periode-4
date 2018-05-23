using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public enum Objective
    {
        Collect,
        GoHome
    }
    private bool start = true;
    public GameObject startText;
    public Objective curObjective = Objective.Collect;
    public Text objectiveText;
    void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        startText.SetActive(true);
    }

    void Update()
    {
        //Debug.Log(FindObjectsOfType<Treasure>().Length);
        switch (curObjective)
        {

            case Objective.Collect:
                if (FindObjectsOfType<Treasure>().Length == 0)
                {
                    //Debug.Log("Collect");
                    curObjective = Objective.GoHome;
                }
                objectiveText.text = "Objective:       Get the treasure.";
                break;

            case Objective.GoHome:
                objectiveText.text = "Objective:           Go home.";
                // Debug.Log("GoHome");
                break;
        }

        if(start == true){
            if(Input.anyKeyDown == true){
                start = false;
                Time.timeScale = 1;
                startText.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void MissionComplete()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
}
