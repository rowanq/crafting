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
            forgemintemp = 80;
            forgemaxtemp = 100;
            forgeprocessneeded = 80;
        }
        else if (name == "Iron")
        {
            image = imagesprites[9];
            hotimage = imagesprites[1];
            anvilimage = imagesprites[2];
            forgemintemp = 120;
            forgemaxtemp = 140;
            forgeprocessneeded = 160;
        }
        else if (name == "Handle")
        {
            image = imagesprites[7];
            anvilimage = imagesprites[8];
            forgemintemp = 1000;
            forgemaxtemp = 1010;
            forgeprocessneeded = 0;
        }
    }
	// Update is called once per frame
	void Update () {
        typeOfItem++;
        if (forgeprogress >= forgeprocessneeded)
        {
            spriterenderer.sprite = hotimage;
        }
        if (anvildone)
        {
            Debug.Log("SUCKFUCKINGCESS!!");
            anvilimage = imagesprites[5];
            image = imagesprites[3];
        }
        if (detailingdone)
        {
            anvilimage = imagesprites[6];
            image = imagesprites[4];
        }
	}
}
