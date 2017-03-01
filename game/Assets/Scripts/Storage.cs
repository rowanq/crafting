using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public Player player;
    public bool trashon;
    public GameObject trashcursor;
    public List<GameObject> displayitems;
    public List<GameObject> storageitems;
    public int storageinventorycount = 3;
    public int bronzecount = 0;
    public int ironcount = 0;
    public int steelcount = 0;
    public int titaniumcount = 0;
    public int handlecount = 0;
    public int longhandlecount = 0;
    public int crestcount = 0;
    // Use this for initialization
    void Start () {
        isRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning)
        {
            if (trashon)
            {
                trashcursor.SetActive(true);
                TrashCursorRun();
            }else
            {
                trashcursor.SetActive(false);
            }
            DisplayItems();
        }
	}
    public void OpenStorage()
    {
        isRunning = true;
        panel.SetActive(true);
        Global.me.openpanel = panel;
    }
    public void CloseStorage()
    {
        isRunning = false;
        panel.SetActive(false);
        Global.me.openpanel = null;
    }
    public void TrashOn()
    {
        if (trashon)
        {
            trashon = false;
        }
        else if (player.playerinventorycount > 0)
        {
            trashon = true;
        }
    }
    void TrashCursorRun()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        trashcursor.transform.position = objposition;
    }
    void DisplayItems()
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
            if (player.playeritems[i].GetComponent<Item>().forgedone && player.playeritems[i].GetComponent<Item>().anvildone == false)
            {
                newsprite = player.storagedisplayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().hotimage;
            }
            spriterenderer.sprite = newsprite;
            i++;
        }
        j = 0;
        while (j < displayitems.Count)
        {
            displayitems[j].SetActive(false);
            displayitems[j].transform.GetChild(0).gameObject.SetActive(false);
            j++;
        }
        i = 0;
        while (i < storageinventorycount)
        {
            displayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().image;
            if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().forgedone && displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().anvildone == false)
            {
                newsprite = displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().hotimage;
            }
            if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().product == "None")
            {
                if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().name == "Bronze")
                {
                    displayitems[i].transform.GetChild(0).gameObject.SetActive(true);
                    displayitems[i].transform.GetChild(0).GetComponent<Text>().text = bronzecount.ToString();
                }
                else if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().name == "Iron")
                {
                    displayitems[i].transform.GetChild(0).gameObject.SetActive(true);
                    displayitems[i].transform.GetChild(0).GetComponent<Text>().text = ironcount.ToString();
                }
                else if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().name == "Steel")
                {
                    displayitems[i].transform.GetChild(0).gameObject.SetActive(true);
                    displayitems[i].transform.GetChild(0).GetComponent<Text>().text = steelcount.ToString();
                }
                else if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().name == "Titanium")
                {
                    displayitems[i].transform.GetChild(0).gameObject.SetActive(true);
                    displayitems[i].transform.GetChild(0).GetComponent<Text>().text = titaniumcount.ToString();
                }
                else if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().name == "Handle")
                {
                    displayitems[i].transform.GetChild(0).gameObject.SetActive(true);
                    displayitems[i].transform.GetChild(0).GetComponent<Text>().text = handlecount.ToString();
                }
                else if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().name == "Long Handle")
                {
                    displayitems[i].transform.GetChild(0).gameObject.SetActive(true);
                    displayitems[i].transform.GetChild(0).GetComponent<Text>().text = longhandlecount.ToString();
                }
                else if (displayitems[i].GetComponent<DragStorage>().item.GetComponent<Item>().name == "Crest")
                {
                    displayitems[i].transform.GetChild(0).gameObject.SetActive(true);
                    displayitems[i].transform.GetChild(0).GetComponent<Text>().text = crestcount.ToString();
                }
            }
            spriterenderer.sprite = newsprite;
            i++;
        }
    }
}
