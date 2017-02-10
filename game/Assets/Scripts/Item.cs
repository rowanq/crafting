using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public Player player;
    public Sprite image;
    public Sprite hotimage;
    public Sprite anvilimage;
    public SpriteRenderer spriterenderer;
    public GameObject displayitem;
    public float forgeprogress;
    public string name;
    public string product;
    public float forgemintemp;
    public float forgemaxtemp;
    public bool forgedone = false;
    public bool welddone = false;
    public bool anvildone = false;
    public bool detailingdone = false;
    public List<Sprite> imagesprites;
    public int spriteplace;
    float forgeprocessneeded;
    float typeOfItem;
    float itemID;
    // Use this for initialization
    void Start()
    {
        product = "None";
        typeOfItem = 0;
        itemID = Random.Range(0, 100000);
        player.itemsingame.Add(gameObject);
        if (name == "Bronze")
        {
            image = imagesprites[0];
            hotimage = imagesprites[1];
            anvilimage = imagesprites[2];
            forgemintemp = 40;
            forgemaxtemp = 60;
            forgeprocessneeded = 80;
        }
        else if (name == "Iron")
        {
            image = imagesprites[9];
            hotimage = imagesprites[1];
            anvilimage = imagesprites[2];
            forgemintemp = 100;
            forgemaxtemp = 120;
            forgeprocessneeded = 160;
        }
        else if (name == "Handle")
        {
            image = imagesprites[7];
            anvilimage = imagesprites[8];
            forgemintemp = 1000;
            forgemaxtemp = 1010;
            forgeprocessneeded = 100;
        }
        else if (name == "Handle_Long")
        {
            image = imagesprites[14];
            anvilimage = imagesprites[15];
            forgemintemp = 1000;
            forgemaxtemp = 1010;
            forgeprocessneeded = 100;
        }
    }
	// Update is called once per frame
	void Update () {
        typeOfItem++;
        if (forgeprogress >= forgeprocessneeded)
        {
            forgedone = true;
        }
        if (anvildone)
        {
            if (product == "Dagger")
            {
                anvilimage = imagesprites[5];
                image = imagesprites[3];
            }
            else if (product == "Hammer")
            {
                anvilimage = imagesprites[12];
                image = imagesprites[10];
            }
            else if (product == "Sword")
            {
                anvilimage = imagesprites[18];
                image = imagesprites[16];
            }
            Debug.Log("SUCKFUCKINGCESS!!");
        }
        if (detailingdone)
        {
            if (product == "Dagger")
            {
                anvilimage = imagesprites[6];
                image = imagesprites[4];
            }
            else if (product == "Hammer")
            {
                anvilimage = imagesprites[13];
                image = imagesprites[11];
            }
            else if (product == "Sword")
            {
                anvilimage = imagesprites[19];
                image = imagesprites[17];
            }
            Debug.Log("SUCKFUCKINGCESS!!");
        }
	}
}
