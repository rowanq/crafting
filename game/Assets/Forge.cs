using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forge : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public int tempdecay;
    public int blowerincrease;
    public Text displayTemp;
    public Slider display;
    public Player player;
    public List<GameObject> displayitems;
    public List<GameObject> forgeitems;
    float bronzemintemp = 40;
    float bronzemaxtemp = 60;
    float temp;
    int amountofcoal;
    int amountoftimespentatrighttemp;
    bool blowerposition; //false is down, true is up
    
	// Use this for initialization
	void Start () {
        isRunning = false;
        temp = 0;
        blowerposition = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning)
        {
            RunBlower();
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log(temp);
            }
            displayTemp.text = temp.ToString();
            display.value = temp;
            DisplayItems();
            CheckBar();
        }
        temp -= tempdecay;
        if (temp <= 0)
        {
            temp = 0;
        }

    }
    void RunBlower()
    {
        if (blowerposition)//blower is up
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                blowerposition = false;
                temp += blowerincrease;
            }
        }
        else//blower is down
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                blowerposition = true;
            }
        }
    }
    void CheckBar()
    {
        int i = 0;
        while (i < forgeitems.Count)
        {
            if (bronzemintemp < temp && temp < bronzemaxtemp)
            {
                forgeitems[i].GetComponent<Item>().forgeprogress++;
            }
            i++;
        }
    }
    public void GetTemperature()
    {
        Debug.Log(temp);
    }
    public void OpenForge()
    {
        isRunning = true;
        panel.active = true;
    }
    public void CloseForge()
    {
        isRunning = false;
        panel.active = false;
    }
    void DisplayItems()
    {
        if (player.playeritems.Count != 0)
        {
            int j = 0;
            while (j < player.displayitems.Count)
            {
                player.displayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < player.playeritems.Count)
            {
                player.displayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = player.displayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = player.playeritems[i].GetComponent<SpriteRenderer>().sprite;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
        if (forgeitems.Count != 0)
        {
            int j = 0;
            while (j < displayitems.Count)
            {
                displayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < forgeitems.Count)
            {
                displayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = forgeitems[i].GetComponent<SpriteRenderer>().sprite;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
    }

}
