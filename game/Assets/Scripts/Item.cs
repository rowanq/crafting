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
    public int menuspriteplace;
    public int anvilspriteplace;
    float forgeprocessneeded;
    float typeOfItem;
    float itemID;
    bool anvilchecked = false;
    bool detailingchecked = false;
    // Use this for initialization
    void Start()
    {
        typeOfItem = 0;
        itemID = Random.Range(0, 100000);
        player.itemsingame.Add(gameObject);
        if (name == "Bronze")
        {
            menuspriteplace = 3;
            anvilspriteplace = 110;
            hotimage = imagesprites[menuspriteplace+1];
            forgemintemp = 40;
            forgemaxtemp = 60;
            forgeprocessneeded = 80;
            buyprice = 1;
        }
        else if (name == "Iron")
        {
            menuspriteplace = 29;
            anvilspriteplace = 136;
            hotimage = imagesprites[menuspriteplace + 1];
            forgemintemp = 100;
            forgemaxtemp = 120;
            forgeprocessneeded = 160;
            buyprice = 2;
        }
        else if (name == "Steel")
        {
            menuspriteplace = 55;
            anvilspriteplace = 162;
            hotimage = imagesprites[menuspriteplace + 1];
            forgemintemp = 100;
            forgemaxtemp = 120;
            forgeprocessneeded = 160;
            buyprice = 3;
        }
        else if (name == "Titanium")
        {
            menuspriteplace = 81;
            anvilspriteplace = 188;
            hotimage = imagesprites[menuspriteplace + 1];
            forgemintemp = 100;
            forgemaxtemp = 120;
            forgeprocessneeded = 160;
            buyprice = 4;
        }
        else if (name == "Handle")
        {
            menuspriteplace = 0;
            anvilspriteplace = 107;
            forgemintemp = 1000;
            forgemaxtemp = 1010;
            forgeprocessneeded = 100;
            buyprice = 1;
        }
        else if (name == "Handle_Long")
        {
            menuspriteplace = 1;
            anvilspriteplace = 108;
            forgemintemp = 1000;
            forgemaxtemp = 1010;
            forgeprocessneeded = 100;
            buyprice = 1;
        }
        else if (name == "Crest")
        {
            menuspriteplace = 2;
            anvilspriteplace = 109;
            forgemintemp = 1000;
            forgemaxtemp = 1010;
            forgeprocessneeded = 100;
            buyprice = 1;
        }
    }
	// Update is called once per frame
	void Update () {
        image = imagesprites[menuspriteplace];
        anvilimage = imagesprites[anvilspriteplace];
        if (forgeprogress >= forgeprocessneeded)
        {
            forgedone = true;
        }
        if (anvildone && anvilchecked == false)
        {
            anvilchecked = true;
            if (product == "Dagger")
            {
                int i = 2;
                menuspriteplace += i;
                anvilspriteplace += i;

            }
            else if (product == "Hammer")
            {
                int i = 4;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Sword")
            {
                int i = 6;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Scythe")
            {
                int i = 10;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Claymore")
            {
                int i = 12;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Cutlass")
            {
                int i = 14;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Spear")
            {
                int i = 6;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Halberd")
            {
                int i = 18;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Legs")
            {
                int i = 20;
                menuspriteplace += i;
                anvilspriteplace += i;
            }

            else if (product == "Armor")
            {
                int i = 22;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
            else if (product == "Shield")
            {
                int i = 24;
                menuspriteplace += i;
                anvilspriteplace += i;
            }
        }
        if (detailingdone && detailingchecked == false)
        {
            detailingchecked = true;
            CalculateScore();
            CalculatePrice();
            menuspriteplace++;
            anvilspriteplace++;
        }
	}
    void CalculateScore()
    {
        totalscore = (anvilscore + detailingscore) / 2;
    }
    void CalculatePrice()
    {
        float originalprice = 0f;
        float pricemodifier = 0f;
        if(name == "Bronze")
        {
            pricemodifier = 1f;
        }
        else if(name == "Iron")
        {
            pricemodifier = 2f;
        }
        else if (name == "Steel")
        {
            pricemodifier = 3f;
        }
        else if (name == "Titanium")
        {
            pricemodifier = 4f;
        }
        if (product == "Dagger")
        {
            originalprice = 5f;
        }
        price = (int)Mathf.Round(originalprice * (totalscore / 100));
    }
}
