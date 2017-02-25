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
    public GameObject blower;
    public List<Sprite> blowerimages;
    public ParticleSystem smoke;
    float temp;
    int amountofcoal;
    int amountoftimespentatrighttemp;
    bool blowerposition; //false is down, true is up
    int blowerframes = 10;
    
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
            SetUpBlower();
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
        AnimateBlower();
    }
    void SetUpBlower()
    {
        if(Global.me.tutorial.GetComponent<Tutorial>().curtutorial == 1)
        {
            blower.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            blower.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
    void AnimateBlower()
    {
        SpriteRenderer spriterenderer = blower.GetComponent<SpriteRenderer>();
        if(blowerposition == false)
        {
            if(spriterenderer.sprite == blowerimages[2])
            {
                blowerframes = 0;
            }
            blowerframes++;
            if(blowerframes < 5)
            {
                spriterenderer.sprite = blowerimages[1];
            }else
            {
                spriterenderer.sprite = blowerimages[0];
            }
        }else
        {
            if(spriterenderer.sprite == blowerimages[0])
            {
                blowerframes = 0;
            }
            blowerframes++;
            if (blowerframes > 5)
            {
                spriterenderer.sprite = blowerimages[2];
            }else
            {
                spriterenderer.sprite = blowerimages[1];
            }
        }
    }
    void CheckBar()
    {
        int i = 0;
        bool smelting = false;
        while (i < forgeitems.Count)
        {
            if (forgeitems[i].GetComponent<Item>().forgemintemp < temp && temp < forgeitems[i].GetComponent<Item>().forgemaxtemp && forgeitems[i].GetComponent<Item>().forgedone == false)
            {
                forgeitems[i].GetComponent<Item>().forgeprogress++;
                smelting = true;
            }
            i++;
        }
        if (smelting)
        {
            smoke.enableEmission = true;
            smoke.Emit(5);
        }
        else
        {
            smoke.enableEmission = false;
        }
    }
    public void GetTemperature()
    {
        Debug.Log(temp);
    }
    public void OpenForge()
    {
        isRunning = true;
        panel.SetActive(true);
        Global.me.openpanel = panel;
    }
    public void CloseForge()
    {
        isRunning = false;
        panel.SetActive(false);
        Global.me.openpanel = null;
    }
    void DisplayItems()
    {
        int j = 0;
        while (j < player.forgedisplayitems.Count)
        {
            player.forgedisplayitems[j].SetActive(false);
            j++;
        }
        int i = 0;
        while (i < player.playerinventorycount)
        {
            player.forgedisplayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = player.forgedisplayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = player.forgedisplayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().image;
            if (player.playeritems[i].GetComponent<Item>().forgedone && player.playeritems[i].GetComponent<Item>().anvildone == false)
            {
                newsprite = player.forgedisplayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().hotimage;
            }
            spriterenderer.sprite = newsprite;
            i++;
        }
        j = 0;
        while (j < displayitems.Count)
        {
            displayitems[j].SetActive(false);
            j++;
        }
        i = 0;
        while (i < forgeinventorycount)
        {
            displayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = displayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().image;
            if (displayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().forgedone && displayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().anvildone == false)
            {
                newsprite = displayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().hotimage;
            }
            spriterenderer.sprite = newsprite;
            i++;
        }
    }

}
