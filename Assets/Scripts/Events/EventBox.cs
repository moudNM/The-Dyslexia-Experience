using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EventBox
{
    //assigns player to variable
    public GameObject player = GameObject.Find("Player");

    List<Action> functions = new List<Action>();
    string eventID;

    public EventBox(string id)
    {
        eventID = id;
        addActions();
    }

    //add actions that are to be executed, based on eventID
    void addActions()
    {

        switch (eventID)
        {
            
            case "EB000":
                
                List<string> speechStrings = new List<string>(new string[] { "You are late!", ".....", "Go and" + Environment.NewLine + "sit down." });
                GameObject teacher = GameObject.Find("Teacher");

                //teacher start talking
                functions.Add(() => personSpeech(teacher, speechStrings[0]));
                //teacher continues talking
                for (int i = 1; i < speechStrings.Count; i++){
                    string s = speechStrings[i];
                    functions.Add(() => updatePersonSpeech(teacher, s));   
                }

                //teacher stops talking
                functions.Add(() => dismissSpeech(teacher,false));

                //player moves to seat
                addPlayerMovement("Left", 19);
                addPlayerMovement("Down", 25);
                addPlayerMovement("Left", 14);
                functions.Add(() => playerSprite("Up"));

                List<string> speechStrings2 = new List<string>(new string[] { "Okay. Let's" + Environment.NewLine + "begin class." });
                //teacher start talking again
                functions.Add(() => personSpeech(teacher, speechStrings2[0]));
                //teacher stops talking
                functions.Add(() => dismissSpeech(teacher, false));
                //starts first lesson
                functions.Add(() => changeMode("lessonMode","L000"));
                break;

            
        }


    }

    //enable input
    public void enableInput(bool b){
        GameObject ebsm = GameObject.Find("EventBoxManager");
        ebsm.GetComponent<EventBoxManagerScript>().setInput(b);
    }

    //changes game mode
    void changeMode(string mode, string id)
    {
        GameObject ebsm = GameObject.Find("EventBoxManager");
        ebsm.GetComponent<EventBoxManagerScript>().changeMode(mode, id);

    }

    //do action
    public void action()
    {
        if (functions.Count > 0)
        {
            functions[0]();
            functions.RemoveAt(0);
        }
    }

    //list of generic actions
    void personSpeech(GameObject person, string speechString)
    {
        GameObject floatingText = (person.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;
        floatingText.GetComponent<TextMesh>().text = speechString;
        person.GetComponent<PersonScript>().action();//set bubble active
        enableInput(true);
    }

    //updates speech
    void updatePersonSpeech(GameObject person, string speechString)
    {
        GameObject floatingText = (person.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;
        floatingText.GetComponent<TextMesh>().text = speechString;

    }

    //dismisses speech
    void dismissSpeech(GameObject person, bool allowPlayerInput)
    {
        person.GetComponent<PersonScript>().action();
        enableInput(allowPlayerInput);
      
    }

    //player Movement
    void addPlayerMovement(string direction, int noOfTimes)
    {
        functions.Add(() => playerSprite(direction));
        switch (direction)
        {
            case "Left":
                foreach (int index in Enumerable.Range(1, noOfTimes))
                {
                    functions.Add(playerMovementLeft);
                }
                break;
            case "Right":
                foreach (int index in Enumerable.Range(1, noOfTimes))
                {
                    functions.Add(playerMovementRight);
                }
                break;
            case "Up":
                foreach (int index in Enumerable.Range(1, noOfTimes))
                {
                    functions.Add(playerMovementUp);
                }
                break;
            case "Down":
                foreach (int index in Enumerable.Range(1, noOfTimes))
                {
                    functions.Add(playerMovementDown);
                }
                break;
        }
    }

    //player movement
    void playerMovementLeft()
    {
        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        body.MovePosition(new Vector2((player.transform.position.x - 0.15f), (player.transform.position.y)));    
    }
    void playerMovementRight()
    {
        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        body.MovePosition(new Vector2((player.transform.position.x + 0.15f), (player.transform.position.y)));
    }
    void playerMovementUp()
    {
        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        body.MovePosition(new Vector2((player.transform.position.x), (player.transform.position.y + 0.15f)));
    }
    void playerMovementDown()
    {
        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        body.MovePosition(new Vector2((player.transform.position.x), (player.transform.position.y - 0.15f)));
    }
    void playerSprite(string direction)
    {
        switch(direction){

            case "Left":
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerMovement>().PlayerLeft;
                break;
            case "Right":
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerMovement>().PlayerRight;
                break;
            case "Up":
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerMovement>().PlayerUp;
                break;
            case "Down":
                player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerMovement>().PlayerDown;
                break;
        }

    }

}
