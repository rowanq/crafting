using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragForce : MonoBehaviour
{
    public GameObject self;
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
            Debug.Log("Forge");
            GameObject newdisplayitem = player.forgedisplayitems.Find(myitem => myitem.Equals(self));
            int i = 0;
            bool found = false;
            GameObject newitem = new GameObject();
            while (i < player.playeritems.Count || found == false)
            {
                if (player.playeritems[i].GetComponent<Item>().displayitem = newdisplayitem)
                {
                    newitem = player.playeritems[i];
                    player.playeritems.Remove(newitem);
                    forge.forgeitems.Add(newitem);
                    found = true;
                }
                i++;
            }
            transform.position = startposition;
            self.SetActive(false);

        }
        if (playerlocation.OverlapPoint(mouseposition))//is it in the player's inventory?
        {
            //do something related to the thing
            Debug.Log("Player");
            GameObject newdisplayitem = forge.displayitems.Find(myitem => myitem.Equals(self));
            int i = 0;
            GameObject newitem = new GameObject();
            while (i < forge.forgeitems.Count)
            {
                if (forge.forgeitems[i].GetComponent<Item>().displayitem = newdisplayitem)
                {
                    newitem = forge.forgeitems[i];
                    forge.forgeitems.Remove(newitem);
                    player.playeritems.Add(newitem);
                }
                i++;
            }
            transform.position = startposition;
            self.SetActive(false);
        }
    }
}
