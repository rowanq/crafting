﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public SpriteRenderer spriteRenderer;
    public List<Sprite> Anim;
    public List<GameObject> itemsingame;
    public List<GameObject> forgedisplayitems;
    public List<GameObject> storagedisplayitems;
    public List<GameObject> anvildisplayitems;
    public List<GameObject> detailingdisplayitems;
    public List<GameObject> playeritems;
    public int playerinventorycount = 0;
    bool facingRight;
    bool canmove;
    bool moving;
    bool running;
    int curSprite;
    float speed;
    Vector2 position;
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
    }
	
	// Update is called once per frame
	void Update () {
        canmove = true;
        DealWithItems();
        if (forgescript.isRunning)
        {
            canmove = false;
        }
        moving = false;
        position = new Vector2(transform.position.x, transform.position.y);
        if (canmove)
        {
            DealWithMovement();
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
            curSprite += 1;
            if (running)//running!
            {
                if (curSprite > 11)
                {
                    curSprite = 8;
                }
                if (curSprite < 8)
                {
                    curSprite = 8;
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
        else
        {
            curSprite = 0;
        }
        spriteRenderer.sprite = Anim[curSprite];
    }
    void DealWithItems()
    {
        int i = 0;
        while (i < playerinventorycount)
        {
            //storage
            storagedisplayitems[i].GetComponent<DragStorage>().item = playeritems[i];
            forgedisplayitems[i].GetComponent<DragForce>().item = playeritems[i];
            anvildisplayitems[i].GetComponent<DragAnvil>().item = playeritems[i];
            detailingdisplayitems[i].GetComponent<DragDetailing>().item = playeritems[i];
            i++;
        }
    }
}
