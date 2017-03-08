using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour {
    public DayCycle day;
    public Supplies supplies;
    public Image black;
    bool theyresure = false;
    public bool fade = false;
    bool secondfade = false;
    int fadetime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (fade)
        {
            FadeBlack();
        }
	}
    public void Run()
    {
        if (day.endofday)
        {
            if(supplies.alreadyboughtthisnight || theyresure)
            {
                fade = true;
                fadetime = 0;
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
    void FadeBlack()
    {
        Color transparent = new Color(0, 0, 0, 0);
        if (fadetime < 45)
        {
            if(secondfade == false)
            {
                black.color = Color.Lerp(transparent, Color.black, ((float)fadetime / 45.0f));
            }else
            {
                black.color = Color.Lerp(Color.black, transparent, ((float)fadetime / 45.0f));
            }
        }else if(secondfade == false) { fadetime = 0; secondfade = true; day.NextDay(); }
        else
        {
            fade = false;
            fadetime = 0;
        }
        fadetime++;
    }
}
