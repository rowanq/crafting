using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public Player player; 
	// Use this for initialization
	void Start () {
        isRunning = false;
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void OpenAnvil()
    {
        isRunning = true;
        panel.active = true;
    }
    public void CloseAnvil()
    {
        isRunning = false;
        panel.active = false;
    }
}
