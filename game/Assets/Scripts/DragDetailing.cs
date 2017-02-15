using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDetailing : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
    public bool isplayeritem;
    public Collider2D targetlocation;
    public Collider2D playerlocation;
    public Player player;
    public Detailing detailing;
    public bool start; //true is player, false is whatever
    public bool ready = false;
    public int rotated = 0;
    Vector2 startposition;
    // Use this for initialization
    void Start()
    {
        startposition = transform.position;
    }
    void OnMouseDown()
    {
        if (isplayeritem)//goes to detailing
        {
            //find gameobject item needs to go to
            detailing.displayitems[detailing.detailinginventorycount].GetComponent<DragDetailing>().item = item;
            //give this item to it
            player.playeritems.Remove(item);
            detailing.detailingitems.Add(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < player.playerinventorycount)
            {
                if (self == player.detailingdisplayitems[i])//i now equals the place in order
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
                    GameObject curplace = player.detailingdisplayitems[placeinline];
                    GameObject nextplace = player.detailingdisplayitems[placeinline + 1];
                    curplace.GetComponent<DragDetailing>().item = nextplace.GetComponent<DragDetailing>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount--;
            detailing.detailinginventorycount++;
        }
        else if (player.playerinventorycount < 4)//goes to player
        {
            //find gameobject item needs to go to
            player.detailingdisplayitems[player.playerinventorycount].GetComponent<DragDetailing>().item = item;
            player.playeritems.Add(item);
            detailing.detailingitems.Remove(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < detailing.detailinginventorycount)
            {
                if (self == detailing.displayitems[i])//i now equals the place in order
                {
                    if (i != (detailing.detailinginventorycount - 1))//it is not last in the order
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
                while ((placeinline + 1) < detailing.detailinginventorycount)
                {
                    GameObject curplace = detailing.displayitems[placeinline];
                    GameObject nextplace = detailing.displayitems[placeinline + 1];
                    curplace.GetComponent<DragDetailing>().item = nextplace.GetComponent<DragDetailing>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount++;
            detailing.detailinginventorycount--;
        }
    }
}
