using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detailing : MonoBehaviour
{
    public bool isRunning;
    public GameObject panel;
    public Player player;
    // Use this for initialization
    void Start()
    {
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OpenDetailing()
    {
        isRunning = true;
        panel.active = true;
    }
    public void CloseDetailing()
    {
        isRunning = false;
        panel.active = false;
    }
}
