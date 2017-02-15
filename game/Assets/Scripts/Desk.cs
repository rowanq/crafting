using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desk : MonoBehaviour {
    public Player player;
    public GameObject speaking;
    bool isRunning;
    string thesaying;
    int timeitsbeenup;
    int timetillnextorder;
	// Use this for initialization
	void Start () {
        timeitsbeenup = 30;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeitsbeenup <= 0)
        {
            speaking.SetActive(false);
            if (timetillnextorder <= 0)
            {
                GenerateOrder();
                WhenIsNextOrder();
                timeitsbeenup = 60 * 4;
            }
        }
	}
    void FixedUpdate()
    {
        timeitsbeenup--;
        if (timeitsbeenup < 0)
        {
            timeitsbeenup = 0;
            timetillnextorder--;
            if (timetillnextorder < 0)
            {
                timetillnextorder = 0;
            }
        }
    }
    void WhenIsNextOrder()
    {
        int random = Random.Range((60*20), (60*40));
        if(player.orders.Count == 0)
        {
            timetillnextorder = 0;
        }else
        {
            timetillnextorder = random + (player.orders.Count * (60*60));
        }
        Debug.Log(timetillnextorder);
    }
    void GenerateOrder()
    {
        string order = "";
        int ordern = Random.Range(0, 2);
        if(ordern == 0)
        {
            order = "Dagger";
        }else if(ordern == 1)
        {
            order = "Hammer";
        }else if(ordern == 2)
        {
            order = "Sword";
        }
        if (player.orders.Count < 3)
        {
            player.orders.Add(order);
        }
        string speak = "";
        int speakn = Random.Range(0, 3);
        if (speakn == 0)
        {
            speak = "Order up! Someone wants a ";
        }
        else if (speakn == 1)
        {
            speak = "Please sir and/or madam, make a ";
        }
        else if (speakn == 2)
        {
            speak = "Oh god! Someone please make a ";
        }
        else if (speakn == 3)
        {
            speak = "";
        }
        thesaying = speak + order;
        DisplayOrder();
        Debug.Log(thesaying);
    }
    void DisplayOrder()
    {
        speaking.SetActive(true);
        speaking.transform.GetChild(0).gameObject.GetComponent<Text>().text = thesaying;
    }
    public void CheckforOrder()
    {
        int i = 0;
        while (i < player.orders.Count)
        {
            int j = 0;
            while (j < player.playerinventorycount)
            {
                if (player.playeritems[j].GetComponent<Item>().product == player.orders[i])
                {
                    //congrats order completed!!
                    player.playeritems.Remove(player.playeritems[j]);
                    player.playerinventorycount--;
                    thesaying = "Thank you oh so much for " + player.orders[i];
                    timeitsbeenup = 120;
                    DisplayOrder();
                    player.orders.Remove(player.orders[i]);
                    Debug.Log(thesaying);
                }
                j++;
            }
            i++;
        }
    }
}
