using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desk : MonoBehaviour {
    public Player player;
    public GameObject speaking;
    public GameObject orders;
    public int maxplayerorders = 6;
    public GameObject productdisplay;
    public List<string> potentialproducts = new List<string>();
    public List<GameObject> customers = new List<GameObject>();
    public Collider2D exitDoor;
    public int player_customer;
    public DayCycle day;
    bool isRunning;
    string thesaying;
    int timeitsbeenup;
    int timetillnextorder;
    bool go = false;
    int player_j_to_be_sold;
    int order_i;
	// Use this for initialization
	void Start () {
        timeitsbeenup = 30;
        potentialproducts.Add("Dagger");
    }
	
	// Update is called once per frame
	void Update () {
        if (player.orders.Count == 0 && timetillnextorder != 0 && customers.Count == 0 && day.time < day.nearclosingtime)
        {
            go = true;
            WhenIsNextOrder();
        }
        if (timeitsbeenup <= 0 && Global.me.tutorial.GetComponent<Tutorial>().finished)
        {
            speaking.SetActive(false);
            if ((timetillnextorder <= 0 || go) && player.orders.Count < maxplayerorders && day.time < day.nearclosingtime)
            {
                GenerateOrder();
                WhenIsNextOrder();
                timeitsbeenup = 60 * 4;
                go = false;
            }

        }
        int i = 0;
        while (i < orders.transform.childCount)
        {
            orders.transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        i = 0;
        while (i < customers.Count)
        {
            if (player.yarn)
            {
                customers[i].GetComponent<NPC>().yarn = true;
            }else
            {
                customers[i].GetComponent<NPC>().yarn = false;
            }
            if (customers[i].GetComponent<NPC>().mad)
            {
                player.orders.Remove(player.orders[i]);
                customers.Remove(customers[i]);
            }
            else if(customers[i].GetComponent<NPC>().inshop && customers[i].GetComponent<NPC>().ordersaid == false)
            {
                customers[i].GetComponent<NPC>().ordersaid = true;
                string blorp = customers[i].GetComponent<NPC>().order;
                if (player.orders.Count < maxplayerorders)
                {
                    player.orders.Add(blorp);
                }
                blorp = DressUpWords(blorp);
                DisplayOrder(blorp);
                i += customers.Count;
                timetillnextorder += 340;
            }
            i++;
        }
        if (player.canmove)
        {
            i = 0;
            while (i < player.orders.Count+1)
            {
                orders.transform.GetChild(i).gameObject.SetActive(true);
                if(i != 0)
                {
                    orders.transform.GetChild(i).gameObject.GetComponent<Text>().text = player.orders[i-1];
                }
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
        int random = Random.Range((30), (60));
        if(go == false) //time between actual orders
        {
            timetillnextorder = random + (player.orders.Count * (60)) - (player.level * (2));
            timetillnextorder = 60 * timetillnextorder;
            Debug.Log(timetillnextorder / 60);
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
            GameObject customernew = (GameObject)Instantiate(Resources.Load("NPC"), exitDoor.transform.position, exitDoor.transform.rotation, transform.parent);
            customernew.GetComponent<NPC>().deleteZone = exitDoor;
            customernew.GetComponent<NPC>().order = order;
            customers.Add(customernew);
        }
    }
    string DressUpWords(string blorp)
    {
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
        thesaying = speak + blorp;
        return thesaying;
    }
    public void DisplayOrder(string blorp)
    {
        timeitsbeenup = 300;
        speaking.SetActive(true);
        //Debug.Log(blorp);
        speaking.transform.GetChild(0).gameObject.GetComponent<Text>().text = blorp;
    }
    void DisplaySell()
    {
        productdisplay.SetActive(true);
        GameObject pd = productdisplay.transform.GetChild(0).gameObject;
        Item item = player.playeritems[player_j_to_be_sold].GetComponent<Item>();
        pd.transform.GetChild(0).GetComponent<Text>().text = item.product;
        pd.transform.GetChild(1).GetComponent<Image>().sprite = item.image;
        pd.transform.GetChild(2).GetComponent<Text>().text = "Anvil: "+Mathf.Round(item.anvilscore)+"%";
        pd.transform.GetChild(3).GetComponent<Text>().text = "Detail: " + Mathf.Round(item.detailingscore) + "%";
        pd.transform.GetChild(4).GetComponent<Text>().text = "Total: " + Mathf.Round(item.totalscore) + "%";
        pd.transform.GetChild(5).GetComponent<Text>().text = item.scoreword;
        pd.transform.GetChild(6).GetComponent<Text>().text = item.price + " gold";
        float price = (float)player.playeritems[player_j_to_be_sold].GetComponent<Item>().price;
        float tip = customers[order_i].GetComponent<NPC>().tip * price;
        pd.transform.GetChild(7).GetComponent<Text>().text = "+" + (int)Mathf.Round(tip);
        pd.transform.GetChild(10).gameObject.SetActive(false);
        if((int)Mathf.Round(tip) != 0)
        {
            pd.transform.GetChild(10).gameObject.SetActive(true);
        }
    }
    public void Sell()
    {
        float price = (float)player.playeritems[player_j_to_be_sold].GetComponent<Item>().price;
        float tip = customers[order_i].GetComponent<NPC>().tip * price;
        int totalmoney = (int)Mathf.Round(tip+price);
        player.thingssold += player.playeritems[player_j_to_be_sold].GetComponent<Item>().expworth;
        player.DealWithProgression();
        player.money += totalmoney;
        day.moneymade += player.playeritems[player_j_to_be_sold].GetComponent<Item>().price;
        day.ordersmadethisday.Add(player.orders[order_i]);
        day.totalscores.Add(player.playeritems[player_j_to_be_sold].GetComponent<Item>().totalscore);
        player.playeritems.Remove(player.playeritems[player_j_to_be_sold]);
        player.playerinventorycount--;
        //thesaying = "Thank you oh so much for " + player.orders[order_i];
        //DisplayOrder(thesaying);
        player.orders.Remove(player.orders[order_i]);
        //deal with order times resetting
        customers[order_i].GetComponent<NPC>().Leave();
        customers.Remove(customers[order_i]);
        productdisplay.SetActive(false);
    }
    public void ExitSell()
    {
        productdisplay.SetActive(false);
        customers[order_i].GetComponent<NPC>().Go();
        player_j_to_be_sold = -1;
        order_i = -1;
    }
    public void CheckforOrder()
    {
        int i = 0;
        while (i < customers.Count)
        {
            int j = 0;
            while (j < player.playerinventorycount)
            {
                if (player.playeritems[j].GetComponent<Item>().product == customers[i].GetComponent<NPC>().order && i == player_customer)
                {
                    if (player.playeritems[j].GetComponent<Item>().detailingdone)
                    {
                        player_j_to_be_sold = j;
                        order_i = i;
                        DisplaySell();
                        customers[i].GetComponent<NPC>().Stop();
                    }
                }
                j++;
            }
            i++;
        }
    }
}
