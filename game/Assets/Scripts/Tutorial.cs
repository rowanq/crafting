using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public List<GameObject> tutorialdisplays;
    public int curtutorial = 0;
    public int place = 0;
    public bool finished = false;
    public bool forgefinished = false;
    public bool anvilfinished = false;
    public bool detailfinished = false;
    List<string> tutorial;
    List<string> forgetutorial;
    List<string> anviltutorial;
    List<string> detailtutorial;
    List<List<string>> tutorials;
    // Use this for initialization
    void Start()
    {
        tutorial = new List<string>();
        tutorial.Add("Welcome to < Blacksmithing: The Game >, where you play a blacksmith, in a game.The following instructions will help you get acquainted to becoming an expert blacksmith.");
        tutorial.Add("The controls are WASD to move, and E to interact with the various stations. Click on items in menus to move them and to interact with items in the anvil and detailing stations.");
        tutorial.Add("There are seven different stations in the game, each one vital to your blacksmithing operations.");
        tutorial.Add("First, there is the store. Here is where you will buy the supplies you’ll need to craft things. The more experienced you become, the more goods that the store will offer.");
        tutorial.Add("Conveniently located near the store is your storage pile, where you are able to store supplies. This can be useful since you are only capable of carrying four items at a time.");
        tutorial.Add("Next is the Forge, where you heat metal up. It is crucial to do this before you take the metal to the Anvil, otherwise you cannot work it. You will receive more info when you enter the forge.");
        tutorial.Add("Then there is the Library, where you have handy reference books for how to craft the various weapons and tools you will learn to create. You can also take notes here.");
        tutorial.Add("The Anvil is the most complex part of being a blacksmith, where you take metal bars and shape them into useful items. You will receive more info when you enter the Anvil.");
        tutorial.Add("The Detailing Station is where you complete the item you made in the anvil by applying the finishing touches. You will receive more info when you enter there.");
        tutorial.Add("Finally, the Desk is where you will finally be able to sell your crafted item and see how well you made it.");
        tutorial.Add("That’s all the general information we have for you, if you ever get stuck go to the Library for help or you can always redo the tutorial from the options menu. Good luck!");
        forgetutorial = new List<string>();
        forgetutorial.Add("Welcome to the Forge! Before continuing on to the next step of this tutorial, please make sure you have at least one metal bar in your inventory.");
        forgetutorial.Add("Alright, now place your metal bar into the forge.");
        forgetutorial.Add("To heat it up, you are going to need to operate the blower. To do this, you must alternately press the W and S keys or the up and down arrows.");
        forgetutorial.Add("Just like that! And as you can see at the bottom, the temperature goes up when you use the blower. You can also see that is slowly goes down, so you’ll have to keep blowing to keep an even temperature.");
        forgetutorial.Add("To heat up a metal properly, you need to keep it at a certain heat. Since you’re using bronze, you need to keep the temperature at just when the gauge turns color to red-orange.");
        forgetutorial.Add("Good job! You now have heated metal that you can use in the anvil. Don’t forget to put the metal bar back in your inventory before you leave!");
        anviltutorial = new List<string>();
        anviltutorial.Add("Welcome to the Anvil! Before continuing on to the next step of this tutorial, please make sure you have at least one heated metal bar in your inventory.");
        anviltutorial.Add("Alright, now place your metal bar into the anvil.");
        anviltutorial.Add("As you can see, the bar is very different than before. If you hold shift and drag around the bar, it is capable of moving. Do this now.");
        anviltutorial.Add("If you press A or D (or the left and right arrow keys) the bar will rotate as well. Do this now.");
        anviltutorial.Add("Good! Being able to manipulate the bars on the anvil is important, as the configuration of the bars on the screen are how you are able to craft so many items.");
        anviltutorial.Add("While all of the designs for the different products are available in the library, right now you are going to make a dagger, which is a single vertical bar. When you have this configuration, please hit Ready.");
        anviltutorial.Add("Awesome! By hitting Ready, the bars are locked into place and you are now able to craft whatever item you have initiated the design for. It is important to note that once you hit ready, the design is not set into stone, simply by hitting UnReady you are able to move the bars around again and start over.");
        anviltutorial.Add("Now, more complex designs feature a welding step, but since this is a simple dagger all there is the hammer step. Please pick up the hammer now.");
        anviltutorial.Add("To use the hammer, you simply click. Note that the hammer hits where the head lands, not where your mouse is. Please hit where indicated.");
        anviltutorial.Add("Awesome! The color of the checks that you have seen are indications of how close to where you were supposed to hit you actually did. You want to be as close as possible to get the highest score.");
        anviltutorial.Add("Now that you’re finished, put the hammer away and hit UnReady.");
        anviltutorial.Add("Hitting UnReady indicates that you are done with the design of your product, and are now able to move your item back into your inventory. Please do this.");
        anviltutorial.Add("Great! The last step now is detailing, see you there soon! Don’t forget your unfinished dagger!");
        detailtutorial = new List<string>();
        detailtutorial.Add("Welcome to the Detailing Station! Before continuing on to the next step of this tutorial, please make sure you have an unfinished dagger and a small handle in your inventory. (Handles can be bought at the store)");
        detailtutorial.Add("Alright, now please place both into the detailing station.");
        detailtutorial.Add("Note that unlike the Anvil, you cannot move the items at this current stage, all you can do is take them in and out of your inventory. Click Ready when you are.");
        detailtutorial.Add("Now you are capable of moving the items by shift-clicking like in the Anvil station, as well as rotating using A or D. Your goal is to align the two items as closely as possible. Once you are satisfied with your alignment, click Unready");
        detailtutorial.Add("Awesome! You now have a finished dagger and have completed the tutorial! To sell this first creation, go to the desk. Remember you can always go to the library for references on the design of items and general help, and you can redo the tutorial from the options menu.");
        detailtutorial.Add("Good luck!");
        tutorials = new List<List<string>>();
        tutorials.Add(tutorial);
        tutorials.Add(forgetutorial);
        tutorials.Add(anviltutorial);
        tutorials.Add(detailtutorial);

    }
	
	// Update is called once per frame
	void Update () {
        if(place != -1)
        {
            finished = false;
            tutorialdisplays[curtutorial].SetActive(true);
            tutorialdisplays[curtutorial].transform.GetChild(0).GetComponent<Text>().text = tutorials[curtutorial][place];
            tutorialdisplays[curtutorial].transform.GetChild(1).gameObject.SetActive(false);
            tutorialdisplays[curtutorial].transform.GetChild(2).gameObject.SetActive(false);
            tutorialdisplays[curtutorial].transform.GetChild(3).gameObject.SetActive(false);
            if (place != tutorial.Count -1)
            {
                tutorialdisplays[curtutorial].transform.GetChild(1).gameObject.SetActive(true);
            }
            else { finished = true; }
            if (place != 0)
            {
                tutorialdisplays[curtutorial].transform.GetChild(2).gameObject.SetActive(true);
            }else { tutorialdisplays[curtutorial].transform.GetChild(3).gameObject.SetActive(true); }
            if (finished)
            {
                tutorialdisplays[curtutorial].SetActive(false);
                Global.me.openpanel = null;
                place = -1;
            }
        }


    }
    public void Next()
    {
        place++;
    }
    public void Previous()
    {
        place--;
    }
    public void Skip()
    {
        Debug.Log("food");
        tutorialdisplays[curtutorial].SetActive(false);
        Global.me.openpanel = null;
        place = -1;
    }
}
