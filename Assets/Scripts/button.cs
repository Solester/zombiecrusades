using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    private GameObject GameMenu;
    public GameObject GameMenu2;
    private GameObject ObjGear;

    private bool isPaused;
    
    void Start()
    {
        GameMenu = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        ObjGear = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        GameMenu.SetActive(false);       
        isPaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            GameMenu.SetActive(true);
            ObjGear.SetActive(false);
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            GameMenu.SetActive(false);
            ObjGear.SetActive(true);
            isPaused = false;
        }

        if (isPaused)
        {
           Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Gear()
    {
        ObjGear.SetActive(false);
        GameMenu.SetActive(true);
        isPaused = true;



    }

    public void Resume()
    {
        ObjGear.SetActive(true);
        GameMenu.SetActive(false);
        isPaused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}