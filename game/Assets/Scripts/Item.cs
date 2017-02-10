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
    public float forgemintemp;
    public float forgemaxtemp;
    public bool anvildone = false;
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
            forgemintemp = 40;
            forgemaxtemp = 60;
            forgeprocessneeded = 80;
        }
        else if (name == "Iron")
        {
            forgemintemp = 80;
            forgemaxtemp = 100;
            forgeprocessneeded = 160;
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
        }
	}
}
