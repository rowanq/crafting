using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragStorage : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
    public bool isplayeritem;
    public bool trashon;
    public Collider2D targetlocation;
    public Collider2D playerlocation;
    public Player player;
    public Storage storage;
    // Use this for initialization
    void Start()
    {

    }
    void OnMouseDown()
    {
        trashon = storage.trashon;
        Debug.Log(self.name);
        if (isplayeritem)//goes to storage
        {
            //find gameobject item needs to go to
            bool benormal = true;
            if(item.GetComponent<Item>().name == "Bronze")
            {
                benormal = false;
                if(storage.bronzecount == 0)
                {
                    benormal = true;
                }
                storage.bronzecount++;
            }else if (item.GetComponent<Item>().name == "Iron")
            {
                benormal = false;
                if (storage.ironcount == 0)
                {
                    benormal = true;
                }
                storage.ironcount++;
            }else if (item.GetComponent<Item>().name == "Steel")
            {
                benormal = false;
                if (storage.steelcount == 0)
                {
                    benormal = true;
                }
                storage.steelcount++;
            }
            else if (item.GetComponent<Item>().name == "Titanium")
            {
                benormal = false;
                if (storage.titaniumcount == 0)
                {
                    benormal = true;
                }
                storage.titaniumcount++;
            }
            else if (item.GetComponent<Item>().name == "Handle")
            {
                benormal = false;
                if (storage.handlecount == 0)
                {
                    benormal = true;
                }
                storage.handlecount++;
            }
            else if (item.GetComponent<Item>().name == "Long Handle")
            {
                benormal = false;
                if (storage.longhandlecount == 0)
                {
                    benormal = true;
                }
                storage.longhandlecount++;
            }
            else if (item.GetComponent<Item>().name == "Crest")
            {
                benormal = false;
                if (storage.crestcount == 0)
                {
                    benormal = true;
                }
                storage.crestcount++;
            }
            if (benormal)
            {
                storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
                if (trashon == false)
                {
                    storage.storageinventorycount++;
                }
            }
            //give this item to it
            player.playeritems.Remove(item);
            if (trashon == false)
            {
                storage.storageitems.Add(item);
            }
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
        }
        else if(player.playerinventorycount < 4 && trashon == false)//goes to player
        {
            bool benormal = true;
            if (item.GetComponent<Item>().name == "Bronze")
            {
                benormal = false;
                storage.bronzecount--;
                if (storage.bronzecount == 0)
                {
                    benormal = true;
                }
            }else if (item.GetComponent<Item>().name == "Iron")
            {
                benormal = false;
                storage.ironcount--;
                if (storage.ironcount == 0)
                {
                    benormal = true;
                }
            }
            else if (item.GetComponent<Item>().name == "Steel")
            {
                benormal = false;
                storage.steelcount--;
                if (storage.steelcount == 0)
                {
                    benormal = true;
                }
            }
            else if (item.GetComponent<Item>().name == "Titanium")
            {
                benormal = false;
                storage.titaniumcount--;
                if (storage.titaniumcount == 0)
                {
                    benormal = true;
                }
            }
            else if (item.GetComponent<Item>().name == "Handle")
            {
                benormal = false;
                storage.handlecount--;
                if (storage.handlecount == 0)
                {
                    benormal = true;
                }
            }
            else if (item.GetComponent<Item>().name == "Long Handle")
            {
                benormal = false;
                storage.longhandlecount--;
                if (storage.longhandlecount == 0)
                {
                    benormal = true;
                }
            }
            else if (item.GetComponent<Item>().name == "Crest")
            {
                benormal = false;
                storage.crestcount--;
                if (storage.crestcount == 0)
                {
                    benormal = true;
                }
            }
            if (benormal)
            {
                player.storagedisplayitems[player.playerinventorycount].GetComponent<DragStorage>().item = item;
                storage.storageinventorycount--;
            }
                //find gameobject item needs to go to
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
        }
    }
}
