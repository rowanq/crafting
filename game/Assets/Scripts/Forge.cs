using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forge : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public float tempdecay;
    public float blowerincrease;
    public Text displayTemp;
    public Slider display;
    public Player player;
    public List<GameObject> displayitems;
    public List<GameObject> forgeitems;
    public int forgeinventorycount = 0;
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
	void FixedUpdate () {
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
            if (forgeitems[i].GetComponent<Item>().forgemintemp < temp && temp < forgeitems[i].GetComponent<Item>().forgemaxtemp)
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
            while (j < player.forgedisplayitems.Count)
            {
                player.forgedisplayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < player.playeritems.Count)
            {
                player.forgedisplayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = player.forgedisplayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = player.playeritems[i].GetComponent<Item>().image;
                if (player.playeritems[i].GetComponent<Item>().forgedone)
                {
                    newsprite = player.playeritems[i].GetComponent<Item>().hotimage;
                }
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
            while (i < forgeinventorycount)
            {
                displayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = displayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().image;
                if (displayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().forgedone)
                {
                    newsprite = displayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().hotimage;
                }
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
    }

}
