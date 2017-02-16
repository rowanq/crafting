using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour {
    public Player player;
    public Collider2D self;
    public Image selfs;
    public GameObject help;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        help.SetActive(false);
        if (player.canmove)
        {
            selfs.enabled = true;
            transform.GetChild(0).GetComponent<Text>().enabled = true;
            Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
            if (self.OverlapPoint(mouse))
            {
                help.SetActive(true);
            }
        }
        else
        {
            selfs.enabled = false;
            transform.GetChild(0).GetComponent<Text>().enabled = false;
        }
    }
}
