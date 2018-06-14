using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public enum Tutorial
    {
        Walk,
        Jump,
        Death,
        HookShot,
        SwitchItem,
        SmokeBomb,
        Star,
        None
    }

    public Tutorial curTutorial = Tutorial.Walk;
    public Transform uiTutorial;
    public bool uiVisible = true;
    Text txt;
    PlayerController plyr;
    public float deathTimer = 15;
    WeaponSwitcher wpn;
    private Tutorial tutBuffer = Tutorial.None;//als de tutorial klaar is, zet dit het naar een nieuwe
    bool textBuffer = false;



    void Start()
    {
        txt = uiTutorial.GetChild(2).GetComponent<Text>();
        plyr = FindObjectOfType<PlayerController>();
        wpn = FindObjectOfType<WeaponSwitcher>();
    }

    void FixedUpdate()
    {
        SetVisible();
        CompleteCondition();
    }
    void LateUpdate()
    {
        CompleteCondition();
        SetText();
    }

    void SetVisible()
    {
        if (curTutorial == Tutorial.None)
        {
            uiVisible = false;
        }
        else
        {
            uiVisible = true;
        }
        if (uiVisible == true)
        {
            uiTutorial.localPosition = Vector3.MoveTowards(uiTutorial.localPosition, new Vector3(0, -225, 0), Time.deltaTime * 10000);
        }
        else
        {
            uiTutorial.localPosition = Vector3.MoveTowards(uiTutorial.localPosition, new Vector3(1442, -225, 0), Time.deltaTime * 10000);
        }
    }

    void SetText()
    {
        switch (curTutorial)
        {
            case Tutorial.Walk:
                txt.text = "Walk: WASD";
                break;
            case Tutorial.Jump:
                txt.text = "Jump: spacebar";
                break;
            case Tutorial.HookShot:
                txt.text = "Hookshot: left click";
                break;
            case Tutorial.SwitchItem:
                txt.text = "Switch Item: scroll";
                break;
            case Tutorial.SmokeBomb:
                txt.text = "SmokeBomb: left click";
                break;
            case Tutorial.Star:
                txt.text = "Throw star: left click";
                break;
            case Tutorial.Death:
                txt.text = "Don't get spotted! If the Detection percentage is at 100% it's game over.";
                break;
            case Tutorial.None:
                txt.text = "";
                break;
        }
    }

    void CompleteCondition()
    {
        switch (curTutorial)
        {
            case Tutorial.Walk:
                if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) != 0)
                {
                    Complete();
                }
                break;
            case Tutorial.Jump:
                if (Input.GetButtonDown("Jump"))
                {
                    Complete();
                }
                break;
            case Tutorial.HookShot:
                if (plyr.curState == PlayerController.State.HookShot)
                {
                    Complete();
                }
                if (wpn.curWeapon != 0)
                {
                    curTutorial = Tutorial.SwitchItem;
                    tutBuffer = Tutorial.HookShot;
                }
                break;
            case Tutorial.Death:
                deathTimer -= Time.deltaTime;
                if (deathTimer < 0)
                {
                    Complete();
                }
                break;
            case Tutorial.SwitchItem:
                if (Vector2.SqrMagnitude(Input.mouseScrollDelta) != 0)
                {
                    Complete();
                }
                break;
            case Tutorial.Star:
                if (wpn.curWeapon == 1)
                {
                    tutBuffer = Tutorial.SmokeBomb;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Complete();
                    }
                }
                else
                {
                    curTutorial = Tutorial.SwitchItem;
                    tutBuffer = Tutorial.Star;
                }
                break;
            case Tutorial.SmokeBomb:
                if (wpn.curWeapon == 2)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Complete();
                    }
                }
                else
                {
                    curTutorial = Tutorial.SwitchItem;
                    tutBuffer = Tutorial.SmokeBomb;
                }
                break;
        }
    }

    void Complete()
    {
        curTutorial = tutBuffer;
        tutBuffer = Tutorial.None;
    }
}
