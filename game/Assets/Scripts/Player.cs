using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Camera camera;
    public float xMovement;
    public float yMovement;
    public int money;
    public int thingssold = 1;
    public Collider2D forge;
    public Forge forgescript;
    public Collider2D storage;
    public Storage storagescript;
    public Collider2D anvil;
    public Anvil anvilscript;
    public Collider2D detailing;
    public Detailing detailingscript;
    public Collider2D library;
    public Library libraryscript;
    public Collider2D desk;
    public Desk deskscript;
    public Collider2D store;
    public Store storescript;
    public List<string> orders;
    public List<GameObject> orderdisplays;
    public Collider2D backdoor;
    public bool inback = false;
    public Collider2D supplies;
    public Supplies suppliesscript;
    public Collider2D frontdoor;
    public bool infront = false;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> Anim;
    public List<GameObject> forgedisplayitems;
    public List<GameObject> storagedisplayitems;
    public List<GameObject> anvildisplayitems;
    public List<GameObject> detailingdisplayitems;
    public List<GameObject> storedisplayitems;
    public List<GameObject> huddisplayitems;
    public List<GameObject> playeritems;
    public int playerinventorycount = 0;
    bool facingRight;
    public bool canmove;
    public int level;
    bool moving;
    bool running;
    int curSprite;
    float speed;
    Vector2 position;
    int timesincelastmove;
    int timesincelastsave = 0;
    // Use this for initialization
    void Start () {
        DealWithProgression();
        facingRight = true;
        moving = false;
        canmove = true;
        curSprite = 0;
        speed = 1.0f;
        float x = transform.position.x;
        float y = transform.position.y;
        position = new Vector2(x, y);
        timesincelastmove = 10;
    }
	
	// Update is called once per frame
	void Update () {
        canmove = true;
        DealWithItems();
        if (forgescript.isRunning || storagescript.isRunning || anvilscript.isRunning || libraryscript.isRunning || detailingscript.isRunning || storescript.isRunning || Global.me.tutorial.GetComponent<Tutorial>().finished == false)
        {
            canmove = false;
        }
        moving = false;
        position = new Vector2(transform.position.x, transform.position.y);
        if (canmove)
        {
            DealWithMovement();
            //DisplayOrders();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(Time.time);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool foundcustomer = false;
            int w = 0;
            while (w < deskscript.customers.Count)
            {
                if (deskscript.customers[w].GetComponent<Collider2D>().OverlapPoint(position))
                {
                    deskscript.CheckforOrder();
                    deskscript.player_customer = w;
                    foundcustomer = true;
                }
                w++;
            }
            if (foundcustomer)
            {

            }
            else if (backdoor.OverlapPoint(position))
            {
                if(camera.transform.position.y == 10)
                {
                    camera.transform.position = new Vector3(0,0,-10);
                    transform.position += new Vector3(0, -2.5f,0);
                    inback = false;
                }else
                {
                    camera.transform.position = new Vector3(0, 10,-10);
                    transform.position += new Vector3(0, 2.5f, 0);
                    inback = true;
                }
            }
            else if (frontdoor.OverlapPoint(position))
            {
                if (camera.transform.position.x == 17.7f)
                {
                    camera.transform.position = new Vector3(0, 0, -10);
                    transform.position += new Vector3(-2.5f, 0, 0);
                    infront = false;
                }
                else
                {
                    camera.transform.position = new Vector3(17.7f, 0, -10);
                    transform.position += new Vector3(2.5f, 0, 0);
                    infront = true;
                }
            }
            else if (forge.OverlapPoint(position))
            {
                forgescript.OpenForge();
            }
            else if (storage.OverlapPoint(position))
            {
                storagescript.OpenStorage();
            }
            else if (anvil.OverlapPoint(position))
            {
                anvilscript.OpenAnvil();
            }
            else if (detailing.OverlapPoint(position))
            {
                detailingscript.OpenDetailing();
            }
            else if (library.OverlapPoint(position))
            {
                libraryscript.OpenLibrary();
            }
            else if (store.OverlapPoint(position))
            {
                storescript.OpenStore();
            }
            else if (supplies.OverlapPoint(position))
            {
                suppliesscript.OpenSupplies();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (forgescript.isRunning)
            {
                forgescript.CloseForge();
            }
            else if (storagescript.isRunning)
            {
                storagescript.CloseStorage();
            }
            else if (anvilscript.isRunning)
            {
                anvilscript.CloseAnvil();
            }
            else if (detailingscript.isRunning)
            {
                detailingscript.CloseDetailing();
            }
            else if (libraryscript.isRunning)
            {
                libraryscript.CloseLibrary();
            }
            else if (storescript.isRunning)
            {
                storescript.CloseStore();
            }
            else
            {
                Global.me.gameon = false;
            }
        }

    }
    void FixedUpdate()
    {
        timesincelastmove++;
        timesincelastsave++;
        if(timesincelastsave >= 50)
        {
            Save();
            timesincelastsave = 0;
        }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Inventory Size", playerinventorycount);
        if (playerinventorycount > 0)
        {
            PlayerPrefs.SetString("Item 0", playeritems[0].GetComponent<Item>().name);
        }
        if (playerinventorycount > 1)
        {
            PlayerPrefs.SetString("Item 1", playeritems[1].GetComponent<Item>().name);
        }
        if (playerinventorycount > 2)
        {
            PlayerPrefs.SetString("Item 2", playeritems[2].GetComponent<Item>().name);
        }
        if (playerinventorycount > 3)
        {
            PlayerPrefs.SetString("Item 3", playeritems[3].GetComponent<Item>().name);
        }
        PlayerPrefs.Save();
    }
    public void Load()
    {
        level = PlayerPrefs.GetInt("Level");
        money = PlayerPrefs.GetInt("Money");
        playerinventorycount = PlayerPrefs.GetInt("Inventory Size");
        if (playerinventorycount > 0)
        {
            GameObject newitem = (GameObject)Instantiate(Resources.Load("Item"));
            playeritems.Add(newitem);
            playeritems[0].GetComponent<Item>().name = PlayerPrefs.GetString("Item 0");
        }
        if (playerinventorycount > 1)
        {
            GameObject newitem = (GameObject)Instantiate(Resources.Load("Item"));
            playeritems.Add(newitem);
            playeritems[1].GetComponent<Item>().name = PlayerPrefs.GetString("Item 1");
        }
        if (playerinventorycount > 2)
        {
            GameObject newitem = (GameObject)Instantiate(Resources.Load("Item"));
            playeritems.Add(newitem);
            playeritems[2].GetComponent<Item>().name = PlayerPrefs.GetString("Item 2");
        }
        if (playerinventorycount > 3)
        {
            GameObject newitem = (GameObject)Instantiate(Resources.Load("Item"));
            playeritems.Add(newitem);
            playeritems[3].GetComponent<Item>().name = PlayerPrefs.GetString("Item 3");
        }
    }
    void DealWithMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 4.0f;
            running = true;
        }
        else
        {
            speed = 1.0f;
            running = false;
        }
        int xdirection = 0;
        int ydirection = 0;
        if (Input.GetKey(KeyCode.D))
        {
            xdirection = 1;
            moving = true;
            if (!facingRight)
            {
                facingRight = true;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            xdirection = -1;
            moving = true;
            if (facingRight)
            {
                facingRight = false;
            }
        }
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            xdirection = 0;
            moving = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            ydirection = 1;
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ydirection = -1;
            moving = true;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            ydirection = 0;
            moving = false;
        }
        if (moving)
        {
            //xmovement
            int ylow = -4;
            int yhi = 4;
            if (inback)
            {
                yhi = 14;
                ylow = 6;
            }
            float xlow = -8f;
            float xhi = 8f;
            if (infront)
            {
                xlow += 17.7f;
                xhi += 17.7f;
            }
            float xposition = Mathf.Clamp(transform.position.x+Time.deltaTime * xMovement * speed * xdirection, xlow, xhi);
            transform.position = new Vector3(xposition, transform.position.y, 0);
            //ymovement
            float yposition = Mathf.Clamp(transform.position.y+Time.deltaTime * yMovement * speed * ydirection, ylow, yhi);
            transform.position = new Vector3(transform.position.x, yposition, 0);

        }
        if (facingRight)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        DealwithMovementAnimation();
    }
    void DealwithMovementAnimation()
    {
        if (moving)
        {
            if(timesincelastmove >= 6)
            {
                timesincelastmove = 0;
                curSprite += 1;
                if (running)//running!
                {
                    if (curSprite > 11)
                    {
                        curSprite = 9;
                    }
                    if (curSprite < 8)
                    {
                        curSprite = 9;
                    }
                }
                else //just moving
                {
                    if (curSprite > 7)
                    {
                        curSprite = 0;
                    }
                }
            }
        }
        else
        {
            curSprite = 0;
        }
        spriteRenderer.sprite = Anim[curSprite];
    }
    void DealWithItems()
    {
        int j = 0;
        while (j < 4)
        {
            storedisplayitems[j].GetComponent<DragStore>().item = null;
            storagedisplayitems[j].GetComponent<DragStorage>().item = null;
            forgedisplayitems[j].GetComponent<DragForce>().item = null;
            anvildisplayitems[j].GetComponent<DragAnvil>().item = null;
            detailingdisplayitems[j].GetComponent<DragDetailing>().item = null;
            huddisplayitems[j].GetComponent<DragForce>().item = null;
            j++;
        }
        int i = 0;
        while (i < playerinventorycount)
        {
            //storage
            storedisplayitems[i].GetComponent<DragStore>().item = playeritems[i];
            storagedisplayitems[i].GetComponent<DragStorage>().item = playeritems[i];
            forgedisplayitems[i].GetComponent<DragForce>().item = playeritems[i];
            anvildisplayitems[i].GetComponent<DragAnvil>().item = playeritems[i];
            detailingdisplayitems[i].GetComponent<DragDetailing>().item = playeritems[i];
            huddisplayitems[i].GetComponent<DragForce>().item = playeritems[i];
            i++;
        }
    }
    void DisplayOrders()
    {
        int i = 0;
        while (i< orderdisplays.Count)
        {
            orderdisplays[i].SetActive(false);
            i++;
        }
        int j = 0;
        while (j < orders.Count)
        {
            orderdisplays[j].SetActive(true);
            orderdisplays[j].GetComponent<Text>().text = orders[j];
            j++;
        }
    }
    void LevelUp()
    {
        level++;
        string recog = "";
        if (level == 1 || level == 9 || level == 12)//newbook
        {
            recog = "You got a new book!";
        }
        else if (level == 2 || level == 6 || level == 8)//newpage in blades
        {
            recog = "You got a new page in Blades.";
        }
        else if (level == 4 || level == 5)//newpage in tools
        {
            recog = "You got a new page in Tools.";
        }
        else if (level == 10)//newpage in poles
        {
            recog = "You got a new page in Poles.";
        }
        else if (level == 13 || level == 14)//newpage in armor
        {
            recog = "You got a new page in Armor.";
        }
        else if (level == 3 || level == 7 || level == 11)//newpage in metal
        {
            recog = "You got a new page in Metal.";
        }
        Global.me.sendmessage = true;
        Global.me.message = recog;
    }
    public void DealWithProgression()
    {
        if (thingssold > 1 && level < 1)
        {
            deskscript.potentialproducts.Add("Hammer");
            libraryscript.booksunlocked++;
            LevelUp();
        }
        if (thingssold > 4 && level < 2)
        {
            deskscript.potentialproducts.Add("Sword");
            libraryscript.pagesunlocked[1]++;
            LevelUp();
        }
        if (thingssold > 8 && level < 3)
        {
            libraryscript.pagesunlocked[0]++;
            storescript.storeinventorycount++;
            LevelUp();
        }
        if (thingssold > 13 && level < 4)
        {
            deskscript.potentialproducts.Add("Axe");
            libraryscript.pagesunlocked[2]++;
            LevelUp();
        }
        if (thingssold > 18 && level < 5)
        {
            deskscript.potentialproducts.Add("Scythe");
            libraryscript.pagesunlocked[2]++;
            LevelUp();
        }
        if (thingssold > 23 && level < 6)
        {
            deskscript.potentialproducts.Add("Claymore");
            libraryscript.pagesunlocked[1]++;
            LevelUp();
        }
        if (thingssold > 30 && level < 7)
        {
            libraryscript.pagesunlocked[0]++;
            storescript.storeinventorycount++;
            LevelUp();
        }
        if (thingssold > 37 && level < 8)
        {
            deskscript.potentialproducts.Add("Cutlass");
            libraryscript.pagesunlocked[1]++;
            LevelUp();
        }
        if (thingssold > 44 && level < 9)
        {
            deskscript.potentialproducts.Add("Spear");
            libraryscript.booksunlocked++;
            LevelUp();
        }
        if (thingssold > 51 && level < 10)
        {
            deskscript.potentialproducts.Add("Halberd");
            libraryscript.pagesunlocked[3]++;
            LevelUp();
        }
        if (thingssold > 60 && level < 11)
        {
            libraryscript.pagesunlocked[0]++;
            storescript.storeinventorycount++;
            LevelUp();
        }
        if (thingssold > 69 && level < 12)
        {
            deskscript.potentialproducts.Add("Legs");
            libraryscript.booksunlocked++;
            LevelUp();
        }
        if (thingssold > 78 && level < 13)
        {
            deskscript.potentialproducts.Add("Shield");
            libraryscript.pagesunlocked[4]++;
            LevelUp();
        }
        if (thingssold > 87 && level < 14)
        {
            deskscript.potentialproducts.Add("Armor");
            libraryscript.pagesunlocked[4]++;
            LevelUp();
        }
    }
}
