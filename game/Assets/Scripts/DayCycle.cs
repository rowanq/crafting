using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour {
    public int day;
    public float timemod;
    public int time;
    public Text timedisplay;
    public List<string> ordersmadethisday;
    public List<float> totalscores;
    public int moneymade;
    public float averagescore;
    public int nearclosingtime; //the time of the day in which the computer decides to stop giving orders
    public bool endofday = false;
    int faketime; //time in frames
    int lasttimedisplayed;
	// Use this for initialization
	void Start () {
        time = 160;
        lasttimedisplayed = -100;
        day = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(time > lasttimedisplayed + 4)
        {
            TimeSpan result = TimeSpan.FromMinutes(time * 3);
            TimeSpan regularclock = TimeSpan.FromHours(12);
            string addition = " AM - Day "+day;
            if(result.Hours > 12)
            {
                result = result.Subtract(regularclock);
                addition = " PM - Day " + day;
            }
            if(time >= 400 && endofday == false)
            {
                endofday = true;
                Global.me.sendmessage = true;
                Global.me.message = "The working day is over!";
            }
            string fixit = result.ToString();
            fixit = fixit.Remove(5, 3);
            fixit = fixit + addition;
            timedisplay.text = fixit;
            lasttimedisplayed = time;
        }
        if (endofday)
        {
            int i = 0;
            averagescore = 0;
            while (i < totalscores.Count)
            {
                averagescore += totalscores[i];
                i++;
            }
            averagescore = (int)Mathf.Round(averagescore / totalscores.Count);
        }
	}
    void FixedUpdate()
    {
        if (Global.me.gameon)
        {
            faketime++;
            if (faketime >= 60)
            {
                time++;
                faketime = 0;
            }
        }
    }
    public void NextDay()
    {
        time = 160;
        faketime = 0;
        day++;
        ordersmadethisday = new List<string>();
        totalscores = new List<float>();
        averagescore = 0;
        moneymade = 0;
        endofday = false;
        lasttimedisplayed = -100;
    }
}
