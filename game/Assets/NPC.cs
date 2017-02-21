using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public float xMovement;
    public float yMovement;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> Anim;
    bool facingRight;
    bool moving;
    int curSprite;
    float speed;
    Vector2 position;
    int timesincelastmove;
    int timesincelastdecision;
    int xdirection = 0;
    int ydirection = 0;
    // Use this for initialization
    void Start () {
        facingRight = true;
        moving = false;
        curSprite = 0;
        speed = 1.0f;
        timesincelastmove = 10;
        timesincelastdecision = 12;
    }
	
	// Update is called once per frame
	void Update () {
        position = new Vector2(transform.position.x, transform.position.y);
        DealWithMovement();
        spriteRenderer.color = Color.Lerp(Color.white,Color.blue, (float)timesincelastdecision/360f);
    }
    void FixedUpdate()
    {
        timesincelastmove++;
        timesincelastdecision++;
    }
    void DealWithMovement()
    {
        if (timesincelastdecision >= 360)
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
            if (transform.localPosition.x <= -12)
            {
                xdirection = 1;
                facingRight = true;
                moving = true;
            }
            else if(transform.localPosition.x >= 4)
            {
                xdirection = -1;
                facingRight = false;
                moving = true;
            }
            if(transform.localPosition.y <= -4)
            {
                ydirection = 1;
                moving = true;
            }else if(transform.localPosition.y >= 4)
            {
                ydirection = -1;
                moving = true;
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
}
