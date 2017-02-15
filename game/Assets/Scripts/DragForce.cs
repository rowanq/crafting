using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragForce : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
    public bool isplayeritem;
    public Collider2D targetlocation;
    public Collider2D playerlocation;
    public Player player;
    public Forge forge;
    Vector2 startposition;
    // Use this for initialization
    void Start()
    {
        startposition = transform.position;
    }
    void OnMouseDown()
    {
        if (isplayeritem)//goes to forge
        {
            //find gameobject item needs to go to
            forge.displayitems[forge.forgeinventorycount].GetComponent<DragForce>().item = item;
            //give this item to it
            player.playeritems.Remove(item);
            forge.forgeitems.Add(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < player.playerinventorycount)
            {
                if (self == player.forgedisplayitems[i])//i now equals the place in order
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
                    GameObject curplace = player.forgedisplayitems[placeinline];
                    GameObject nextplace = player.forgedisplayitems[placeinline + 1];
                    curplace.GetComponent<DragForce>().item = nextplace.GetComponent<DragForce>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount--;
            forge.forgeinventorycount++;
        }
        else if (player.playerinventorycount < 4)//goes to player
        {
            //find gameobject item needs to go to
            player.forgedisplayitems[player.playerinventorycount].GetComponent<DragForce>().item = item;
            player.playeritems.Add(item);
            forge.forgeitems.Remove(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < forge.forgeinventorycount)
            {
                if (self == forge.displayitems[i])//i now equals the place in order
                {
                    if (i != (forge.forgeinventorycount - 1))//it is not last in the order
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
                while ((placeinline + 1) < forge.forgeinventorycount)
                {
                    GameObject curplace = forge.displayitems[placeinline];
                    GameObject nextplace = forge.displayitems[placeinline + 1];
                    curplace.GetComponent<DragForce>().item = nextplace.GetComponent<DragForce>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount++;
            forge.forgeinventorycount--;
        }
    }
}
