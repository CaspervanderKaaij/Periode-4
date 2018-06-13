using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActivator : MonoBehaviour
{

    public enum Tutorial
    {
        Jump,
        Death,
        HookShot,
        SwitchItem,
        SmokeBomb,
        Star,
    }
    public Tutorial curTutorial = Tutorial.Jump;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (curTutorial)
            {

                case Tutorial.Jump:
                    FindObjectOfType<TutorialManager>().curTutorial = TutorialManager.Tutorial.Jump;
                    break;
                case Tutorial.HookShot:
                    FindObjectOfType<TutorialManager>().curTutorial = TutorialManager.Tutorial.HookShot;
                    break;
                case Tutorial.SmokeBomb:
                    FindObjectOfType<TutorialManager>().curTutorial = TutorialManager.Tutorial.SmokeBomb;
                    break;
                case Tutorial.Star:
                    FindObjectOfType<TutorialManager>().curTutorial = TutorialManager.Tutorial.Star;
                    break;
                case Tutorial.Death:
                    FindObjectOfType<TutorialManager>().curTutorial = TutorialManager.Tutorial.Death;
                    transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case Tutorial.SwitchItem:
                    FindObjectOfType<TutorialManager>().curTutorial = TutorialManager.Tutorial.SwitchItem;
                    break;
            }
            if(curTutorial != Tutorial.Death){
                Destroy(gameObject);
            }
        }
    }
}
