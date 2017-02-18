using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public Text text;
    public GameObject optionmenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Global.me.gameon)
        {
            text.text = "Continue";
        }
	}
    public void OpenOptionsMenu()
    {
        int i = 0;
        while (i < transform.childCount)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        optionmenu.SetActive(true);
    }
}
