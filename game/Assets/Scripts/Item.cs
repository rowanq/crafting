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
    public string product = "None";
    public float forgemintemp;
    public float forgemaxtemp;
    public bool forgedone = false;
    public float forgescore = 100;
    public bool welddone = false;
    public bool anvildone = false;
    public float anvilscore = 0;
    public bool detailingdone = false;
    public float detailingscore = 0;
    public float totalscore;
    public int price;
    public int buyprice;
    public List<Sprite> imagesprites;
    public int spriteplace;
    float forgeprocessneeded;
    float typeOfItem;
    float itemID;
    // Use this for initialization
    void Start()
    {
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
            buyprice = 1;
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
            buyprice = 1;
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
            else if (product == "Scythe")
            {
                anvilimage = imagesprites[22];
                image = imagesprites[20];
            }
        }
        if (detailingdone)
        {
            CalculateScore();
            CalculatePrice();
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
            else if(product == "Scythe")
            {
                anvilimage = imagesprites[23];
                image = imagesprites[21];
            }
        }
	}
    void CalculateScore()
    {
        totalscore = (anvilscore + detailingscore) / 2;
    }
    void CalculatePrice()
    {
        float originalprice = 0f;
        if(name == "Bronze")
        {
            if(product == "Dagger")
            {
                originalprice = 5f;
            }
        }
        price = (int)Mathf.Round(originalprice * (totalscore / 100));
    }
}
