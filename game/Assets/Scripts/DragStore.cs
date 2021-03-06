﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragStore : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
    public bool isplayeritem;
    public Collider2D targetlocation;
    public Collider2D playerlocation;
    public Player player;
    public Store store;
    // Use this for initialization
    void Start()
    {

    }
    void OnMouseOver()
    {
        if(isplayeritem == false)
        {
            self.transform.GetChild(0).gameObject.SetActive(true);
            self.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = item.GetComponent<Item>().name + "\n" + (int)Mathf.Round(item.GetComponent<Item>().buyprice * 1.5f);
        }
    }
    void OnMouseExit()
    {
        if(isplayeritem == false)
        {
            self.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnMouseDown()
    {
        if (isplayeritem)//goes to storage
        {
            //find gameobject item needs to go to
            if(item.GetComponent<Item>().product == "None")
            {
                //give this item to it
                player.playeritems.Remove(item);
                item.GetComponent<DragStore>().item.GetComponent<Item>().isplayeritem = false;
                //check if GAMEOBJECT SELf is last in list
                int i = 0;
                int placeinline = -1;
                while (i < player.playerinventorycount)
                {
                    if (self == player.storedisplayitems[i])//i now equals the place in order
                    {
                        if (i != (player.playerinventorycount - 1))//it is not last in the order
                        {
                            placeinline = i;
                        }
                        else //is last
                        {
                            self.SetActive(false);
                        }
                    }
                    i++;
                }
                if (placeinline != -1)//its not last and you need to do something
                {
                    while ((placeinline + 1) < player.playerinventorycount)
                    {
                        GameObject curplace = player.storedisplayitems[placeinline];
                        GameObject nextplace = player.storedisplayitems[placeinline + 1];
                        curplace.GetComponent<DragStore>().item = nextplace.GetComponent<DragStore>().item;
                        placeinline++;
                    }
                }
                player.playerinventorycount--;
            }
            else if(player.lawyer)
            {
                if (item.GetComponent<Item>().detailingdone)
                {
                    //give this item to it
                    player.money += (int)(item.GetComponent<Item>().price * 0.5f);
                    player.playeritems.Remove(item);
                    item.GetComponent<DragStore>().item.GetComponent<Item>().isplayeritem = false;
                    //check if GAMEOBJECT SELf is last in list
                    int i = 0;
                    int placeinline = -1;
                    while (i < player.playerinventorycount)
                    {
                        if (self == player.storedisplayitems[i])//i now equals the place in order
                        {
                            if (i != (player.playerinventorycount - 1))//it is not last in the order
                            {
                                placeinline = i;
                            }
                            else //is last
                            {
                                self.SetActive(false);
                            }
                        }
                        i++;
                    }
                    if (placeinline != -1)//its not last and you need to do something
                    {
                        while ((placeinline + 1) < player.playerinventorycount)
                        {
                            GameObject curplace = player.storedisplayitems[placeinline];
                            GameObject nextplace = player.storedisplayitems[placeinline + 1];
                            curplace.GetComponent<DragStore>().item = nextplace.GetComponent<DragStore>().item;
                            placeinline++;
                        }
                    }
                    player.playerinventorycount--;
                }
                else
                {
                    Debug.Log("You can't sell this.");
                }
            }else
            {
                Debug.Log("You can't sell this");
            }
        }
        else if (player.playerinventorycount < 4)//goes to player
        {
            //find gameobject item needs to go to
            if(player.money > (Mathf.Round(item.GetComponent<Item>().buyprice*1.5f)))
            {
                if(item.GetComponent<Item>().upgrade == false)
                {
                    GameObject newitem = Instantiate(item);
                    player.storedisplayitems[player.playerinventorycount].GetComponent<DragStore>().item = newitem;
                    newitem.GetComponent<Item>().isplayeritem = true;
                    newitem.GetComponent<Item>().player = player;
                    player.playeritems.Add(newitem);
                    player.playerinventorycount++;
                }else
                {
                    if(item.GetComponent<Item>().name == "Mitt")
                    {
                        player.ovenmitts = true;
                    }
                    if(item.GetComponent<Item>().name == "Hot Forge")
                    {
                        player.hotforge = true;
                    }
                    if(item.GetComponent<Item>().name == "Lawyer")
                    {
                        player.lawyer = true;
                    }
                    if(item.GetComponent<Item>().name == "Yarn")
                    {
                        player.yarn = true;
                    }
                    //check if GAMEOBJECT SELf is last in list
                    int i = 0;
                    int placeinline = -1;
                    while (i < store.storeinventorycount)
                    {
                        if (self == store.displayitems[i])//i now equals the place in order
                        {
                            if (i != (store.storeinventorycount - 1))//it is not last in the order
                            {
                                placeinline = i;
                            }
                            else //is last
                            {
                                self.SetActive(false);
                            }
                        }
                        i++;
                    }
                    if (placeinline != -1)//its not last and you need to do something
                    {
                        while ((placeinline + 1) < store.storeinventorycount)
                        {
                            GameObject curplace = store.displayitems[placeinline];
                            GameObject nextplace = store.displayitems[placeinline + 1];
                            curplace.GetComponent<DragStore>().item = nextplace.GetComponent<DragStore>().item;
                            placeinline++;
                        }
                    }
                    store.storeinventorycount--;
                }
                player.money -= (int)Mathf.Round(item.GetComponent<Item>().buyprice*1.5f);
            }else
            {
                Global.me.sendmessage = true;
                Global.me.message = "You can't afford that!";
            }
        }
    }
}
