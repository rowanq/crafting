using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Supplies : MonoBehaviour {
    public bool isRunning;
    public Player player;
    public DayCycle day;
    public Storage storage;
    public GameObject panel; //use children for supplies
    public GameObject endofdaypanel;
    public bool alreadyboughtthisnight = false;
    int bronzeprice = 1;
    int ironprice = 2;
    int steelprice = 3;
    int titaniumprice = 4;
    int handleprice = 1;
    int longhandleprice = 1;
    int crestprice = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning)
        {
            DisplayOptions();
            DisplayStats();
            DisplayBuying();
        }
	}
    void DisplayBuying()
    {

    }
    void DisplayStats()
    {
        GameObject name = endofdaypanel.transform.GetChild(0).transform.GetChild(0).gameObject;
        name.GetComponent<Text>().text = "Day: " + day.day;
        GameObject products = endofdaypanel.transform.GetChild(0).transform.GetChild(1).gameObject;
        products.GetComponent<Text>().text = "Products: " + day.ordersmadethisday.Count;
        GameObject score = endofdaypanel.transform.GetChild(0).transform.GetChild(2).gameObject;
        score.GetComponent<Text>().text = "Total Score: " + day.averagescore;
        GameObject money = endofdaypanel.transform.GetChild(0).transform.GetChild(3).gameObject;
        money.GetComponent<Text>().text = "Money Made: " + day.moneymade;
    }
    void DisplayOptions()
    {
        endofdaypanel.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
        if(alreadyboughtthisnight)
        {
            endofdaypanel.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);
        }
        Transform metal = panel.transform.GetChild(0);//3,7,11
        int i = 0;
        while(i < metal.childCount)
        {
            metal.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        metal.GetChild(0).gameObject.SetActive(true);
        metal.GetChild(1).gameObject.SetActive(true);
        metal.GetChild(2).gameObject.SetActive(true);
        metal.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        metal.GetChild(2).transform.GetChild(3).gameObject.GetComponent<Text>().text = bronzeprice.ToString()+"g";
        if(player.money < bronzeprice)
        {
            metal.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        }
        metal.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = storage.bronzecount.ToString();
        if (player.level >= 3)
        {
            metal.GetChild(3).gameObject.SetActive(true);
            metal.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
            metal.GetChild(3).transform.GetChild(3).gameObject.GetComponent<Text>().text = ironprice.ToString() + "g";
            if (player.money < ironprice)
            {
                metal.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            }
            metal.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = storage.ironcount.ToString();
        }
        if(player.level >= 7)
        {
            metal.GetChild(4).gameObject.SetActive(true);
            metal.GetChild(4).transform.GetChild(1).gameObject.SetActive(true);
            metal.GetChild(4).transform.GetChild(3).gameObject.GetComponent<Text>().text = steelprice.ToString() + "g";
            if (player.money < steelprice)
            {
                metal.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
            }
            metal.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = storage.steelcount.ToString();
        }
        if(player.level >= 11)
        {
            metal.GetChild(5).gameObject.SetActive(true);
            metal.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
            metal.GetChild(5).transform.GetChild(3).gameObject.GetComponent<Text>().text = titaniumprice.ToString() + "g";
            if (player.money < titaniumprice)
            {
                metal.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            }
            metal.GetChild(5).transform.GetChild(0).GetComponent<Text>().text = storage.titaniumcount.ToString();
        }
        Transform handle = panel.transform.GetChild(1);
        i = 0;
        while(i < handle.childCount)
        {
            handle.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        handle.GetChild(0).gameObject.SetActive(true);
        handle.GetChild(1).gameObject.SetActive(true);
        handle.GetChild(2).gameObject.SetActive(true);
        handle.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        handle.GetChild(2).transform.GetChild(3).gameObject.GetComponent<Text>().text = handleprice.ToString() + "g";
        if (player.money < handleprice)
        {
            handle.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        }
        handle.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = storage.handlecount.ToString();
        handle.GetChild(3).gameObject.SetActive(true);
        handle.GetChild(3).transform.GetChild(3).gameObject.GetComponent<Text>().text = longhandleprice.ToString() + "g";
        if (player.money < longhandleprice)
        {
            handle.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
        }
        handle.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = storage.longhandlecount.ToString();
        if (player.level >= 12)
        {
            handle.GetChild(4).gameObject.SetActive(true);
            handle.GetChild(4).transform.GetChild(3).gameObject.GetComponent<Text>().text = crestprice.ToString() + "g";
            if (player.money < crestprice)
            {
                handle.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
            }
            handle.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = storage.crestcount.ToString();
        }
    }
    public void OpenSupplies()
    {
        if (day.endofday)
        {
            endofdaypanel.SetActive(true);
            isRunning = true;
        }
    }
    public void CloseSupplies()
    {
        endofdaypanel.SetActive(false);
        panel.transform.GetChild(0).gameObject.SetActive(false);
        panel.transform.GetChild(1).gameObject.SetActive(false);
        isRunning = false;
    }
    public void OpenMetal()
    {
        endofdaypanel.SetActive(false);
        panel.SetActive(true);
        panel.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OpenAccessories()
    {
        panel.transform.GetChild(0).gameObject.SetActive(false);
        panel.transform.GetChild(1).gameObject.SetActive(true);
        alreadyboughtthisnight = true;
    }
    public void BuyBronze()
    {
        player.money -= bronzeprice;
        if(storage.bronzecount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Bronze";
            item.GetComponent<Item>().product = "None";
            item.GetComponent<Item>().player = player;
            item.GetComponent<Item>().isplayeritem = false;
            storage.storageitems.Add(item);
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageinventorycount++;
        }
        storage.bronzecount++;
    }
    public void SellBronze()
    {
        player.money += bronzeprice;
        storage.bronzecount--;
        Debug.Log("A");
        if(storage.bronzecount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Bronze";
            storage.storageinventorycount--;
            storage.storageitems.Remove(item);
        }
    }
    public void BuyIron()
    {
        player.money -= ironprice;
        if (storage.ironcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Iron";
            item.GetComponent<Item>().product = "None";
            item.GetComponent<Item>().player = player;
            item.GetComponent<Item>().isplayeritem = false;
            storage.storageitems.Add(item);
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageinventorycount++;
        }
        storage.ironcount++;
    }
    public void SellIron()
    {
        player.money += ironprice;
        storage.ironcount--;
        if (storage.ironcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Iron";
            storage.storageinventorycount--;
            storage.storageitems.Remove(item);
        }
    }
    public void BuySteel()
    {
        player.money -= steelprice;
        if (storage.steelcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Steel";
            item.GetComponent<Item>().product = "None";
            item.GetComponent<Item>().player = player;
            item.GetComponent<Item>().isplayeritem = false;
            storage.storageitems.Add(item);
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageinventorycount++;
        }
        storage.steelcount++;
    }
    public void SellSteel()
    {
        player.money += steelprice;
        storage.steelcount--;
        if (storage.steelcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Steel";
            storage.storageinventorycount--;
            storage.storageitems.Remove(item);
        }
    }
    public void BuyTitanium()
    {
        player.money -= titaniumprice;
        if (storage.titaniumcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Titanium";
            item.GetComponent<Item>().product = "None";
            item.GetComponent<Item>().player = player;
            item.GetComponent<Item>().isplayeritem = false;
            storage.storageitems.Add(item);
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageinventorycount++;
        }
        storage.titaniumcount++;
    }
    public void SellTitanium()
    {
        player.money += titaniumprice;
        storage.titaniumcount--;
        if (storage.titaniumcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Titanium";
            storage.storageinventorycount--;
            storage.storageitems.Remove(item);
        }
    }
    public void BuyHandle()
    {
        player.money -= handleprice;
        if (storage.handlecount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Handle";
            item.GetComponent<Item>().product = "None";
            item.GetComponent<Item>().player = player;
            item.GetComponent<Item>().isplayeritem = false;
            storage.storageitems.Add(item);
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageinventorycount++;
        }
        storage.handlecount++;
    }
    public void SellHandle()
    {
        player.money += handleprice;
        storage.handlecount--;
        if (storage.handlecount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Handle";
            storage.storageinventorycount--;
            storage.storageitems.Remove(item);
        }
    }
    public void BuyLongHandle()
    {
        player.money -= longhandleprice;
        if (storage.longhandlecount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Long Hande";
            item.GetComponent<Item>().product = "None";
            item.GetComponent<Item>().player = player;
            item.GetComponent<Item>().isplayeritem = false;
            storage.storageitems.Add(item);
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageinventorycount++;
        }
        storage.longhandlecount++;
    }
    public void SellLongHandle()
    {
        player.money += longhandleprice;
        storage.longhandlecount--;
        if (storage.longhandlecount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Long Handle";
            storage.storageinventorycount--;
            storage.storageitems.Remove(item);
        }
    }
    public void BuyCrest()
    {
        player.money -= crestprice;
        if (storage.crestcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Crest";
            item.GetComponent<Item>().product = "None";
            item.GetComponent<Item>().player = player;
            item.GetComponent<Item>().isplayeritem = false;
            storage.storageitems.Add(item);
            storage.displayitems[storage.storageinventorycount].GetComponent<DragStorage>().item = item;
            storage.storageinventorycount++;
        }
        storage.crestcount++;
    }
    public void SellCrest()
    {
        player.money += crestprice;
        storage.crestcount--;
        if (storage.crestcount == 0)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item"));
            item.GetComponent<Item>().name = "Crest";
            storage.storageinventorycount--;
            storage.storageitems.Remove(item);
        }
    }
}
