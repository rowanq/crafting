using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour {
    public bool isRunning;
    public int tempdecay;
    public int blowerincrease;
    int temp;
    int amountofcoal;
    int amountoftimespentatrighttemp;
    bool blowerposition; //false is down, true is up
    
	// Use this for initialization
	void Start () {
        isRunning = false;
        temp = 0;
        blowerposition = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning)
        {
            RunBlower();
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log(temp);
            }
            temp -= tempdecay;
            if (temp <= 0)
            {
                temp = 0;
            }
        }

	}
    void RunBlower()
    {
        if (blowerposition)//blower is up
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                blowerposition = false;
                temp += blowerincrease;
            }
        }
        else//blower is down
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                blowerposition = true;
            }
        }
    }
}
