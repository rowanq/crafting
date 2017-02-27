using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour {
    public DayCycle day;
    public Supplies supplies;
    bool theyresure = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Run()
    {
        if (day.endofday)
        {
            if(supplies.alreadyboughtthisnight || theyresure)
            {
                day.NextDay();
            }else
            {
                Global.me.sendmessage = true;
                Global.me.message = "You haven't bought supplies for the night, are you sure you want to sleep?";
                theyresure = true;
            }
        }else
        {
            Global.me.sendmessage = true;
            Global.me.message = "You can't go to sleep yet, you have work to do!";
        }
    }
}
