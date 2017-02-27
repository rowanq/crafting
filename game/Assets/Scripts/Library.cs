using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour {
    public bool isRunning;
    public GameObject panel;
    public List<GameObject> books;
    public int pagenumber;
    public int booksunlocked;
    public List<int> pagesunlocked = new List<int>();
    int bookopen;
    int curmaxpage;
	// Use this for initialization
    void Awake()
    {
        booksunlocked = 2;
        pagesunlocked.Add(6); //0:metal book
        pagesunlocked.Add(6); //1:blade book
        pagesunlocked.Add(6); //2:tool book
        pagesunlocked.Add(6); //3:poles book
        pagesunlocked.Add(6); //4:armor book
    }
	void Start () {
        isRunning = false;
        pagenumber = 5;
        bookopen = -1;
    }
	
	// Update is called once per frame
	void Update () {
		if (isRunning)
        {
            int i = 0;
            while (i < booksunlocked)
            {
                books[i].SetActive(true);
                i++;
            }
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
        while (i < booksunlocked)
        {
            if (books[i].transform.GetChild(0).GetComponent<Collider2D>().OverlapPoint(mouse))
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
        i = 0;
        while(i < books.Count)
        {
            if(i != bookopen)
            {
                books[i].SetActive(false);
            }
            i++;
        }
        books[bookopen].transform.GetChild(3).gameObject.SetActive(true);
        books[bookopen].transform.GetChild(2).gameObject.SetActive(true);
        books[bookopen].transform.GetChild(4).gameObject.SetActive(true);
        if (pagenumber == 5)
        {
            books[bookopen].transform.GetChild(1).gameObject.SetActive(false);
            books[bookopen].transform.GetChild(0).gameObject.SetActive(false);
            books[bookopen].transform.GetChild(3).gameObject.SetActive(false);

        }
        else
        {
            books[bookopen].transform.GetChild(3).gameObject.SetActive(true);
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
        panel.SetActive(true);
        Global.me.openpanel = panel;
    }
    public void CloseLibrary()
    {
        isRunning =false;
        panel.SetActive(false);
        Global.me.openpanel = null;
    }
    public void TurnPageLeft()
    {
        pagenumber--;
        if (pagenumber < 5)
        {
            pagenumber++;
        }
        if(pagenumber > pagesunlocked[bookopen])
        {
            pagenumber = pagesunlocked[bookopen];
        }
    }
    public void TurnPageRight()
    {
        pagenumber++;
        if(pagenumber == pagesunlocked[bookopen]+1)
        {
            pagenumber = curmaxpage;
        }
    }
    public void CloseBook()
    {
        int i = 2;
        books[bookopen].transform.GetChild(0).gameObject.SetActive(true);
        books[bookopen].transform.GetChild(1).gameObject.SetActive(true);
        while (i < books[bookopen].transform.childCount)
        {
            books[bookopen].transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        bookopen = -1;
        pagenumber = 5;
    }
}
