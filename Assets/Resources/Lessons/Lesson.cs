using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Lesson
{
    //constant
    public GameObject player = GameObject.Find("Player");

    public List<Action> preLessonFunctions = new List<Action>();
    public List<Action> postLessonFunctions = new List<Action>();
    string eventID;

    public Lesson(string id)
    {
        eventID = id;
        addActions();
    }

    //add actions to be run later
    void addActions()
    {

        switch (eventID)
        {
            case "L000":
                //prelesson
                GameObject teacher = GameObject.Find("Teacher");
                List<string> teacherSpeechStrings = new List<string>(new string[] { "Today,we will" + Environment.NewLine + "be doing some" + Environment.NewLine + "reading." });
                //teacher start talking again
                preLessonFunctions.Add(() => personSpeech(teacher, teacherSpeechStrings[0]));
                //teacher stops talking
                preLessonFunctions.Add(() => dismissSpeech(teacher, true));

                //postlesson
                List<string> teacherSpeechStrings2 = new List<string>(new string[] { "Okay","I will now" + 
                    Environment.NewLine + " be handing out " + Environment.NewLine + "worksheets.",
                    "The questions " + Environment.NewLine + "will be about" + Environment.NewLine + " the story." });
                //teacher start talking again
                postLessonFunctions.Add(() => personSpeech(teacher, teacherSpeechStrings2[0]));

                //teacher continues talking
                for (int i = 1; i < teacherSpeechStrings2.Count; i++)
                {
                    string s = teacherSpeechStrings2[i];
                    postLessonFunctions.Add(() => updatePersonSpeech(teacher, s));
                }
                //teacher stops talking
                postLessonFunctions.Add(() => dismissSpeech(teacher, false));
                break;

            case "L001":

                //prelesson
                teacherSpeechStrings = new List<string>(new string[] { "Okay, " + Environment.NewLine + "next lesson!", 
                    "Spelling.", "When I " + Environment.NewLine + "say a word,",
                   "say each" + Environment.NewLine + "letter out!" });
                teacher = GameObject.Find("Teacher");

                //teacher start talking
                preLessonFunctions.Add(() => personSpeech(teacher, teacherSpeechStrings[0]));
                //teacher continues talking
                for (int i = 1; i < teacherSpeechStrings.Count; i++)
                {
                    string s = teacherSpeechStrings[i];
                    preLessonFunctions.Add(() => updatePersonSpeech(teacher, s));
                }
                //teacher stops talking
                preLessonFunctions.Add(() => dismissSpeech(teacher, true));

                //tutorial

                //postlesson
                postLessonFunctions.Add(() => changeMode("lessonMode", "L002"));
                break;

            case "L002":

                //prelesson
                teacherSpeechStrings = new List<string>(new string[] { "Okay,now we " 
                    + Environment.NewLine + "will count" + Environment.NewLine + "syllables." });
                teacher = GameObject.Find("Teacher");

                //teacher start talking
                preLessonFunctions.Add(() => personSpeech(teacher, teacherSpeechStrings[0]));
                //teacher continues talking
                for (int i = 1; i < teacherSpeechStrings.Count; i++)
                {
                    string s = teacherSpeechStrings[i];
                    preLessonFunctions.Add(() => updatePersonSpeech(teacher, s));
                }
                //teacher stops talking
                preLessonFunctions.Add(() => dismissSpeech(teacher, true));

                //postlesson
                break;
        }


    }

    public void enableInput(bool b)
    {
        GameObject lm = GameObject.Find("LessonManager");
        lm.GetComponent<LessonManagerScript>().setInput(b);
    }


    void changeMode(string mode, string id)
    {
        GameObject lm = GameObject.Find("LessonManager");
        lm.GetComponent<LessonManagerScript>().changeMode(mode, id);

    }

    //do action
    public void action()
    {
        if (preLessonFunctions.Count > 0)
        {
            preLessonFunctions[0]();
            preLessonFunctions.RemoveAt(0);
        }
        else{
            postLessonFunctions[0]();
            postLessonFunctions.RemoveAt(0);
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

    void updatePersonSpeech(GameObject person, string speechString)
    {
        GameObject floatingText = (person.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;
        floatingText.GetComponent<TextMesh>().text = speechString;

    }

    void dismissSpeech(GameObject person, bool allowPlayerInput)
    {
     
        person.GetComponent<PersonScript>().action();
        enableInput(allowPlayerInput);
        //triggerGameMode();
    }

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
        switch (direction)
        {

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
