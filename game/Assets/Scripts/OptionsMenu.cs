using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {
    public GameObject startmenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenStartMenu()
    {
        int i = 0;
        while (i < startmenu.transform.childCount)
        {
            startmenu.transform.GetChild(i).gameObject.SetActive(true);
            i++;
        }
        transform.gameObject.SetActive(false);
    }
}
