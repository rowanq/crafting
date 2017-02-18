using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour {
    public Player player;
    public Store store;
    public Text money;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(player.canmove || store.isRunning)
        {
            money.enabled = true;
            money.text = "Money: " + player.money;
        }else
        {
            money.enabled = false;
        }
	}
}
