using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public Player player;
    public bool hammer;
    public GameObject realhammer;
    public List<GameObject> displayitems;
    public List<GameObject> anvilitems;
    public List<GameObject> targets;
    public float placementscore;
    public float hitscore;
    public int anvilinventorycount;
    int numberofbarsused;
    bool rotated;
    string product;
    public List<Collider2D> placestohit;
    public List<bool> placeshit;
    bool clicked = false;
    int framesclicked = 0;
	// Use this for initialization
	void Start () {
        isRunning = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isRunning)
        {
            DisplayItems();
            if (hammer)
            {
                AnimateHammer();
                if (Input.GetMouseButtonDown(0))
                {
                    clicked = true;
                    if (anvilinventorycount> 0)
                    {
                        CheckHit();
                    }
                }
            }
        }
	}
    void AnimateHammer()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        realhammer.transform.position = objposition;
        if (clicked)
        {
            framesclicked++;
            if (framesclicked < 6)
            {
                Debug.Log("a"+framesclicked);
                realhammer.transform.Rotate(0, 0, 18);
            }
            else if (framesclicked < 11)
            {
                Debug.Log("b" + framesclicked);
                realhammer.transform.Rotate(0, 0, -18);
            }
            else
            {
                clicked = false;
                framesclicked = 0;
            }
        }
    }
    public void OpenAnvil()
    {
        isRunning = true;
        panel.active = true;
    }
    public void CloseAnvil()
    {
        isRunning = false;
        panel.active = false;
    }
    public void ReadyAnvil()
    {
        int i = 0;
        while (i < displayitems.Count)
        {
            displayitems[i].GetComponent<DragAnvil>().ready = true;
            if (displayitems[i].active)
            {
                numberofbarsused++;
                if (displayitems[i].GetComponent<DragAnvil>().rotated%2 == 1) //odd and therefore rotated
                {
                    rotated = true;
                }
                else //even and therefore not rotated
                {
                    rotated = false;
                }
            }
            i++;
        }
        CheckProduct();
        CheckPlacement();
    }
    public void UnReadyAnvil()
    {
        int i = 0;
        while (i < displayitems.Count)
        {
            displayitems[i].GetComponent<DragAnvil>().ready = false;
            i++;
        }
    }
    public void GetHammer()
    {
        if (hammer)
        {
            hammer = false;
        }else
        {
            hammer = true;
        }

    }
    void CheckProduct()
    {
        if (numberofbarsused == 1)
        {
            if (rotated == false)
            {
                product = "Dagger";
                //GameObject newdisplayitem = player.anvildisplayitems.Find(myitem => myitem.Equals(self));
                int i = displayitems[0].transform.childCount - 1;
                while (i > -1)
                {
                    if(displayitems[0].transform.GetChild(i).name == "MidLeftX")
                    {
                        placestohit.Add(displayitems[0].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        placeshit.Add(false);
                    }
                    if (displayitems[0].transform.GetChild(i).name == "MidRightX")
                    {
                        placestohit.Add(displayitems[0].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        placeshit.Add(false);
                    }
                    i--;
                }
            }
            else
            {
                product = "Hammer";
            }
        }
    }
    void CheckPlacement()
    {
        Debug.Log("Making a "+product);
        if (product == "Dagger")
        {
            placementscore = 100;
        }
    }
    void CheckHit()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
        if (product == "Dagger")
        {
            int i = 0;
            while (i < placestohit.Count)
            {
                if (placestohit[i].OverlapPoint(mouse))
                {
                    placeshit[i] = true;
                }
                i++;
            }
            int j = 0;
            bool done = true;
            while (j < placeshit.Count)
            {
                if (placeshit[j] == false)
                {
                    done = false;
                }
                j++;
            }
            if (done)
            {
                anvilitems[0].GetComponent<Item>().anvildone = true;
                anvilitems[0].GetComponent<Item>().product = "Dagger";
            }
        }
    }
    void DisplayItems()
    {
        if (player.playeritems.Count != 0)
        {
            int j = 0;
            while (j < player.anvildisplayitems.Count)
            {
                player.anvildisplayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < player.playeritems.Count)
            {
                player.anvildisplayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = player.anvildisplayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = player.playeritems[i].GetComponent<Item>().image;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
        if (anvilitems.Count != 0)
        {
            int j = 0;
            while (j < displayitems.Count)
            {
                displayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < anvilinventorycount)
            {
                Debug.Log(i);
                displayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = displayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().anvilimage;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
    }
}
