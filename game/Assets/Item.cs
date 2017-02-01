using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public Player player;
    public Sprite image;
    public GameObject displayitem;
    public float forgeprogress;
    public string name;
    int typeOfItem;
    float itemID;
	// Use this for initialization
	void Start () {
        typeOfItem = 0;
        itemID = Random.Range(0,100000);
        player.itemsingame.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        typeOfItem++;
        if (forgeprogress >= 60)
        {
            Debug.Log("Hot!!");
        }
	}
}
