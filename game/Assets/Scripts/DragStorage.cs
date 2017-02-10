using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragStorage : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
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
    void OnMouseDrag()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        transform.position = objposition;
    }
    void OnMouseUp()
    {
        //Debug.Log("a"+transform.name);
        Vector2 mouseposition = new Vector2(transform.position.x, transform.position.y);
        if (targetlocation.OverlapPoint(mouseposition))//is it in the target place?
        {
            //do something related to the thing
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            player.playeritems.Remove(item);
            storage.storageitems.Add(item);
            //deal with the game object you left behind in old menu
            if ((player.playerinventorycount - 1) <= player.playeritems.Count)
            {
                item = player.storagedisplayitems[player.playerinventorycount - 1].GetComponent<DragStorage>().item;
                //Debug.Log("x" + player.storagedisplayitems[player.playerinventorycount - 1].transform.name);
            }
            else
            {
                item = null;
            }
            player.playerinventorycount--;
            storage.storageinventorycount++;
        }
        if (playerlocation.OverlapPoint(mouseposition))//is it in the player's inventory?
        {
            //do something related to the thing
            //set the newitem to display that item
            player.storagedisplayitems[player.playerinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageitems.Remove(item);
            player.playeritems.Add(item);
            //deal with old game object
            if ((player.playerinventorycount + 1) <= storage.storageitems.Count)
            {
                item = storage.displayitems[player.playerinventorycount + 1].GetComponent<DragStorage>().item;
                //Debug.Log("y" + storage.displayitems[player.playerinventorycount + 1].transform.name);
            }
            else
            {
                item = null;
            }
            player.playerinventorycount++;
            storage.storageinventorycount--;
        }
        transform.position = startposition;
        self.SetActive(false);
    }
}
