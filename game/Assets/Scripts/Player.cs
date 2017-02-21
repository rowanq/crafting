using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float xMovement;
    public float yMovement;
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
    public int money;
    public int thingssold = 1;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> Anim;
    public List<GameObject> itemsingame;
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
    // Use this for initialization
    void Start () {
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
        if (forgescript.isRunning || storagescript.isRunning || anvilscript.isRunning || libraryscript.isRunning || detailingscript.isRunning || storescript.isRunning)
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
            if (forge.OverlapPoint(position))
            {
                forgescript.OpenForge();
            }
            if (storage.OverlapPoint(position))
            {
                storagescript.OpenStorage();
            }
            if (anvil.OverlapPoint(position))
            {
                anvilscript.OpenAnvil();
            }
            if (detailing.OverlapPoint(position))
            {
                detailingscript.OpenDetailing();
            }
            if (library.OverlapPoint(position))
            {
                libraryscript.OpenLibrary();
            }
            if (desk.OverlapPoint(position))
            {
                deskscript.CheckforOrder();
            }
            if (store.OverlapPoint(position))
            {
                storescript.OpenStore();
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
        else if (Input.GetKey(KeyCode.A))
        {
            xdirection = -1;
            moving = true;
            if (facingRight)
            {
                facingRight = false;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            ydirection = 1;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            ydirection = -1;
            moving = true;
        }
        if (moving)
        {
            //xmovement
            transform.position += new Vector3(Time.deltaTime*xMovement * speed * xdirection, 0, 0);
            //ymovement
            transform.position += new Vector3(0, Time.deltaTime*yMovement * speed * ydirection, 0);
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
            if(timesincelastmove >= 4)
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
        Debug.Log(thingssold);
        if (thingssold > 1 && level < 1)
        {
            Debug.Log(level);
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
