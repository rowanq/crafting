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
    }
    public void CloseStore()
    {
        isRunning = false;
        panel.SetActive(false);
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
            spriterenderer.sprite = newsprite;
            i++;
        }
    }
}
