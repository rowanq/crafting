using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragForce : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
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
    void OnMouseDrag()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        transform.position = objposition;
    }
    void OnMouseUpAsButton()
    {
        Vector2 mouseposition = new Vector2(transform.position.x, transform.position.y);
        if (targetlocation.OverlapPoint(mouseposition))//is it in the target place?
        {
            //do something related to the thing
            forge.displayitems[forge.forgeinventorycount].GetComponent<DragForce>().item = item;
            player.playeritems.Remove(item);
            forge.forgeitems.Add(item);
            //deal with the game object you left behind in old menu
            if ((player.playerinventorycount - 1) <= player.playeritems.Count)
            {
                item = player.forgedisplayitems[player.playerinventorycount - 1].GetComponent<DragForce>().item;
            }
            else
            {
                item = null;
            }
            player.playerinventorycount--;
            forge.forgeinventorycount++;
            transform.position = startposition;
            self.SetActive(false);
            Debug.Log(player.playerinventorycount);
        }
        if (playerlocation.OverlapPoint(mouseposition))//is it in the player's inventory?
        {
            //do something related to the thing
            //set the newitem to display that item
            player.forgedisplayitems[player.playerinventorycount].GetComponent<DragForce>().item = item;
            forge.forgeitems.Remove(item);
            player.playeritems.Add(item);
            //deal with old game object
            if ((player.playerinventorycount + 1) <= forge.forgeitems.Count)
            {
                item = forge.displayitems[player.playerinventorycount + 1].GetComponent<DragForce>().item;
            }
            else
            {
                item = null;
            }
            player.playerinventorycount++;
            forge.forgeinventorycount--;
            transform.position = startposition;
            self.SetActive(false);
        }
        transform.position = startposition;
        self.SetActive(false);
    }
}
