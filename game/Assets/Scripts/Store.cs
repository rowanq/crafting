using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public Player player;
    public List<GameObject> displayitems;
    public int storeinventorycount = 3;
    // Use this for initialization
    void Start () {
        isRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning)
        {
            DisplayItems();
        }
    }
    public void OpenStore()
    {
        isRunning = true;
        panel.SetActive(true);
        Global.me.openpanel = panel;
    }
    public void CloseStore()
    {
        isRunning = false;
        panel.SetActive(false);
        Global.me.openpanel = null;
    }
    void DisplayItems()
    {
        int j = 0;
        while (j < player.storedisplayitems.Count)
        {
            player.storedisplayitems[j].SetActive(false);
            j++;
        }
        int i = 0;
        Debug.Log(player.playerinventorycount);
        while (i < player.playerinventorycount)
        {
            player.storedisplayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = player.storedisplayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = player.storedisplayitems[i].GetComponent<DragStore>().item.GetComponent<Item>().image;
            if (player.playeritems[i].GetComponent<Item>().forgedone && player.playeritems[i].GetComponent<Item>().anvildone == false)
            {
                newsprite = player.storedisplayitems[i].GetComponent<DragStore>().item.GetComponent<Item>().hotimage;
            }
            spriterenderer.sprite = newsprite;
            i++;
        }
        j = 0;
        while (j < displayitems.Count)
        {
            displayitems[j].SetActive(false);
            j++;
        }
        i = 0;
        while (i < storeinventorycount)
        {
            displayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = displayitems[i].GetComponent<DragStore>().item.GetComponent<Item>().image;
            if (displayitems[i].GetComponent<DragStore>().item.GetComponent<Item>().forgedone)
            {
                newsprite = displayitems[i].GetComponent<DragStore>().item.GetComponent<Item>().hotimage;
            }
            spriterenderer.sprite = newsprite;
            i++;
        }
    }
}
