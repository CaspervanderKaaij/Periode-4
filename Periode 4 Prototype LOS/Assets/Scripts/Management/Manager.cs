using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public enum Objective
    {
        Collect,
        GoHome,
        Complete
    }

    private bool start = true;
    public GameObject startText;
    public Objective curObjective = Objective.Collect;
    public Text objectiveText;
    public bool paused = false;
    [HideInInspector]
    public float timeScale = 1;
    public int nextLevel = 0;

    void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        startText.SetActive(true);
    }

    void Update()
    {

        TimeScaleStuff();
        ObjectiveStuff();
    }

    void ObjectiveStuff()
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


        // voor je begint heb je text en zo. Dat is een soort objective, dis ik zet dat hier neer.
        if (start == true)
        {
            if (Input.anyKeyDown == true)
            {
                start = false;
                //Time.timeScale = 1;
                timeScale = 1;
                startText.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void TimeScaleStuff()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // TimeStop(0.4f,0.1f);
        }

        if (curObjective != Objective.Complete)
        {
            if (Input.GetButtonDown("Start"))
            {
                paused = !paused;
            }
        }
        SetTimeScale(timeScale);
    }

    public void TimeStop(float for_, float newScale)
    {
        SetTimeScale(newScale);
        StartCoroutine(TimeBack(for_));
    }

    IEnumerator TimeBack(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SetTimeScale(1);
    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale;
        if (paused == false)
        {
            Time.timeScale = timeScale;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void MissionComplete()
    {
        // Time.timeScale = 0;
        curObjective = Objective.Complete;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(nextLevel);
    }

    public void Dead(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
