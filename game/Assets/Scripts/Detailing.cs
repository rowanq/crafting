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
    public List<Collider2D> connection1;
    public List<Collider2D> connection2;
    public List<bool> connectionmade;
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
                }
            }
            i++;
        }
        CheckProduct();
    }
    public void UnReadyDetailing()
    {
        CheckPlacement();
        int i = 0;
        while (i < displayitems.Count)
        {
            displayitems[i].GetComponent<DragDetailing>().ready = false;
            i++;
        }
    }
    void CheckProduct()
    {
        if (product == "Dagger" || product == "Sword")
        {
            int i = 0;
            while (i < detailinginventorycount)
            {
                if (displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product == "Dagger")
                {
                    int j = displayitems[i].transform.childCount - 1;
                    while (j > -1)
                    {
                        if (displayitems[0].transform.GetChild(i).name == "Bottom")
                        {
                            connection1.Add(displayitems[i].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            connectionmade.Add(false);
                        }
                        j--;
                    }
                }
                if (displayitems[i].GetComponent<DragDetailing>().item.GetComponent<Item>().product == "None")
                {
                    int j = displayitems[i].transform.childCount - 1;
                    while (j > -1)
                    {
                        if (displayitems[0].transform.GetChild(i).name == "Small_Handle_Top")
                        {
                            connection2.Add(displayitems[i].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        }
                        j--;
                    }
                }
                i++;
            }
        }
    }
    void CheckPlacement()
    {
        int i = 0;
        while (i < connection1.Count)
        {
            if (connection1[i].IsTouching(connection2[i]))
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
            displayitems[0].GetComponent<DragDetailing>().item.GetComponent<Item>().detailingdone = true;
            detailingitems.Remove(displayitems[1].GetComponent<DragDetailing>().item);
            displayitems[1].SetActive(false);
            detailinginventorycount--;
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
