using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAnvil : MonoBehaviour {
    public GameObject self;
    public GameObject item;
    public bool isplayeritem;
    public Collider2D targetlocation;
    public Collider2D playerlocation;
    public Player player;
    public Anvil anvil;
    public bool start; //true is player, false is whatever
    public bool ready = false;
    public int rotated = 0;
    public Vector2 startposition;
    // Use this for initialization
    void Start()
    {
        startposition = transform.position;
    }
    void OnMouseDown()
    {
        if (isplayeritem)//goes to anvil
        {
            //find gameobject item needs to go to
            anvil.displayitems[anvil.anvilinventorycount].GetComponent<DragAnvil>().item = item;
            //give this item to it
            player.playeritems.Remove(item);
            anvil.anvilitems.Add(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < player.playerinventorycount)
            {
                if (self == player.anvildisplayitems[i])//i now equals the place in order
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
                    GameObject curplace = player.anvildisplayitems[placeinline];
                    GameObject nextplace = player.anvildisplayitems[placeinline + 1];
                    curplace.GetComponent<DragAnvil>().item = nextplace.GetComponent<DragAnvil>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount--;
            anvil.anvilinventorycount++;
        }
        else if (player.playerinventorycount < 4)//goes to player
        {
            //find gameobject item needs to go to
            player.anvildisplayitems[player.playerinventorycount].GetComponent<DragAnvil>().item = item;
            player.playeritems.Add(item);
            anvil.anvilitems.Remove(item);
            //check if GAMEOBJECT SELf is last in list
            int i = 0;
            int placeinline = -1;
            while (i < anvil.anvilinventorycount)
            {
                if (self == anvil.displayitems[i])//i now equals the place in order
                {
                    if (i != (anvil.anvilinventorycount - 1))//it is not last in the order
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
                while ((placeinline + 1) < anvil.anvilinventorycount)
                {
                    GameObject curplace = anvil.displayitems[placeinline];
                    GameObject nextplace = anvil.displayitems[placeinline + 1];
                    curplace.GetComponent<DragAnvil>().item = nextplace.GetComponent<DragAnvil>().item;
                    placeinline++;
                }
            }
            player.playerinventorycount++;
            anvil.anvilinventorycount--;
        }
    }
}
