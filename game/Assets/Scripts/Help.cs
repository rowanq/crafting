using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour {
    public Player player;
    public Collider2D self;
    public Image selfs;
    public GameObject help;
    public GameObject items;
    public GameObject level;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        help.SetActive(false);
            int i = 0;
            while (i < player.huddisplayitems.Count)
            {
              player.huddisplayitems[i].SetActive(false);
              i++;
            }
                items.SetActive(false);
        level.SetActive(false);
        if (player.canmove)
        {
            selfs.enabled = true;
            transform.GetChild(0).GetComponent<Text>().enabled = true;
            Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
            if (self.OverlapPoint(mouse))
            {
                help.SetActive(true);
                items.SetActive(true);
                DisplayHUDitems();
                level.SetActive(true);
                level.GetComponent<Text>().text = "Level: " + player.level;
            }
        }
        else
        {
            selfs.enabled = false;
            transform.GetChild(0).GetComponent<Text>().enabled = false;
        }
    }
    void DisplayHUDitems()
    {
        int i = 0;
        while (i < player.playerinventorycount)
        {
            player.huddisplayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = player.huddisplayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = player.huddisplayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().image;
            if (player.playeritems[i].GetComponent<Item>().forgedone && player.playeritems[i].GetComponent<Item>().anvildone == false)
            {
                newsprite = player.huddisplayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().hotimage;
            }
            spriterenderer.sprite = newsprite;
            i++;
        }
    }
}
