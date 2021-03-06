﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviourSingleton<Global> {
    public static Global me;
    public GameObject world;
    public Player player;
    public GameObject startmenu;
    public GameObject canvas;
    public GameObject openpanel;
    public GameObject tutorial;
    public Camera camera;
    public bool gameon;
    public bool gamestarted;
    public string message;
    public bool sendmessage;
    public Desk desk;
    public Bed bed;
    // Use this for initialization
    private void Awake()
    {
        me = this;
    }
    void Start () {
        openpanel = tutorial;
        sendmessage = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (sendmessage)
        {
            desk.DisplayOrder(message);
            sendmessage = false;
            message = "";
        }
        if (Input.GetKeyDown(KeyCode.Escape) && gamestarted)
        {
           gameon = true;
        }
        if (gameon)
        {
            world.SetActive(true);
            startmenu.SetActive(false);
            if (openpanel != null)
            {
                openpanel.SetActive(true);
                if (openpanel.name == "forge_panel" && tutorial.GetComponent<Tutorial>().forgefinished == false && tutorial.GetComponent<Tutorial>().curtutorial != 1)
                {
                    tutorial.GetComponent<Tutorial>().curtutorial = 1;
                    tutorial.GetComponent<Tutorial>().place = 0;
                }
                if (openpanel.name == "anvil_panel" && tutorial.GetComponent<Tutorial>().anvilfinished == false && tutorial.GetComponent<Tutorial>().curtutorial != 2)
                {
                    tutorial.GetComponent<Tutorial>().curtutorial = 2;
                    tutorial.GetComponent<Tutorial>().place = 0;
                }
                if (openpanel.name == "detailing_panel" && tutorial.GetComponent<Tutorial>().detailfinished == false && tutorial.GetComponent<Tutorial>().curtutorial != 3)
                {
                    tutorial.GetComponent<Tutorial>().curtutorial = 3;
                    tutorial.GetComponent<Tutorial>().place = 0;
                }
                if (openpanel.name == "library_panel" && tutorial.GetComponent<Tutorial>().libraryfinished == false && tutorial.GetComponent<Tutorial>().curtutorial != 4)
                {
                    tutorial.GetComponent<Tutorial>().curtutorial = 4;
                    tutorial.GetComponent<Tutorial>().place = 0;
                }
            }
            if (camera.transform.position == new Vector3(0, 10, -10) && tutorial.GetComponent<Tutorial>().bedroomfinished == false && tutorial.GetComponent<Tutorial>().curtutorial != 5)
            {
                tutorial.GetComponent<Tutorial>().curtutorial = 5;
                tutorial.GetComponent<Tutorial>().place = 0;
            }
            int i = 0;
            while(i < canvas.transform.childCount)
            {
                if(canvas.transform.GetChild(i).name == "HUD" || (canvas.transform.GetChild(i).name == "Black" && bed.fade))
                {
                    canvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                if(canvas.transform.GetChild(i).name == "Black" && bed.fade == false)
                {
                    canvas.transform.GetChild(i).gameObject.SetActive(false);
                }
                i++;
            }
        }else
        {
            world.SetActive(false);
            int i = 0;
            while (i < canvas.transform.childCount)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
                i++;
            }
            startmenu.SetActive(true);
        }
	}
    public void GameOn()
    {
        if (gameon)
        {
            gameon = false;
        }else
        {            
            gameon = true;
            gamestarted = true;
        }
    }
    public void Quit()
    {
        player.Save();
        Application.Quit();
    }
}
