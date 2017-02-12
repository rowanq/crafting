using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public List<GameObject> books;
    public int pagenumber;
    int bookopen;
    int curmaxpage;
	// Use this for initialization
	void Start () {
        isRunning = false;
        pagenumber = 4;
        bookopen = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DealWithBooks();
            }
            if (bookopen > -1)
            {
                DisplayBook();
            }
        }
	}
    void DealWithBooks()
    {
        Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouse = Camera.main.ScreenToWorldPoint(mouseposition);
        int i = 0;
        while (i < books.Count)
        {
            if (books[i].GetComponent<Collider2D>().OverlapPoint(mouse))
            {
                bookopen = i;
                curmaxpage = books[i].transform.childCount -1; 
            }
            i++;
        }
    }
    void DisplayBook()
    {
        int i = 1;
        while (i < books[bookopen].transform.childCount)
        {
            books[bookopen].transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        books[bookopen].transform.GetChild(3).gameObject.SetActive(true);
        books[bookopen].transform.GetChild(2).gameObject.SetActive(true);
        Debug.Log("a" + pagenumber);
        if (pagenumber == 4)
        {
            books[bookopen].transform.GetChild(1).gameObject.SetActive(false);

        }else
        {
            Debug.Log(books[bookopen].transform.GetChild(1).gameObject.name);
            books[bookopen].transform.GetChild(1).gameObject.SetActive(true);
        }
        if (pagenumber >= curmaxpage)
        {
            books[bookopen].transform.GetChild(2).gameObject.SetActive(false);
        }
        books[bookopen].transform.GetChild(pagenumber).gameObject.SetActive(true);

    }
    public void OpenLibrary()
    {
        isRunning = true;
        panel.active = true;
    }
    public void CloseLibrary()
    {
        isRunning =false;
        panel.active = false;
    }
    public void TurnPageLeft()
    {
        pagenumber--;
        if (pagenumber < 4)
        {
            pagenumber++;
        }
    }
    public void TurnPageRight()
    {
        pagenumber++;
    }
    public void CloseBook()
    {
        int i = 1;
        while (i < books[bookopen].transform.childCount)
        {
            books[bookopen].transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        bookopen = -1;
        pagenumber = 4;
    }
}
