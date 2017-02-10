using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAnvil : MonoBehaviour {
    public GameObject self;
    public GameObject item;
    public Collider2D targetlocation;
    public Collider2D playerlocation;
    public Player player;
    public Anvil anvil;
    public bool start; //true is player, false is whatever
    public bool ready = false;
    public int rotated = 0;
    Vector2 startposition;
    // Use this for initialization
    void Start()
    {
        startposition = transform.position;
    }
    void OnMouseDrag()
    {
        if (ready)
        {
            Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
            transform.position = objposition;
        }
        if (start == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.Rotate(0, 0, 90);
                rotated--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, -90);
                rotated++;
            }
        }
    }
    void OnMouseUp()
    {
        Vector2 mouseposition = new Vector2(transform.position.x, transform.position.y);
        if (targetlocation.OverlapPoint(mouseposition) && start != false && ready == false)//is it in the target place?
        {
            //do something related to the thing
            anvil.displayitems[anvil.anvilinventorycount].GetComponent<DragAnvil>().item = item;
            player.playeritems.Remove(item);
            anvil.anvilitems.Add(item);
            //deal with the game object you left behind in old menu
            if ((player.playerinventorycount - 1) <= player.playeritems.Count)
            {
                item = player.anvildisplayitems[player.playerinventorycount - 1].GetComponent<DragAnvil>().item;
            }
            else
            {
                item = null;
            }
            player.playerinventorycount--;
            anvil.anvilinventorycount++;
            transform.position = startposition;
            self.SetActive(false);
        }
        if (playerlocation.OverlapPoint(mouseposition) && start != true && ready == false)//is it in the player's inventory?
        {
            //do something related to the thing
            //set the newitem to display that item
            player.anvildisplayitems[player.playerinventorycount].GetComponent<DragAnvil>().item = item;
            anvil.anvilitems.Remove(item);
            player.playeritems.Add(item);
            //deal with old game object
            if ((player.playerinventorycount + 1) <= anvil.anvilitems.Count)
            {
                item = anvil.displayitems[player.playerinventorycount + 1].GetComponent<DragForce>().item;
            }
            else
            {
                item = null;
            }
            player.playerinventorycount++;
            anvil.anvilinventorycount--;
            transform.position = startposition;
            self.SetActive(false);
        }
        if (ready == false)
        {
            transform.position = startposition;
            self.SetActive(false);
        }
    }
}
