using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragStorage : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
    public bool isplayeritem;
    public Collider2D targetlocation;
    public Collider2D playerlocation;
    public Player player;
    public Storage storage;
    Vector2 startposition;
    // Use this for initialization
    void Start()
    {
        startposition = transform.position;
    }
    void OnMouseDown()
    {
        Debug.Log(self.name);
        if (isplayeritem)//goes to storage
        {
            //find gameobject item needs to go to
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            //give this item to it
            player.playeritems.Remove(item);
            storage.storageitems.Add(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < player.playerinventorycount)
            {
                if (self == player.storagedisplayitems[i])//i now equals the place in order
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
                    GameObject curplace = player.storagedisplayitems[placeinline];
                    GameObject nextplace = player.storagedisplayitems[placeinline + 1];
                    curplace.GetComponent<DragStorage>().item = nextplace.GetComponent<DragStorage>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount--;
            storage.storageinventorycount++;
        }
        else if(player.playerinventorycount < 4)//goes to player
        {
            //find gameobject item needs to go to
            player.storagedisplayitems[player.playerinventorycount].GetComponent<DragStorage>().item = item;
            player.playeritems.Add(item);
            storage.storageitems.Remove(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < storage.storageinventorycount)
            {
                if (self == storage.displayitems[i])//i now equals the place in order
                {
                    if (i != (storage.storageinventorycount - 1))//it is not last in the order
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
                while ((placeinline + 1) < storage.storageinventorycount)
                {
                    GameObject curplace = storage.displayitems[placeinline];
                    GameObject nextplace = storage.displayitems[placeinline + 1];
                    curplace.GetComponent<DragStorage>().item = nextplace.GetComponent<DragStorage>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount++;
            storage.storageinventorycount--;
        }
    }
}
