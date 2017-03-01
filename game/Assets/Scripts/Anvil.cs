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
    public GameObject check;
    public List<GameObject> checks;
    int numberofbarsused;
    List<bool> rotated = new List<bool>();
    int numberrotated = 0;
    public string product;
    public List<Collider2D> placestohit;
    public List<bool> placeshit;
    public List<Collider2D> bars;
    int numberofmisses = 0;
    public List<Collider2D> placestoweld;
    public List<Collider2D> placestoweldto;
    public List<bool> placeswelt;
    bool clicked = false;
    int framesclicked = 0;
    public List<float> scores;
    // Use this for initialization
    void Start () {
        isRunning = false;
        product = "None";
    }
	
	// Update is called once per frame
	void Update () {
        if (isRunning)
        {
            DisplayItems();
            int j = 0;
            while (j < anvilinventorycount)
            {
                displayitems[j].GetComponent<BoxCollider2D>().size = displayitems[j].GetComponent<SpriteRenderer>().sprite.bounds.size;
                j++;
            }
            if (product == "None")//only should be able to click Ready, turn off Unready ready is 8 unready is 9
            {
                panel.transform.GetChild(8).gameObject.SetActive(true);
                panel.transform.GetChild(9).gameObject.SetActive(false);
            }else
            {
                panel.transform.GetChild(8).gameObject.SetActive(false);
                panel.transform.GetChild(9).gameObject.SetActive(true);
            }
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
                AnimateWeld();
                if (Input.GetMouseButtonDown(0))
                {
                    clicked = true;
                    if (anvilinventorycount > 0)
                    {
                        CheckWeld();
                    }
                }
            }
        }
	}
    void AnimateWeld()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objposition = Camera.main.ScreenToWorldPoint(mouseposition);
        realweld.transform.position = objposition;
        if (clicked)
        {
            framesclicked++;
            if (framesclicked < 6)
            {
                realweld.transform.Rotate(0, 0, 18);
            }
            else if (framesclicked < 11)
            {
                realweld.transform.Rotate(0, 0, -18);
            }
            else
            {
                clicked = false;
                framesclicked = 0;
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
        panel.SetActive(true);
        Global.me.openpanel = panel;
    }
    public void CloseAnvil()
    {
        isRunning = false;
        panel.SetActive(false);
        Global.me.openpanel = null;
    }
    public void ReadyAnvil()
    {
        if(numberofbarsused == 0)
        {
            int i = 0;
            bool nothot = false;
            bool alreadydone = false;
            while (i < anvilinventorycount && nothot == false)
            {
                if (displayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().forgedone)
                {
                    if(displayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().anvildone == false)
                    {
                        displayitems[i].GetComponent<DragAnvil>().ready = true;
                        if (displayitems[i].active)
                        {
                            numberofbarsused++;
                            bars.Add(displayitems[i].GetComponent<Collider2D>());
                            if (displayitems[i].GetComponent<DragAnvil>().rotated % 2 != 0) //odd and therefore rotated
                            {
                                string a = i.ToString();
                                rotated.Add(true);
                                numberrotated++;
                            }
                            else //even and therefore not rotated
                            {
                                rotated.Add(false);
                                string a = i.ToString();
                            }
                            //turn on hitareas
                            int w = 0;
                            while (w < displayitems[i].transform.childCount)
                            {
                                displayitems[i].transform.GetChild(w).gameObject.SetActive(true);
                                w++;
                            }
                        }
                    }else
                    {
                        alreadydone = true;
                    }
                }
                else //item isn't hot enough, FUCK YOU!!
                {
                    nothot = true;
                }
                i++;
            }
            if (nothot)
            {
                i = 0;
                while (i < displayitems.Count)
                {
                    displayitems[i].GetComponent<DragAnvil>().ready = false;
                    numberofbarsused = 0;
                    i++;
                }
                UnReadyAnvil();
                Global.me.sendmessage = true;
                Global.me.message = "You can't do anything if your bars aren't hot!";
            }else if (alreadydone)
            {
                i = 0;
                while (i < displayitems.Count)
                {
                    displayitems[i].GetComponent<DragAnvil>().ready = false;
                    numberofbarsused = 0;
                    i++;
                }
                UnReadyAnvil();
                Global.me.sendmessage = true;
                Global.me.message = "This is already done";
            }
            else
            {
                CheckProduct();
                CheckPlacement();
            }
        }
    }
    public void UnReadyAnvil()
    {
        if (hammer == false)
        {
            int i = 0;
            product = "None";
            while (i < displayitems.Count)
            {
                displayitems[i].GetComponent<DragAnvil>().ready = false;
                displayitems[i].transform.position = displayitems[i].GetComponent<DragAnvil>().startposition;
                i++;
            }
            numberofbarsused = 0;
            i = 0;
            while (i < placeshit.Count)
            {
                placestohit.Remove(placestohit[i]);
                placeshit.Remove(placeshit[i]);
                i++;
            }
            i = 0;
            while (i < placeswelt.Count)
            {
                placestoweld.Remove(placestoweld[i]);
                placestoweldto.Remove(placestoweldto[i]);
                placeswelt.Remove(placeswelt[i]);
                i++;
            }
            i = 0;
            while(i < displayitems.Count)
            {
                int w = 0;
                displayitems[i].GetComponent<DragAnvil>().rotated = 0;
                displayitems[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                while (w < displayitems[i].transform.childCount)
                {
                    displayitems[i].transform.GetChild(w).gameObject.SetActive(false);
                    w++;
                }
                i++;
            }
            rotated = new List<bool>();
            numberrotated = 0;
            i = 0;
            while (i < checks.Count)
            {
                Destroy(checks[i]);
                i++;
            }
            checks = new List<GameObject>();
            bars = new List<Collider2D>();
            numberofmisses = 0;
        }
       
    }
    public void GetHammer()
    {
        if (product != "None" && weld == false)
        {
            if (hammer)
            {
                hammer = false;
                clicked = false;
                framesclicked = 0;
                realhammer.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                hammer = true;
            }
        }

    }
    public void GetWeld()
    {
        if(product != "None" && hammer == false)
        {
            if (weld)
            {
                weld = false;
                clicked = false;
                framesclicked = 0;
                realweld.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                weld = true;
            }
        }
    }
    void CheckProduct()
    {
        if (numberofbarsused == 1)
        {
            if (rotated[0] == false)
            {
                product = "Dagger";
                int i = displayitems[0].transform.childCount - 1;
                while (i > -1)
                {
                    if (displayitems[0].transform.GetChild(i).name == "MidLeftX")
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
                int i = 0;
                while (i < displayitems[0].transform.childCount)
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
                    i++;
                }
            }
        }
        else if (numberofbarsused == 2)
        {
            if (numberrotated == 0)
            {
                int j = 0;
                int top_j = 0;
                int bot_j = 0;
                while (j < numberofbarsused)
                {
                    if (displayitems[j].transform.position.y > displayitems[top_j].transform.position.y)
                    {
                        top_j = j;
                    }
                    j++;
                }
                product = "Sword";
                j = 0;
                while (j < numberofbarsused)
                {
                    int i = 0;
                    while (i < displayitems[j].transform.childCount)
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
                        if (displayitems[j].transform.GetChild(i).name == "Bottom" && j == top_j)
                        {
                            placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeswelt.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "Top" && j != top_j)
                        {
                            placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        }
                        i++;
                    }
                    j++;
                }
                int q = 0;
                while (q < placestoweld.Count)
                {
                    if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                    {
                        placestoweld.Remove(placestoweld[q]);
                        placestoweldto.Remove(placestoweldto[q]);
                        placeswelt.Remove(placeswelt[q]);
                        if (placestoweld.Count == 0)
                        {
                            UnReadyAnvil();
                        }
                    }
                    q++;
                }

            }
            else if (numberrotated == 1)
            {
                product = "Axe";
                int j = 0;
                while (j < numberofbarsused)
                {
                    int i = 0;
                    while (i < displayitems[j].transform.childCount)
                    {
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && rotated[j] == true)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && rotated[j] == true)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && rotated[j] == true)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightBottom" && rotated[j] == true)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightBottom" && rotated[j] == false)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && rotated[j] == false)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop"&& rotated[j] == true && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeswelt.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && rotated[j] == true && displayitems[j].transform.rotation.z > 0)
                        {
                            placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeswelt.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "3/4LeftX"&& rotated[j] == false && displayitems[j].transform.rotation.z >= 0)
                        {
                            placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        }
                        if (displayitems[j].transform.GetChild(i).name == "1/4RightX" && rotated[j] == false && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                        }
                        i++;
                    }
                    j++;
                }
                int q = 0;
                while (q < placestoweld.Count)
                {
                    if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                    {
                        placestoweld.Remove(placestoweld[q]);
                        placestoweldto.Remove(placestoweldto[q]);
                        placeswelt.Remove(placeswelt[q]);
                        if (placestoweld.Count == 0)
                        {
                            UnReadyAnvil();
                        }
                    }
                    q++;
                }
            }
            else if (numberrotated == 2)
            {
                product = "Scythe";
                int j = 0;
                int left_j = 0;
                while (j < numberofbarsused)
                {
                    if (displayitems[j].transform.position.x < displayitems[left_j].transform.position.x)
                    {
                        left_j = j;
                    }
                    j++;
                }
                j = 0;
                while (j < numberofbarsused)
                {
                    int addedaweld = 0;
                    int i = 0;
                    while (i < displayitems[j].transform.childCount)
                    {
                        if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j != left_j && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j != left_j && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop" && j != left_j && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j != left_j && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j == left_j && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop" && j == left_j && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == left_j && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightBottom" && j == left_j && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "Bottom" && addedaweld < 4)
                        {
                            placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "Top" && addedaweld < 4)
                        {
                            placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            addedaweld++;
                        }
                        i++;
                    }
                    j++;
                }
                int q = 0;
                while (q < placestoweld.Count)
                {
                    if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                    {
                        placestoweld.Remove(placestoweld[q]);
                        placestoweldto.Remove(placestoweldto[q]);
                        placeswelt.Remove(placeswelt[q]);
                        if (placestoweld.Count == 0)
                        {
                            UnReadyAnvil();
                        }
                    }
                    q++;
                }
            }
        }
        if (numberofbarsused == 3)
        {
            int a = 0;
            int left_a = 0;
            int same_x = 0;
            while (a < numberofbarsused)
            {
                if (displayitems[a].transform.position.x < displayitems[left_a].transform.position.x && displayitems[a].transform.position.x + 1.5f > displayitems[left_a].transform.position.x)
                {
                    left_a = a;
                }
                else if ((displayitems[left_a].transform.position.x - 0.4f < displayitems[a].transform.position.x) && displayitems[a].transform.position.x < displayitems[left_a].transform.position.x + 0.4f)
                {
                    same_x++;
                }
                a++;
            }
            int j = 0;
            int top_j = 0;
            int bot_j = 0;
            while (j < numberofbarsused)
            {
                if (displayitems[j].transform.position.y > displayitems[top_j].transform.position.y)
                {
                    top_j = j;
                }
                if (displayitems[j].transform.position.y < displayitems[bot_j].transform.position.y)
                {
                    bot_j = j;
                }
                j++;
            }
            if (numberrotated == 0)
            {
                if(same_x == 3)
                {
                    product = "Claymore";
                    j = 0;
                    while (j < numberofbarsused)
                    {
                        int addedaweld = 0;
                        int i = 0;
                        while (i < displayitems[j].transform.childCount)
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
                            if (displayitems[j].transform.GetChild(i).name == "Bottom" && (j != bot_j))
                            {
                                placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "Top" && (j != top_j))
                            {
                                placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                addedaweld++;
                            }
                            i++;
                        }
                        j++;
                    }
                    int q = 0;
                    while (q < placestoweld.Count)
                    {
                        if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                        {
                            placestoweld.Remove(placestoweld[q]);
                            placestoweldto.Remove(placestoweldto[q]);
                            placeswelt.Remove(placeswelt[q]);
                            if (placestoweld.Count == 0)
                            {
                                UnReadyAnvil();
                            }
                        }
                        q++;
                    }
                }
                else
                {
                    product = "Cutlass";
                    j = 0;
                    while (j < numberofbarsused)
                    {
                        int addedaweld = 0;
                        int i = 0;
                        while (i < displayitems[j].transform.childCount)
                        {
                            if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j == left_a && displayitems[i].transform.rotation.z >= 0)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j == left_a && displayitems[i].transform.rotation.z < 0)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "RightBottom" && j == top_j && displayitems[i].transform.rotation.z >= 0)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == top_j && displayitems[i].transform.rotation.z < 0)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j != left_a)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j != left_a)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidTop" && j == top_j && displayitems[i].transform.rotation.z >= 0)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j == top_j && displayitems[i].transform.rotation.z < 0)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == bot_j && displayitems[i].transform.rotation.z >= 0)
                            {
                                placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "RightBottom" && j == bot_j && displayitems[i].transform.rotation.z < 0)
                            {
                                placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == top_j && displayitems[i].transform.rotation.z >= 0)
                            {
                                placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == top_j && displayitems[i].transform.rotation.z < 0)
                            {
                                placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                addedaweld++;
                            }
                            i++;
                        }
                        j++;
                    }
                    int q = 0;
                    while (q < placestoweld.Count)
                    {
                        if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                        {
                            placestoweld.Remove(placestoweld[q]);
                            placestoweldto.Remove(placestoweldto[q]);
                            placeswelt.Remove(placeswelt[q]);
                            if (placestoweld.Count == 0)
                            {
                                UnReadyAnvil();
                            }
                        }
                        q++;
                    }

                }

            }
            else if(numberrotated == 1)
            {
                j = 0;
                int rotated_j = -1;
                while (j < numberofbarsused)
                {
                    if (rotated[j])
                    {
                        rotated_j = j;
                    }
                    j++;
                }
                if(rotated_j == top_j)
                {
                    product = "Legs";
                    j = 0;
                    Collider2D h = new Collider2D();
                    placestoweld.Add(h);
                    placestoweld.Add(h);
                    placestoweldto.Add(h);
                    placestoweldto.Add(h);
                    while (j < numberofbarsused)
                    {
                        int addedaweld = 0;
                        int i = 0;
                        while (i < displayitems[j].transform.childCount)
                        {
                            if (displayitems[j].transform.GetChild(i).name == "MidLeftX")
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidBottom")
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j != top_j)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidTop" && j == top_j)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "RightBottom" && j == top_j && displayitems[j].transform.rotation.z < 0)
                            {
                                placestoweld[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == top_j && displayitems[j].transform.rotation.z > 0)
                            {
                                placestoweld[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == top_j && displayitems[j].transform.rotation.z < 0)
                            {
                                placestoweld[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == top_j && displayitems[j].transform.rotation.z > 0)
                            {
                                placestoweld[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidTop" && j != top_j && displayitems[j].transform.rotation.z >= 0)
                            {
                                if(j == left_a)
                                {
                                    placestoweldto[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                }else
                                {
                                    placestoweldto[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                }
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j != top_j && displayitems[j].transform.rotation.z < 0)
                            {
                                if (j == left_a)
                                {
                                    placestoweldto[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                }
                                else
                                {
                                    placestoweldto[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                }
                                addedaweld++;
                            }
                            i++;
                        }
                        j++;
                    }
                    int q = 0;
                    while (q < placestoweld.Count)
                    {
                        if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                        {
                            placestoweld.Remove(placestoweld[q]);
                            placestoweldto.Remove(placestoweldto[q]);
                            placeswelt.Remove(placeswelt[q]);
                            if (placestoweld.Count == 0)
                            {
                                UnReadyAnvil();
                            }
                        }
                        q++;
                    }
                }
                else {
                    product = "Spear";
                    j = 0;
                    Collider2D h = new Collider2D();
                    placestoweld.Add(h);
                    placestoweld.Add(h);
                    placestoweldto.Add(h);
                    placestoweldto.Add(h);
                    while (j < numberofbarsused)
                    {
                        int addedaweld = 0;
                        int i = 0;
                        while (i < displayitems[j].transform.childCount)
                        {
                            if (displayitems[j].transform.GetChild(i).name == "RightTop" && (j == top_j || j == bot_j))
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == top_j)
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "RightBottom" && j != top_j )
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j != bot_j && j != top_j )
                            {
                                placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                                placeshit.Add(false);
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidBottom")
                            {
                                if (j == top_j)
                                {
                                    placestoweld[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                    placeswelt.Add(false);
                                }
                                else if(j != bot_j)
                                {
                                    placestoweldto[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                }
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidTop" && j != top_j && j != bot_j)
                            {
                                placestoweldto[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j == bot_j && displayitems[j].transform.rotation.z < 0)
                            {
                                placestoweld[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                addedaweld++;
                            }
                            if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j == bot_j && displayitems[j].transform.rotation.z > 0)
                            {
                                placestoweld[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                addedaweld++;
                            }
                            i++;
                        }
                        j++;
                    }
                    int q = 0;
                    while (q < placestoweld.Count)
                    {
                        if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                        {
                            placestoweld.Remove(placestoweld[q]);
                            placestoweldto.Remove(placestoweldto[q]);
                            placeswelt.Remove(placeswelt[q]);
                            if (placestoweld.Count == 0)
                            {
                                UnReadyAnvil();
                            }
                        }
                        q++;
                    }
                }
            }else if(numberrotated == 3)
            {
                product = "Halberd";
                j = 0;
                while (j < numberofbarsused)
                {
                    int addedaweld = 0;
                    int i = 0;
                    while (i < displayitems[j].transform.childCount)
                    {
                        if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j == left_a)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightBottom" && (j == left_a || j == bot_j))
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == left_a)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == bot_j)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j != left_a)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == left_a && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == left_a && displayitems[j].transform.rotation.z > 0)
                        {
                            placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == bot_j && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == bot_j && displayitems[j].transform.rotation.z > 0)
                        {
                            placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            addedaweld++;
                        }
                        i++;
                    }
                    j++;
                }
                int q = 0;
                while (q < placestoweld.Count)
                {
                    if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                    {
                        placestoweld.Remove(placestoweld[q]);
                        placestoweldto.Remove(placestoweldto[q]);
                        placeswelt.Remove(placeswelt[q]);
                        if (placestoweld.Count == 0)
                        {
                            UnReadyAnvil();
                        }
                    }
                    q++;
                }
            }
        }
        if(numberofbarsused == 4)
        {
            int j = 0;
            int topleft = -1;
            int botleft = -1;
            int topright = -1;
            int botright = -1;
            while (j < numberofbarsused)
            {
                int leftof = 0;//how many its left of
                int above = 0;//how many its above
                int i = 0;
                while (i < numberofbarsused)
                {
                    if (displayitems[j].transform.position.x < displayitems[i].transform.position.x)
                    {
                        leftof++;
                    }
                    if(displayitems[j].transform.position.y> displayitems[i].transform.position.y)
                    {
                        above++;
                    }
                    i++;
                }
                if (leftof >= 2 && above >= 2)
                {
                    topleft = j;
                }
                if(leftof <= 1 && above >= 2)
                {
                    topright = j;
                }
                if (leftof >= 2 && above <= 1)
                {
                    botleft = j;
                }
                if (leftof <= 1 && above <= 1)
                {
                    botright = j;
                }
                j++;
            }
            Debug.Log(topleft);
            Debug.Log(botleft);
            Debug.Log(topright);
            Debug.Log(botright);
            bool close = false;
            if(botleft == -1 && botright == -1 && topleft == -1 && topright == -1)
            {
                close = true;
                UnReadyAnvil();
            }
            if (numberrotated == 0 && close == false)
            {
                product = "Shield";
                Collider2D h = new Collider2D();
                placestoweld.Add(h);
                placestoweld.Add(h);
                placestoweld.Add(h);
                placestoweld.Add(h);
                placestoweldto.Add(h);
                placestoweldto.Add(h);
                placestoweldto.Add(h);
                placestoweldto.Add(h);
                j = 0;
                while (j < numberofbarsused)
                {
                    int addedaweld = 0;
                    int i = 0;
                    while (i < displayitems[j].transform.childCount)
                    {
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && j ==topright)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == topleft)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightBottom" && (j == botright || j == botleft))
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == botleft)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom")
                        {
                            if (j == topleft)
                            {
                                placestoweld[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                            }
                            else if (j == topright)
                            {
                                placestoweld[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                            }
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop")
                        {
                            if (j == botleft)
                            {
                                placestoweldto[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            }
                            else if (j == botright)
                            {
                                placestoweldto[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            }
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidRightX")
                        {
                            if (j == topleft)
                            {
                                placestoweld[2] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                            }
                            else if (j == botleft)
                            {
                                placestoweld[3] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                                placeswelt.Add(false);
                            }
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidLeftX")
                        {
                            if (j == botright)
                            {
                                placestoweldto[3] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            }
                            else if (j == topright)
                            {
                                placestoweldto[2] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            }
                            addedaweld++;
                        }
                        i++;
                    }
                    j++;
                }
                int q = 0;
                while (q < placestoweld.Count)
                {
                    Debug.Log(q);
                    if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                    {
                        placestoweld.Remove(placestoweld[q]);
                        placestoweldto.Remove(placestoweldto[q]);
                        placeswelt.Remove(placeswelt[q]);
                        if (placestoweld.Count == 0)
                        {
                            UnReadyAnvil();
                        }
                    }
                    q++;
                }

            }
            else if(numberrotated == 2 && close == false)
            {
                product = "Armor";
                j = 0;
                Collider2D h = new Collider2D();
                Debug.Log(placestoweld.Count);
                placestoweld.Add(h);
                placestoweld.Add(h);
                placestoweld.Add(h);
                placestoweldto.Add(h);
                placestoweldto.Add(h);
                placestoweldto.Add(h);
                while (j < numberofbarsused)
                {
                    int addedaweld = 0;
                    int i = 0;
                    while (i < displayitems[j].transform.childCount)
                    {
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == topright && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == topright && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "Middle" && j != botleft && j != botright)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j != botleft && j != botright && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j != botleft && j != botright && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop" && j == topleft && displayitems[j].transform.rotation.z < 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j == topleft && displayitems[j].transform.rotation.z > 0)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightBottom" && (j == botright || j == topleft && displayitems[j].transform.rotation.z < 0))
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && (j == botright || j == topleft && displayitems[j].transform.rotation.z > 0))
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == botleft)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop" && j == topleft && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweld[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j == topleft && displayitems[j].transform.rotation.z > 0)
                        {
                            placestoweld[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == topleft && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweld[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftBottom" && j == topleft && displayitems[j].transform.rotation.z > 0)
                        {
                            placestoweld[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j == botleft && displayitems[j].transform.rotation.z >= 0)
                        {
                            placestoweld[2] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j == botleft && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweld[2] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j == botright && displayitems[j].transform.rotation.z >= 0)
                        {
                            placestoweldto[2] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidRightX" && j == botright && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweldto[2] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightBottom" && j == topright && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweldto[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == topright && displayitems[j].transform.rotation.z > 0)
                        {
                            placestoweldto[1] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j == topright && displayitems[j].transform.rotation.z < 0)
                        {
                            placestoweldto[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop" && j == topright && displayitems[j].transform.rotation.z > 0)
                        {
                            placestoweldto[0] = displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                            addedaweld++;
                        }
                        i++;
                    }
                    j++;
                }
                int q = 0;
                while (q < placestoweld.Count)
                {
                    if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                    {
                        placestoweld.Remove(placestoweld[q]);
                        placestoweldto.Remove(placestoweldto[q]);
                        placeswelt.Remove(placeswelt[q]);
                        if (placestoweld.Count == 0)
                        {
                            UnReadyAnvil();
                        }
                    }
                    q++;
                }
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
        Vector2 mouses = mouse - new Vector2(0.65f, 0);
        if (product != "None" && (numberofbarsused == 1 || displayitems[0].GetComponent<DragAnvil>().item.GetComponent<Item>().welddone))
        {
            int i = 0;
            bool hit = false;
            while (i < placestohit.Count)
            {
                if (placestohit[i].OverlapPoint(mouses) && placeshit[i] == false)
                {
                    hit = true;
                    placeshit[i] = true;
                    //Debug.Log("hit "+placestohit[i].transform.name);
                    Vector2 goal = placestohit[i].transform.position;
                    float distance = Vector2.Distance(goal, mouses);
                    float score = 100 + (-612*distance*distance);
                    if (score < 25)
                    {
                        score = 25;
                    }
                    scores.Add(score);
                    GameObject newcheck = Instantiate(check, new Vector3(mouses.x,mouses.y,0), new Quaternion(0,0,0,0),panel.transform);
                    Color green = Color.green;
                    Color red = Color.red;
                    Color used = Color.Lerp(red, green, (score / 100));
                    newcheck.GetComponent<SpriteRenderer>().color = used;
                    newcheck.SetActive(true);
                    checks.Add(newcheck);
                }
                i++;
            }
            if(hit == false)
            {
                i = 0;
                bool hitthebar = false;
                while (i < bars.Count)
                {
                    if (bars[i].OverlapPoint(mouses))
                    {
                        hitthebar = true;
                    }
                    i++;
                }
                if (hitthebar)
                {
                    Debug.Log("You hit the bar, decrease your fucking score.");
                    numberofmisses++;
                }
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
            if (done && anvilitems[0].GetComponent<Item>().anvildone == false)
            {
                i = 0;
                float totalscore = 0;
                while (i < scores.Count)
                {
                    totalscore = totalscore + scores[i];
                    i++;
                }
                totalscore = totalscore / scores.Count;
                totalscore += (-numberofmisses);
                anvilitems[0].GetComponent<Item>().anvildone = true;
                anvilitems[0].GetComponent<Item>().anvilscore = totalscore;
                //Debug.Log(totalscore);
                anvilitems[0].GetComponent<Item>().product = product;
                if (product != "Dagger" || product != "Hammer")
                {
                    int p = 1;
                    while (p < numberofbarsused)
                    {
                        anvilitems.Remove(displayitems[p].GetComponent<DragAnvil>().item);
                        displayitems[p].SetActive(false);
                        anvilinventorycount--;
                        displayitems[p].transform.position = displayitems[p].GetComponent<DragAnvil>().startposition;
                        p++;
                    }
                }
                displayitems[0].GetComponent<DragAnvil>().rotated = 0;
                displayitems[0].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    void CheckWeld()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
        mouse = mouse - new Vector2(0.65f, 0);
        if (product != "Dagger" || product != "Hammer")
        {
            int i = 0;
            while (i < placestoweld.Count)
            {
                if ((placestoweldto[i].OverlapPoint(mouse) || placestoweld[i].OverlapPoint(mouse)) && placeswelt[i] == false)
                {
                    placeswelt[i] = true;
                    GameObject newcheck = Instantiate(check, new Vector3(mouse.x, mouse.y, 0), new Quaternion(0, 0, 0, 0), panel.transform);
                    newcheck.SetActive(true);
                    checks.Add(newcheck);
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
                displayitems[0].GetComponent<DragAnvil>().item.GetComponent<Item>().welddone = true;
            }
        }
    }
    void DisplayItems()
    {
        int j = 0;
        while (j < player.anvildisplayitems.Count)
        {
            player.anvildisplayitems[j].SetActive(false);
            j++;
        }
        int i = 0;
        while (i < player.playerinventorycount)
        {
            player.anvildisplayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = player.anvildisplayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = player.anvildisplayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().image;
            if (player.playeritems[i].GetComponent<Item>().forgedone && player.playeritems[i].GetComponent<Item>().anvildone == false)
            {
                newsprite = player.anvildisplayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().hotimage;
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
        while (i < anvilinventorycount)
        {
            displayitems[i].SetActive(true);
            SpriteRenderer spriterenderer = displayitems[i].GetComponent<SpriteRenderer>();
            Sprite newsprite = displayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().anvilimage;
            spriterenderer.sprite = newsprite;
            int anvilsize = displayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().anvilsize;
            displayitems[i].transform.localScale = new Vector2(anvilsize, anvilsize);
            i++;
        }
    }
}
