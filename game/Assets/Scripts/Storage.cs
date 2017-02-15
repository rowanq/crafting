using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public Player player;
    public List<GameObject> displayitems;
    public List<GameObject> storageitems;
    public int storageinventorycount = 3;
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
    public void OpenStorage()
    {
        isRunning = true;
        panel.SetActive(true);
    }
    public void CloseStorage()
    {
        isRunning = false;
        panel.SetActive(false);
    }
    void DisplayItems()
    {
        if (player.playeritems.Count != 0)
        {
            int j = 0;
            while (j < player.storagedisplayitems.Count)
            {
                player.storagedisplayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < player.playerinventorycount)
            {
                player.storagedisplayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = player.storagedisplayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = player.storagedisplayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().image;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
        if (storageitems.Count != 0)
        {
            int j = 0;
            while (j < displayitems.Count)
            {
                displayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < storageinventorycount)
            {
                displayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().image;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
    }
}
