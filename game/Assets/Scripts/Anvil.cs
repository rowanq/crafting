using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public Player player;
    public bool hammer;
    public GameObject realhammer;
    public bool weld;
    public GameObject realweld;
    public List<GameObject> displayitems;
    public List<GameObject> anvilitems;
    public List<GameObject> targets;
    public float placementscore;
    public float hitscore;
    public int anvilinventorycount;
    int numberofbarsused;
    bool rotated;
    string product;
    public List<Collider2D> placestohit;
    public List<bool> placeshit;
    public List<Collider2D> placestoweld;
    public List<Collider2D> placestoweldto;
    public List<bool> placeswelt;
    int lastplaceclicked = -1;
    bool clicked = false;
    int framesclicked = 0;
	// Use this for initialization
	void Start () {
        isRunning = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isRunning)
        {
            DisplayItems();
            if (hammer)
            {
                AnimateHammer();
                if (Input.GetMouseButtonDown(0))
                {
                    clicked = true;
                    if (anvilinventorycount> 0)
                    {
                        CheckHit();
                    }
                }
            }
            if (weld)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (anvilinventorycount > 0)
                    {
                        CheckWeld();
                    }
                }
            }
        }
	}
    void AnimateHammer()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        realhammer.transform.position = objposition;
        if (clicked)
        {
            framesclicked++;
            if (framesclicked < 6)
            {
                realhammer.transform.Rotate(0, 0, 18);
            }
            else if (framesclicked < 11)
            {
                realhammer.transform.Rotate(0, 0, -18);
            }
            else
            {
                clicked = false;
                framesclicked = 0;
            }
        }
    }
    public void OpenAnvil()
    {
        isRunning = true;
        panel.active = true;
    }
    public void CloseAnvil()
    {
        isRunning = false;
        panel.active = false;
    }
    public void ReadyAnvil()
    {
        int i = 0;
        while (i < displayitems.Count)
        {
            displayitems[i].GetComponent<DragAnvil>().ready = true;
            if (displayitems[i].active)
            {
                numberofbarsused++;
                Debug.Log(displayitems[i].GetComponent<DragAnvil>().rotated);
                Debug.Log(displayitems[i].GetComponent<DragAnvil>().rotated % 2);
                if (displayitems[i].GetComponent<DragAnvil>().rotated%2 != 0) //odd and therefore rotated
                {
                    rotated = true;
                }
                else //even and therefore not rotated
                {
                    rotated = false;
                }
            }
            i++;
        }
        CheckProduct();
        CheckPlacement();
    }
    public void UnReadyAnvil()
    {
        int i = 0;
        while (i < displayitems.Count)
        {
            displayitems[i].GetComponent<DragAnvil>().ready = false;
            i++;
        }
    }
    public void GetHammer()
    {
        if (hammer)
        {
            hammer = false;
        }else
        {
            hammer = true;
        }

    }
    public void GetWeld()
    {
        if (weld)
        {
            weld = false;
        }else
        {
            weld = true;
        }
    }
    void CheckProduct()
    {
        if (numberofbarsused == 1)
        {
            if (rotated == false)
            {
                product = "Dagger";
                int i = displayitems[0].transform.childCount - 1;
                while (i > -1)
                {
                    if(displayitems[0].transform.GetChild(i).name == "MidLeftX")
                    {
                        placestohit.Add(displayitems[0].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        placeshit.Add(false);
                    }
                    if (displayitems[0].transform.GetChild(i).name == "MidRightX")
                    {
                        placestohit.Add(displayitems[0].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        placeshit.Add(false);
                    }
                    i--;
                }
            }
            else
            {
                product = "Hammer";
                int i = displayitems[0].transform.childCount - 1;
                while (i > -1)
                {
                    if (displayitems[0].transform.GetChild(i).name == "MidBottom")
                    {
                        placestohit.Add(displayitems[0].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        placeshit.Add(false);
                    }
                    if (displayitems[0].transform.GetChild(i).name == "MidTop")
                    {
                        placestohit.Add(displayitems[0].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        placeshit.Add(false);
                    }
                    if (displayitems[0].transform.GetChild(i).name == "Middle")
                    {
                        placestohit.Add(displayitems[0].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        placeshit.Add(false);
                    }
                    i--;
                }
            }
        }
        else if(numberofbarsused == 2)
        {
            if (rotated == false)
            {
                product = "Sword";
                int j = 0;
                while (j < numberofbarsused)
                {
                    bool addedaweld = false;
                    int i = displayitems[j].transform.childCount - 1;
                    while (i > -1)
                    {
                        if (displayitems[j].transform.GetChild(i).name == "MidLeftX")
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidRightX")
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && addedaweld == false)
                        {
                            if (placestoweldto.Count > placestoweld.Count || placestoweld.Count == 0) //more those than these
                            {
                                placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeswelt.Add(false);
                                addedaweld = true;
                            }
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop" && addedaweld == false)
                        {
                            if (placestoweld.Count > placestoweldto.Count || placestoweldto.Count == 0) //more those than these
                            {
                                placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                addedaweld = true;
                            }
                        }
                        i--;
                    }
                    j++;
                }
                Debug.Log(placestohit.Count);

            }
        }
    }
    void CheckPlacement()
    {
        Debug.Log("Making a "+product);
        if (product == "Dagger" || product == "Hammer")
        {
            placementscore = 100;
        }
    }
    void CheckHit()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
        Vector2 mouses = mouse -  new Vector2(150, 0);
        if (product == "Dagger" || product == "Hammer" || product == "Sword")
        {
            int i = 0;
            while (i < placestohit.Count)
            {
                if (placestohit[i].OverlapPoint(mouse))
                {
                    placeshit[i] = true;
                }
                i++;
            }
            int j = 0;
            bool done = true;
            while (j < placeshit.Count)
            {
                if (placeshit[j] == false)
                {
                    done = false;
                }
                j++;
            }
            Debug.Log(done+"  "+ anvilitems[0].GetComponent<Item>().anvildone);
            if (done && anvilitems[0].GetComponent<Item>().anvildone == false)
            {
                anvilitems[0].GetComponent<Item>().anvildone = true;
                anvilitems[0].GetComponent<Item>().product = product;
                if (product == "Sword")
                {
                    anvilitems.Remove(displayitems[1].GetComponent<DragAnvil>().item);
                    Debug.Log(displayitems[1].transform.name);
                    displayitems[1].SetActive(false);
                    anvilinventorycount--;
                    displayitems[1].transform.position = displayitems[1].GetComponent<DragAnvil>().startposition;
                }
            }
        }
    }
    void CheckWeld()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
        if (product == "Sword")
        {
            int i = 0;
            while (i < placestoweld.Count)
            {
                if (placestoweldto[i].OverlapPoint(mouse))
                {
                    if (lastplaceclicked > -1)
                    {
                        if (lastplaceclicked == i)
                        {
                            placeswelt[i] = true;
                            lastplaceclicked = -1;
                        }
                    }
                }
                if (placestoweld[i].OverlapPoint(mouse))
                {
                    if (lastplaceclicked == -1)
                    {
                        lastplaceclicked = i;
                    }
                }
                i++;
            }
            int j = 0;
            bool done = true;
            while (j < placeswelt.Count)
            {
                if (placeswelt[j] == false)
                {
                    done = false;
                }
                j++;
            }
            if (done)
            {
                displayitems[1].transform.position = new Vector2(displayitems[0].transform.position.x, displayitems[0].transform.position.y - 2.1f);
                Debug.Log("wtf");
            }
        }
    }
    void DisplayItems()
    {
        if (player.playeritems.Count != 0)
        {
            int j = 0;
            while (j < player.anvildisplayitems.Count)
            {
                player.anvildisplayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < player.playeritems.Count)
            {
                player.anvildisplayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = player.anvildisplayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = player.playeritems[i].GetComponent<Item>().image;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
        if (anvilitems.Count != 0)
        {
            int j = 0;
            while (j < displayitems.Count)
            {
                displayitems[j].SetActive(false);
                j++;
            }
            int i = 0;
            while (i < anvilinventorycount)
            {
                displayitems[i].SetActive(true);
                SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
                Sprite newsprite = displayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().anvilimage;
                spriterenderer.sprite = newsprite;
                i++;
            }
        }
    }
}
