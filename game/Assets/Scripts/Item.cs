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
    public string scoreword;
    public int price;
    public int buyprice;
    public int expworth; //how much experience they give the player (i.e. # of bars used)
    public int anvilsize = 75;
    public List<Sprite> imagesprites;
    public int menuspriteplace;
    public int anvilspriteplace;
    public int originalprice;
    public float temperature;
    public bool upgrade = false;
    public bool isplayeritem = false;
    float forgeprocessneeded;
    float typeOfItem;
    float itemID;
    bool anvilchecked = false;
    bool detailingchecked = false;
    // Use this for initialization
    void Start()
    {
        temperature = -1;
        typeOfItem = 0;
        anvilsize = 75;
        itemID = Random.Range(0, 100000);
        if (name == "Bronze")
        {
            menuspriteplace = 3;
            anvilspriteplace = 110;
            hotimage = imagesprites[menuspriteplace+1];
            forgemintemp = 40;
            forgemaxtemp = 60;
            forgeprocessneeded = 80;
            buyprice = 2;
        }
        else if (name == "Iron")
        {
            menuspriteplace = 29;
            anvilspriteplace = 136;
            hotimage = imagesprites[menuspriteplace + 1];
            forgemintemp = 100;
            forgemaxtemp = 120;
            forgeprocessneeded = 160;
            buyprice = 5;
        }
        else if (name == "Steel")
        {
            menuspriteplace = 55;
            anvilspriteplace = 162;
            hotimage = imagesprites[menuspriteplace + 1];
            forgemintemp = 160;
            forgemaxtemp = 180;
            forgeprocessneeded = 240;
            buyprice = 8;
        }
        else if (name == "Titanium")
        {
            menuspriteplace = 81;
            anvilspriteplace = 188;
            hotimage = imagesprites[menuspriteplace + 1];
            forgemintemp = 200;
            forgemaxtemp = 220;
            forgeprocessneeded = 320;
            buyprice = 11;
        }
        else if (name == "Handle")
        {
            menuspriteplace = 0;
            anvilspriteplace = 107;
            forgemintemp = 10;
            forgemaxtemp = 500;
            forgeprocessneeded = 5;
            buyprice = 1;
        }
        else if (name == "Long Handle")
        {
            menuspriteplace = 1;
            anvilspriteplace = 108;
            forgemintemp = 10;
            forgemaxtemp = 500;
            forgeprocessneeded = 10;
            buyprice = 2;
        }
        else if (name == "Crest")
        {
            menuspriteplace = 2;
            anvilspriteplace = 109;
            forgemintemp = 1000;
            forgemaxtemp = 1010;
            forgeprocessneeded = 100;
            buyprice = 3;
            anvilsize = 300;
        }
        else if(name == "Mitt")
        {
            upgrade = true;
            buyprice = 20;
            menuspriteplace = 216;
            image = imagesprites[menuspriteplace];
        }
        else if(name == "Hot Forge")
        {
            upgrade = true;
            buyprice = 30;
            menuspriteplace = 217;
            image = imagesprites[menuspriteplace];
        }
        else if(name == "Lawyer")
        {
            upgrade = true;
            buyprice = 40;
            menuspriteplace = 218;
            image = imagesprites[menuspriteplace];
        }
        else if(name == "Yarn")
        {
            upgrade = true;
            buyprice = 15;
            menuspriteplace = 215;
            image = imagesprites[menuspriteplace];
        }
    }
    void FixedUpdate()
    {
        if (forgedone && anvildone == false)
        {
            if (temperature > 0)
            {
                if(player.ovenmitts == false || (player.ovenmitts == true && isplayeritem == false))
                {
                    temperature--;
                }
            }
            else
            {
                temperature = -1;
                if (anvildone == false)
                {
                    forgedone = false;
                    forgeprogress = 0;
                }
            }
        }
        
    }
	// Update is called once per frame
	void Update () {
        image = imagesprites[menuspriteplace];
        anvilimage = imagesprites[anvilspriteplace];
        if (forgeprogress >= forgeprocessneeded && temperature < 0)
        {
            forgedone = true;
            temperature = 1800;
            if(name == "Handle" || name == "Long Handle")
            {
                name = "Trash";
            }
        }
        if(name ==  "Trash")
        {
            menuspriteplace = 214;
            hotimage = imagesprites[menuspriteplace];
        }
        if (anvildone && anvilchecked == false)
        {
            anvilchecked = true;
            if (product == "Dagger")
            {
                int i = 2;
                menuspriteplace += i;
                anvilspriteplace += i;
                originalprice = 5;
                expworth = 1;

            }
            else if (product == "Hammer")
            {
                int i = 4;
                menuspriteplace += i;
                anvilspriteplace += i;
                originalprice = 8;
                expworth = 1;
            }
            else if (product == "Sword")
            {
                int i = 6;
                menuspriteplace += i;
                anvilspriteplace += i;
                originalprice = 10;
                expworth = 2;
            }
            else if (product == "Axe")
            {
                int i = 8;
                menuspriteplace += i;
                anvilspriteplace += i;
                originalprice = 12;
                expworth = 2;
            }
            else if (product == "Scythe")
            {
                int i = 10;
                menuspriteplace += i;
                anvilspriteplace += i;
                originalprice = 14;
                expworth = 2;
            }
            else if (product == "Claymore")
            {
                int i = 12;
                menuspriteplace += i;
                anvilspriteplace += i;
                originalprice = 14;
                expworth = 3;
            }
            else if (product == "Cutlass")
            {
                int i = 14;
                menuspriteplace += i;
                anvilspriteplace += i;
                originalprice = 20;
                expworth = 3;
            }
            else if (product == "Spear")
            {
                int i = 16;
                menuspriteplace += i;
                anvilspriteplace += i;
                anvilsize = 50;
                originalprice = 17;
                expworth = 3;
            }
            else if (product == "Halberd")
            {
                int i = 18;
                menuspriteplace += i;
                anvilspriteplace += i;
                anvilsize = 50;
                originalprice = 19;
                expworth = 3;
            }
            else if (product == "Legs")
            {
                int i = 20;
                menuspriteplace += i;
                anvilspriteplace += i;
                anvilsize = 300;
                originalprice = 18;
                expworth = 3;
            }

            else if (product == "Armor")
            {
                int i = 22;
                menuspriteplace += i;
                anvilspriteplace += i;
                anvilsize = 300;
                originalprice = 24;
                expworth = 4;
            }
            else if (product == "Shield")
            {
                int i = 24;
                menuspriteplace += i;
                anvilspriteplace += i;
                anvilsize = 300;
                originalprice = 22;
                expworth = 4;
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
        if (totalscore > 94)
        {
            scoreword = "Perfect!";
        }else if( totalscore > 90)
        {
            scoreword = "Excellent!";
        }else if(totalscore > 80)
        {
            scoreword = "Good";
        }else if(totalscore > 70)
        {
            scoreword =  "Decent";
        }else if(totalscore > 60)
        {
            scoreword = "Below";
        }
        else
        {
            scoreword = "Nope";
        }
    }
    void CalculatePrice()
    {
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
        float blorp = totalscore;
        if(totalscore > 90)
        {
            blorp = 100;
        }
        price = (int)Mathf.Round((pricemodifier*originalprice) * (blorp / 100));
    }
}
