using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public int curSelected;
    Transform mainCam;
    public Transform[] camPositions;
    public RectTransform[] cursorPositions;
    public RectTransform cursor;
    private bool canPress = true;
    public float camPosSpeed;
    public float camRotSpeed;
    public Image fadeIn;
	public GameObject loadObject;
	

    public enum MenuOptions
    {
        NewGame,
        LoadGame,
        Options,
        Quit
    }
    public MenuOptions[] menuOptions;

    void Start()
    {
        mainCam = Camera.main.transform;
        mainCam.position = camPositions[curSelected].position;
        mainCam.rotation = camPositions[curSelected].rotation;
        fadeIn.color = new Color(fadeIn.color.r, fadeIn.color.g, fadeIn.color.b, 1);
    }

    void Update()
    {
        SetSelected();
        SetCamPos();
        SetCursor();
        OnPress();
        FadeIn();
		MouseSelect();
    }

    void FadeIn()
    {
        //fadeIn.color = Color.Lerp(fadeIn.color,Color.clear,Time.deltaTime * 4);
        fadeIn.color = new Color(fadeIn.color.r, fadeIn.color.g, fadeIn.color.b, Mathf.Lerp(fadeIn.color.a, 0, Time.deltaTime * 3));
    }

    void SetSelected()
    {
        if (Vector3.Distance(mainCam.position, camPositions[curSelected].position) < 1f)
        {
            if (-Input.GetAxis("Vertical") != 0)
            {
                int dir = 0;
                if (-Input.GetAxis("Vertical") > 0)
                {
                    dir = 1;
                }
                else
                {
                    dir = -1;
                }
                if (canPress == true)
                {
                    curSelected += 1 * dir;

                    if (curSelected > cursorPositions.Length - 1)
                    {
                        curSelected = 0;
                    }

                    if (curSelected < 0)
                    {
                        curSelected = cursorPositions.Length - 1;
                    }
                    canPress = false;
                }
            }
            else
            {
                canPress = true;
            }
        }
    }

    void SetCamPos()
    {
        mainCam.position = Vector3.Lerp(mainCam.position, camPositions[curSelected].position, Time.deltaTime * camPosSpeed);
        mainCam.rotation = Quaternion.Lerp(mainCam.rotation, camPositions[curSelected].rotation, Time.deltaTime * camRotSpeed);
    }

    void SetCursor()
    {
        cursor.position = cursorPositions[curSelected].position;
    }

    void OnPress()
    {
        if (Input.GetButtonDown("Fire1") == true)
        {
            switch (menuOptions[curSelected])
            {

                case MenuOptions.NewGame:
					SceneManager.LoadScene(1);
					loadObject.SetActive(true);
                    break;
                case MenuOptions.LoadGame:
					SceneManager.LoadScene(0);
					loadObject.SetActive(true);
                    break;
                case MenuOptions.Options:
					//uhm..
                    break;
                case MenuOptions.Quit:
                    Application.Quit();
                    break;
            }
            Debug.Log(menuOptions[curSelected]);
        }
    }

	void MouseSelect(){
		Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin,Camera.main.ScreenPointToRay(Input.mousePosition).direction);
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100,~9)){
			curSelected = int.Parse(hit.transform.name);
		}
	}
}
