using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDetailing : MonoBehaviour
{
    public GameObject self;
    public GameObject item;
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
    void OnMouseDrag()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        transform.position = objposition;
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
        //Debug.Log("a"+transform.name);
        Vector2 mouseposition = new Vector2(transform.position.x, transform.position.y);
        if (targetlocation.OverlapPoint(mouseposition) && ready == false)//is it in the target place?
        {
            //do something related to the thing
            detailing.displayitems[detailing.detailinginventorycount].GetComponent<DragDetailing>().item = item;
            player.playeritems.Remove(item);
            detailing.detailingitems.Add(item);
            //deal with the game object you left behind in old menu
            if ((player.playerinventorycount - 1) <= player.playeritems.Count)
            {
                item = player.detailingdisplayitems[player.playerinventorycount - 1].GetComponent<DragDetailing>().item;
            }
            else
            {
                item = null;
            }
            player.playerinventorycount--;
            detailing.detailinginventorycount++;
        }
        if (playerlocation.OverlapPoint(mouseposition) && ready == false)//is it in the player's inventory?
        {
            //do something related to the thing
            //set the newitem to display that item
            player.detailingdisplayitems[player.playerinventorycount].GetComponent<DragDetailing>().item = item;
            detailing.detailingitems.Remove(item);
            player.playeritems.Add(item);
            //deal with old game object
            if ((player.playerinventorycount + 1) <= detailing.detailingitems.Count)
            {
                item = detailing.displayitems[player.playerinventorycount + 1].GetComponent<DragDetailing>().item;
            }
            else
            {
                item = null;
            }
            player.playerinventorycount++;
            detailing.detailinginventorycount--;
        }
        if (ready == false)
        {
            transform.position = startposition;
            self.SetActive(false);
        }
    }
}
