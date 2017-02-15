using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detailing : MonoBehaviour
{
    public bool isRunning;
    public GameObject panel;
    public Player player;
    public List<GameObject> displayitems;
    public List<GameObject> detailingitems;
    public int detailinginventorycount;
    bool rotated;
    string product;
    int product_i;
    public List<Collider2D> connection1;
    public List<Collider2D> connection2;
    public List<bool> connectionmade;
    bool notrotatedproperly = false;
    bool rotationsame; //if true, rotations need to be the same, if not the opposite
    // Use this for initialization
    void Start()
    {
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            DisplayItems();
        }
    }
    public void OpenDetailing()
    {
        isRunning = true;
        panel.active = true;
    }
    public void CloseDetailing()
    {
        isRunning = false;
        panel.active = false;
    }
    public void ReadyDetailing()
    {
        int i = 0;
        while (i < displayitems.Count)
        {
            displayitems[i].GetComponent<DragDetailing>().ready = true;
            if (displayitems[i].active)
            {
                if (displayitems[i].GetComponent<DragDetailing>().rotated % 2 == 1) //odd and therefore rotated
                {
                    rotated = true;
                }
                else //even and therefore not rotated
                {
                    rotated = false;
                }
                if (displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product != "None")
                {
                    product = displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product;
                    product_i = i;
                }
            }
            i++;
        }
        CheckProduct();
    }
    public void UnReadyDetailing()
    {
        CheckRotation();
        if (notrotatedproperly == false)
        {
            Debug.Log("yay");
            CheckPlacement();
        }
        else
        {
            Debug.Log("boo");
        }
        int i = 0;
        while (i < displayitems.Count)
        {
            displayitems[i].GetComponent<DragDetailing>().ready = false;
            displayitems[i].transform.position = displayitems[i].GetComponent<DragDetailing>().startposition;
            i++;
        }
        i = 0;
        while (i < connectionmade.Count)
        {
            connection1.Remove(connection1[i]);
            connection2.Remove(connection2[i]);
            connectionmade.Remove(connectionmade[i]);
            i++;
        }
    }
    void CheckProduct()
    {
        if (product == "Dagger" || product == "Sword")
        {
            rotationsame = true;
            int i = 0;
            while (i < detailinginventorycount)
            {
                if (displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product != "None")
                {
                    int j = 0;
                    while (j < displayitems[i].transform.childCount)
                    {
                        if (displayitems[i].transform.GetChild(j).name == "Bottom")
                        {
                            connection1.Add(displayitems[i].transform.GetChild(j).gameObject.GetComponent<Collider2D>());
                            connectionmade.Add(false);
                        }
                        j++;
                    }
                }
                if (displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product == "None")
                {
                    int j = 0;
                    while (j < displayitems[i].transform.childCount)
                    {
                        if (displayitems[i].transform.GetChild(j).name == "Small_Handle_Top")
                        {
                            connection2.Add(displayitems[i].transform.GetChild(j).gameObject.GetComponent<Collider2D>());
                        }
                        j++;
                    }
                }
                i++;
            }
        }
        else if(product == "Hammer")
        {
            rotationsame = false;
            int i = 0;
            while (i < detailinginventorycount)
            {
                if (displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product != "None")
                {
                    int j = 0;
                    while (j < displayitems[i].transform.childCount)
                    {
                        if (displayitems[i].transform.GetChild(j).name == "Through_Middle")
                        {
                            connection1.Add(displayitems[i].transform.GetChild(j).gameObject.GetComponent<Collider2D>());
                            connectionmade.Add(false);
                        }
                        j++;
                    }
                }
                if (displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product == "None")
                {
                    int j = 0;
                    while (j < displayitems[i].transform.childCount)
                    {
                        if (displayitems[i].transform.GetChild(j).name == "Middle")
                        {
                            connection2.Add(displayitems[i].transform.GetChild(j).gameObject.GetComponent<Collider2D>());
                        }
                        j++;
                    }
                }
                i++;
            }
        }
    }
    void CheckRotation()
    {
        int i = 0;
        int oldr = 1000;
        while (i < detailinginventorycount)
        {
            int r = displayitems[i].GetComponent<DragDetailing>().rotated % 4;
            Debug.Log(i + " is " + r);
            if (i != 0) //not first time
            {
                if (rotationsame)//supposed to be the same
                {
                    if(r != oldr)
                    {
                        notrotatedproperly = true;
                    }
                }else
                {
                    if(r == oldr)
                    {
                        notrotatedproperly = true;
                    }
                }
            }
            else
            {
                oldr = r;
            }
            i++;
        }
    }
    void CheckPlacement()
    {
        int i = 0;
        while (i < connection1.Count)
        {
            if (connection1[i].bounds.Intersects(connection2[i].bounds))
                {
                    connectionmade[i] = true;
                }
            i++;
        }
        int j = 0;
        bool done = true;
        while (j < connectionmade.Count)
        {
            if (connectionmade[j] == false)
            {
                done = false;
            }
            j++;
        }
        if (done)
        {
            displayitems[product_i].GetComponent<DragDetailing>().item.GetComponent<Item>().detailingdone = true;
            i = 0;
            while (i < detailinginventorycount)
            {
                if(i != product_i)
                {
                    detailingitems.Remove(displayitems[i].GetComponent<DragDetailing>().item);
                    displayitems[i].SetActive(false);
                    detailinginventorycount--;
                }
                i++;
            }
        }
    }
    void DisplayItems()
    {
        if (player.playeritems.Count != 0)
        {
            int j = 0;
            while (j < player.detailingdisplayitems.Count)
            {
                player.detailingdisplayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < player.playerinventorycount)
            {
                player.detailingdisplayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = player.detailingdisplayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite =player.detailingdisplayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().image;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
        if (detailingitems.Count != 0)
        {
            int j = 0;
            while (j < displayitems.Count)
            {
                displayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < detailinginventorycount)
            {
                displayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().anvilimage;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
    }
}
