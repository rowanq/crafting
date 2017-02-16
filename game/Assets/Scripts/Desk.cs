using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desk : MonoBehaviour {
    public Player player;
    public GameObject speaking;
    public GameObject orders;
    public int maxplayerorders = 3;
    bool isRunning;
    string thesaying;
    int timeitsbeenup;
    int timetillnextorder;
    List<string> potentialproducts =  new List<string>();
	// Use this for initialization
	void Start () {
        timeitsbeenup = 30;
        potentialproducts.Add("Dagger");
	}
	
	// Update is called once per frame
	void Update () {
        if (timeitsbeenup <= 0)
        {
            speaking.SetActive(false);
            if (timetillnextorder <= 0 && player.orders.Count < maxplayerorders)
            {
                GenerateOrder();
                WhenIsNextOrder();
                timeitsbeenup = 60 * 4;
            }
        }
        int i = 0;
        while (i < orders.transform.childCount)
        {
            orders.transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        if (player.canmove)
        {
            i = 0;
            while (i < player.orders.Count)
            {
                orders.transform.GetChild(i).gameObject.SetActive(true);
                orders.transform.GetChild(i).gameObject.GetComponent<Text>().text = player.orders[i];
                i++;
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
    void DealWithPlayerProgression()
    {
        int level = player.thingssold;
        if (level > 1)
        {
            potentialproducts.Add("Hammer");
        }
        else if(level > 4)
        {
            potentialproducts.Add("Sword");
        }
        else if (level > 8)
        {
            potentialproducts.Add("Scythe");
        }
    }
    void GenerateOrder()
    {
        string order = "";
        int ordern = Random.Range(0, potentialproducts.Count);
        if(potentialproducts.Count == 1)
        {
            ordern = 0;
        }
        order = potentialproducts[ordern];
        if (player.orders.Count < maxplayerorders)
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
            bool wassomethingremoved = false;
            int j = 0;
            while (j < player.playerinventorycount)
            {
                if(wassomethingremoved == false)
                {
                    if (player.playeritems[j].GetComponent<Item>().product == player.orders[i])
                    {
                        //congrats order completed!!
                        player.thingssold++;
                        DealWithPlayerProgression();
                        player.money += player.playeritems[j].GetComponent<Item>().price;
                        Debug.Log("Anvil Score: " + player.playeritems[j].GetComponent<Item>().anvilscore);
                        Debug.Log("Detailing Score: " + player.playeritems[j].GetComponent<Item>().detailingscore);
                        Debug.Log("Total Score: " + player.playeritems[j].GetComponent<Item>().totalscore);
                        player.playeritems.Remove(player.playeritems[j]);
                        wassomethingremoved = true;
                        thesaying = "Thank you oh so much for " + player.orders[i];
                        timeitsbeenup = 120;
                        DisplayOrder();
                        player.orders.Remove(player.orders[i]);
                        Debug.Log(thesaying);
                        j--;
                    }
                }
                j++;
            }
            if (wassomethingremoved)
            {
                player.playerinventorycount--;
            }
            i++;
        }
    }
}
