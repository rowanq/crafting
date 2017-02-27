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
    public bool ready = false;
    public int rotated = 0;
    public Vector2 startposition;
    // Use this for initialization
    void Start()
    {
        startposition = transform.position;
    }
    void OnMouseDrag()
    {
        if (isplayeritem == false)
        {
            if (ready == false && Input.GetKey(KeyCode.LeftShift))
            {
                Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
                transform.position = objposition;
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    transform.Rotate(0, 0, 90);
                    rotated--;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    transform.Rotate(0, 0, -90);
                    rotated++;
                }
            }
        }
    }
    void OnMouseDown()
    {
        if (isplayeritem && anvil.product == "None" && anvil.anvilinventorycount < 4)//goes to anvil
        {
            //find gameobject item needs to go to
            item.GetComponent<Item>().isplayeritem = false;
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
        else if (player.playerinventorycount < 4 && ready == false && Input.GetKey(KeyCode.LeftShift) == false && anvil.product == "None")//goes to player
        {
            //find gameobject item needs to go to
            item.GetComponent<Item>().isplayeritem = true;
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
