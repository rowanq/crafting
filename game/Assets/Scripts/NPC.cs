using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public float xMovement;
    public float yMovement;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> Anim;
    public Collider2D deleteZone;
    public string order;
    public int timesinceorderbegan = 0;
    public int giveuptime = 5400;
    public float tip = 0.25f;
    bool facingRight;
    bool moving;
    int curSprite;
    float speed;
    Vector2 position;
    int timesincelastmove;
    int timesincelastdecision;
    int xdirection = 0;
    int ydirection = 0;
    bool stop = false;
    bool leave = false;
    public bool inshop = false;
    public bool ordersaid = false;
    public bool mad = false;
    Color choice;
    // Use this for initialization
    void Start () {
        facingRight = true;
        moving = false;
        curSprite = 0;
        speed = 1.0f;
        timesincelastmove = 10;
        timesincelastdecision = 12;
        int h = Random.Range(0, 6);
        choice = Color.blue;
        if (h == 1)
        {
            choice = Color.green;
        }
        if (h == 2)
        {
            choice = Color.cyan;
        }
        if (h == 3)
        {
            choice = Color.yellow;
        }
        if (h == 4)
        {
            choice = Color.white;
        }
        if (h == 5)
        {
            choice = Color.magenta;
        }
    }
	
	// Update is called once per frame
	void Update () {
        position = new Vector2(transform.position.x, transform.position.y);
        DealWithMovement();
        //spriteRenderer.color = Color.Lerp(Color.white,choice, (float)timesincelastdecision/360f);
        spriteRenderer.color = Color.Lerp(choice, Color.red, (float)timesinceorderbegan / giveuptime);
        DealWithLeaving();
        if(transform.localPosition.x <= 4)
        {
            inshop = true;
        }
        if (timesinceorderbegan >= giveuptime && leave == false)
        {
            Leave();
            mad = true;
            Global.me.sendmessage = true;
            Global.me.message = "You've taken too long! The customer is taking their business elsewhere!";
        }
    }
    void FixedUpdate()
    {
        timesincelastmove++;
        timesincelastdecision++;
        if (inshop && stop == false)
        {
            timesinceorderbegan++;
        }
    }
    void DealWithMovement()
    {
        if (leave)
        {
            if (position.y > deleteZone.transform.position.y)
            {
                ydirection = -1;
            }
            else { ydirection = 0; }
            if (position.x < deleteZone.transform.position.x)
            {
                xdirection = 1;
                facingRight = true;
            }
            else { xdirection = 0; }
            timesincelastdecision = 0;
        }
            if (timesincelastdecision >= 360 && stop == false)
        {
            moving = true;
            timesincelastdecision = 0;
            int a = Random.Range(0, 21);
            if (a > 10)
            {
                xdirection = 1;
                facingRight = true;
            }
            else if (a == 0) { xdirection = 0; }
            else
            {
                xdirection = -1;
                facingRight = false;
            }
            int b = Random.Range(0, 21);
            if (b > 10)
            {
                ydirection = 1;
            }else if(b == 0) { ydirection = 0; }
            else
            {
                ydirection = -1;
            }
            if (a == b || a + b == 10)
            {
                xdirection = 0;
                ydirection = 0;
                moving = false;
                facingRight = true;
            }
        }
        else
        {
            if (leave == false)
            {
                DealwithBounds();
            }
        }
        if (moving)
        {
                //xmovement
                transform.position += new Vector3(Time.deltaTime * xMovement * speed * xdirection, 0, 0);
                //ymovement
                transform.position += new Vector3(0, Time.deltaTime * yMovement * speed * ydirection, 0);
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
    void DealwithBounds()
    {
        if (transform.localPosition.y <= -4)
        {
            ydirection = 1;
            moving = true;
        }
        else if (transform.localPosition.y >= 4)
        {
            ydirection = -1;
            moving = true;
        }
        if (transform.localPosition.x <= -12)
        {
            xdirection = 1;
            facingRight = true;
            moving = true;
        }
        else if (transform.localPosition.x >= 4)
        {
            xdirection = -1;
            facingRight = false;
            moving = true;
            if (transform.localPosition.y > -3)
            {
                ydirection = -1;
            }
            else if(transform.localPosition.y < -3)
            {
                ydirection = 1;
            }
            else
            {
                ydirection = 0;
            }
        }
    }
    void DealwithMovementAnimation()
    {
        if (moving)
        {
            if(timesincelastmove >= 6)
            {
                curSprite += 1;
                timesincelastmove = 0;
                if (curSprite > 4)
                {
                    curSprite = 1;
                }
            }
        }
        else
        {
            curSprite = 0;
        }
        spriteRenderer.sprite = Anim[curSprite];
    }
    void DealWithLeaving()
    {
        if (leave)
        {
            if(deleteZone.OverlapPoint(transform.position))
            {
                DestroyObject(gameObject);
            }
        }
    }
    public void Stop()
    {
        moving = false;
        stop = true;
        timesincelastdecision = 150;
    }
    public void Go()
    {
        moving = true;
        stop = false;
        timesincelastdecision = 350;
    }
    public void Leave()
    {
        moving = true;
        stop = false;
        leave = true;
        timesincelastdecision = 350;
    }
}
