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
    bool rotated;
    string product;
    public List<Collider2D> placestohit;
    public List<bool> placeshit;
    public List<Collider2D> placestoweld;
    public List<Collider2D> placestoweldto;
    public List<bool> placeswelt;
    bool clicked = false;
    int framesclicked = 0;
    public List<float> scores;
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
        bool nothot = false;
        while (i < anvilinventorycount && nothot == false)
        {
            if (displayitems[i].GetComponent<DragAnvil>().item.GetComponent<Item>().forgedone)
            {
                displayitems[i].GetComponent<DragAnvil>().ready = true;
                if (displayitems[i].active)
                {
                    numberofbarsused++;
                    if (displayitems[i].GetComponent<DragAnvil>().rotated % 2 != 0) //odd and therefore rotated
                    {
                        rotated = true;
                    }
                    else //even and therefore not rotated
                    {
                        rotated = false;
                    }
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
            while(i < displayitems.Count)
            {
                displayitems[i].GetComponent<DragAnvil>().ready = false;
                numberofbarsused = -1;
                i++;
            }
            Debug.Log("You cna't fuckin use thees");
        }else
        {
            CheckProduct();
            CheckPlacement();
        }
    }
    public void UnReadyAnvil()
    {
        int i = 0;
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
        while(i < checks.Count)
        {
            Destroy(checks[i]);
            i++;
        }
        checks = new List<GameObject>();
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
                    int addedaweld = 0;
                    int i = 0;
                    while (i <displayitems[j].transform.childCount)
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
                        if (displayitems[j].transform.GetChild(i).name == "Bottom" && addedaweld <4)
                        {
                            placestoweld.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeswelt.Add(false);
                            addedaweld++;
                        }
                        if (displayitems[j].transform.GetChild(i).name == "Top" && addedaweld <4)
                        {
                            placestoweldto.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            addedaweld++;
                        }
                        i++;
                    }
                    j++;
                }
                Debug.Log(placestoweld.Count);
                int q = 0;
                while (q < placestoweld.Count)
                {
                    if (placestoweld[q].bounds.Intersects(placestoweldto[q].bounds) == false)
                    {
                        placestoweld.Remove(placestoweld[q]);
                        placestoweldto.Remove(placestoweldto[q]);
                        placeswelt.Remove(placeswelt[q]);
                        if(placestoweld.Count == 0)
                        {
                            UnReadyAnvil();
                        }
                    }
                    q++;
                }
                Debug.Log(placestoweld.Count);

            }else
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
                Debug.Log(displayitems[j].transform.name);
                j = 0;
                while (j < numberofbarsused)
                {
                    int addedaweld = 0;
                    int i = 0;
                    while (i < displayitems[j].transform.childCount)
                    {
                        if (displayitems[j].transform.GetChild(i).name == "MidLeftX" && j != left_j)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidTop" && j != left_j)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "MidBottom" && j == left_j)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "RightTop" && j == left_j)
                        {
                            placestohit.Add(displayitems[j].transform.GetChild(i).gameObject.GetComponent<Collider2D>());
                            placeshit.Add(false);
                        }
                        if (displayitems[j].transform.GetChild(i).name == "LeftTop" && j == left_j)
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
                Debug.Log(placestohit.Count);
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
        if (product != "None")
        {
            int i = 0;
            while (i < placestohit.Count)
            {
                if (placestohit[i].OverlapPoint(mouses))
                {
                    placeshit[i] = true;
                    Debug.Log("hit "+placestohit[i].transform.name);
                    Vector2 goal = placestohit[i].transform.position;
                    float distance = Vector2.Distance(goal, mouses);
                    float score = 100 + (-612*distance*distance);
                    scores.Add(score);
                    GameObject newcheck = Instantiate(check, new Vector3(mouses.x,mouses.y,0), new Quaternion(0,0,0,0),panel.transform);
                    newcheck.SetActive(true);
                    checks.Add(newcheck);
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
                anvilitems[0].GetComponent<Item>().anvildone = true;
                anvilitems[0].GetComponent<Item>().anvilscore = totalscore;
                Debug.Log(totalscore);
                anvilitems[0].GetComponent<Item>().product = product;
                if (product == "Sword" || product == "Scythe")
                {
                    anvilitems.Remove(displayitems[1].GetComponent<DragAnvil>().item);
                    Debug.Log(displayitems[1].transform.name);
                    displayitems[1].SetActive(false);
                    anvilinventorycount--;
                    displayitems[1].transform.position = displayitems[1].GetComponent<DragAnvil>().startposition;
                }
                i = 0;
                while (i < checks.Count)
                {
                    Destroy(checks[i]);
                    i++;
                }
                checks = new List<GameObject>();
            }
        }
    }
    void CheckWeld()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
        if (product == "Sword" || product == "Scythe")
        {
            int i = 0;
            while (i < placestoweld.Count)
            {
                if (placestoweldto[i].OverlapPoint(mouse) || placestoweld[i].OverlapPoint(mouse))
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
            /*if (done)
            {
                displayitems[1].transform.position = new Vector2(displayitems[0].transform.position.x, displayitems[0].transform.position.y - 2.1f);
                Debug.Log("wtf");
            }*/
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
                newsprite = player.forgedisplayitems[i].GetComponent<DragForce>().item.GetComponent<Item>().hotimage;
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
            i++;
        }
    }
}
