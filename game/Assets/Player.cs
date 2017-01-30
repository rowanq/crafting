using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float xMovement;
    public float yMovement;
    public Collider2D forge;
    public Forge forgescript;
    public Sprite walk0;
    public Sprite walk1;
    public Sprite walk2;
    public Sprite walk3;
    public Sprite walk4;
    public Sprite walk5;
    public Sprite walk6;
    public Sprite walk7;
    public Sprite walk8;
    public Sprite run9;
    public Sprite run10;
    public Sprite run11;
    public Sprite run12;
    public SpriteRenderer spriteRenderer;
    List<Sprite> Anim;
    bool facingRight;
    bool canmove;
    bool moving;
    bool running;
    int curSprite;
    float speed;
    Vector2 position;
    // Use this for initialization
    void Start () {
        Anim = new List<Sprite>();
        Anim.Add(walk0);
        Anim.Add(walk2);
        Anim.Add(walk3);
        Anim.Add(walk4);
        Anim.Add(walk5);
        Anim.Add(walk6);
        Anim.Add(walk7);
        Anim.Add(walk8);
        Anim.Add(run9);
        Anim.Add(run10);
        Anim.Add(run11);
        Anim.Add(run12);
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
        moving = false;
        position = new Vector2(transform.position.x, transform.position.y);
        if (canmove)
        {
            DealWithMovement();
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (forge.OverlapPoint(position))
            {
                forgescript.isRunning = true;
                canmove = false;
            }
        }

    }
    void DealWithMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3.0f;
            running = true;
        }
        else
        {
            speed = 1.0f;
            running = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(xMovement*speed, 0, 0);
            moving = true;
            if (!facingRight)
            {
                facingRight = true;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-xMovement * speed, 0, 0);
            moving = true;
            if (facingRight)
            {
                facingRight = false;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, yMovement * speed, 0);
            moving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -yMovement * speed, 0);
            moving = true;
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
}
