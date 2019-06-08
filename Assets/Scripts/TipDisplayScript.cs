using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TipDisplayScript : MonoBehaviour
{
    //tips UI
    Boolean TipActive = false;
    public GameObject TipDisplay;
    public GameObject TipTitle;
    public GameObject Tip;

    //Note UI
    public GameObject NoteDisplay;
    public GameObject NoteTitle;
    public GameObject Note;

    //Tutorial UI
    public GameObject Tutorial;

    public GameObject Teacher;
    public GameObject LessonDisplay;
    public GameObject LessonInterfaceDisplay;
    public GameObject LessonManager;

    public Boolean MovableCheck = false;
    public Boolean InteractCheck = false;
    public Boolean InteractCheck2 = false;
    public Boolean InteractCheck3 = false;

	private void Start()
	{
        MoveKeys();
	}
	// Update is called once per frame
	void Update()
    {
        DisplayNote();
    }

    //To check for all tips and notes
    public void DisplayNote()
    {
        CheckLate();
        CheckFlippings();
        CheckMovable();
        CheckNextLesson();
        CheckSymbols();
        CheckTutorial();
    }



    //user is late
    public void CheckLate()
    {
        GameObject floatingText = (Teacher.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;

        //if teacher says that student is late
        if (floatingText.GetComponent<TextMesh>().text == "You are late!")
        {
            Note.GetComponent<Text>().text = "People with dyslexia" + Environment.NewLine + "tend to be late.";
            TipActive = false;
            EnableNote();
            PressE();  

        }
        //after teacher tells student to sit down
        else if((!floatingText.activeInHierarchy) && (floatingText.GetComponent<TextMesh>().text == "Go and" + Environment.NewLine + "sit down.")){
            DisableNote();
            DisableTip();
        }

        //before first lesson
        if (floatingText.GetComponent<TextMesh>().text == "Okay. Let's" + Environment.NewLine + "begin class.")
        {
            PressE();

        }

    }

    //check when next lesson starts
    public void CheckNextLesson()
    {
        GameObject floatingText = (Teacher.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;

        if (floatingText.GetComponent<TextMesh>().text == "Okay" || 
            floatingText.GetComponent<TextMesh>().text == "Okay, " + Environment.NewLine + "next lesson!" ||
            floatingText.GetComponent<TextMesh>().text =="Okay,now we "
             + Environment.NewLine + "will count" + Environment.NewLine + "syllables.")
            
        {
            PressE();
            floatingText.SetActive(true);
        }

    }

    //disable tip if tutorial active
    public void CheckTutorial(){
        if(Tutorial.activeSelf){
            DisableTip();
        }
    }



    //flip of b and d, p and q
    public void CheckFlippings()
    {
        if (LessonDisplay.GetComponent<LessonDisplayScript>().GetOriginalSentence() == "Eddy took his dog, Buddy, to the beach.")
        {
            //display b and p note
            Note.GetComponent<Text>().text = "People with dyslexia" + Environment.NewLine 
                + "tend to see the letters" + Environment.NewLine + "b and d flipped.";
            EnableNote();
            if (!TipDisplay.activeSelf)
            {
                PressESentence();
            }
            
        }else if (LessonDisplay.GetComponent<LessonDisplayScript>().GetOriginalSentence() == "He saw few people there. The place was quiet.")
        {
            //display b and p note
            Note.GetComponent<Text>().text = "Dyslexiacs also" + Environment.NewLine
                + "tend to see the letters" + Environment.NewLine + " p and q flipped.";
            EnableNote();

        }

        GameObject floatingText = (Teacher.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;
        //if teacher says that student is late
        if (floatingText.GetComponent<TextMesh>().text == "Okay")
        {
            DisableNote();
            DisableTip();
        }

    }

    //tips for movement
    public void CheckMovable()
    {
        //check if L000 lesson has ended
        GameObject floatingText = (Teacher.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;

        if ((!floatingText.activeInHierarchy) && (floatingText.GetComponent<TextMesh>().text == "The questions " + Environment.NewLine + "will be about" + Environment.NewLine + " the story."))
        {
            if (!MovableCheck)
            {
                TipActive = false;
                MoveKeys();
                MovableCheck = true;
            }

            else if (!InteractCheck)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    TipActive = false;
                    InteractE();
                    InteractCheck = true;
                }


            }
            else if (!InteractCheck2){

                if (Input.GetKeyDown(KeyCode.E)){
                    TipActive = false;
                    InteractE2();
                    InteractCheck2 = true;

                }
            }
            else if(InteractCheck3){
                if(Input.GetKeyDown(KeyCode.E)){
                    TipActive = false;
                    DisableTip();
                    InteractCheck3 = true;
                }

            }

        }
    }

    //note for symbols
    public void CheckSymbols(){
        GameObject floatingText = (Teacher.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;


        if(LessonDisplay.GetComponent<LessonDisplayScript>().GetOriginalWord() != null && LessonInterfaceDisplay.activeInHierarchy){
            if(LessonManager.GetComponent<LessonManagerScript>().lessonID == "L001"){
                Note.GetComponent<Text>().text = "Dyslexiacs have" + Environment.NewLine
               + "trouble ordering " + Environment.NewLine + " letters in a word.";
                TipActive = false;
                SymbolOrder();
            }
            else if(LessonManager.GetComponent<LessonManagerScript>().lessonID == "L002"){
                Note.GetComponent<Text>().text = "Dyslexiacs have" + Environment.NewLine
              + "trouble knowing how " + Environment.NewLine + " many syllables there are in a word.";
                TipActive = false;
                SyllableNumber();
            }
            EnableNote();
            floatingText.SetActive(false);

        }
        else if ((floatingText.GetComponent<TextMesh>().text == "say each" + Environment.NewLine + "letter out!"
                  && !floatingText.activeSelf  
                  &&!LessonInterfaceDisplay.activeInHierarchy &&
                  LessonManager.GetComponent<LessonManagerScript>().lessonID == "L001"))
            {
                DisableNote();
            DisableTip();
            }
        else if ((floatingText.GetComponent<TextMesh>().text == "Okay,now we "
                    + Environment.NewLine + "will count" + Environment.NewLine + "syllables."
                  && !floatingText.activeSelf
                  && !LessonInterfaceDisplay.activeInHierarchy &&
                  LessonManager.GetComponent<LessonManagerScript>().lessonID == "L002"))
        {

            DisableNote();
            DisableTip();
      
        }

    }

    public void EnableNote()
    {
        NoteDisplay.SetActive(true);
    }

    public void DisableNote()
    {
        NoteDisplay.SetActive(false);
    }

    //tips

    //display press E to continue
    public void PressE(){
        if (!TipActive)
        {
            Tip.GetComponent<Text>().text = "Press 'E' to continue.";
            TipDisplay.SetActive(true);
            TipActive = true;
        }
         
    }

    //display press E to continue to next sentence
    public void PressESentence()
    {
        if (!TipActive)
        {
            Tip.GetComponent<Text>().text = "Press 'E' to continue" + Environment.NewLine + "to next sentence.";
            TipDisplay.SetActive(true);
            TipActive = true;
        }

    }

    //up down left right
    public void MoveKeys(){
        if (!TipActive)
        {
            Tip.GetComponent<Text>().text = "Use the Up,Down,Left,Right" + Environment.NewLine + "keys to move.";
            TipDisplay.SetActive(true);
            TipActive = true;
        }

    }

    //press e button
    public void InteractE()
    {
        if (!TipActive)
        {
            Tip.GetComponent<Text>().text = "Press 'E' to interact " + Environment.NewLine + "with other students.";
            TipDisplay.SetActive(true);
            TipActive = true;
        }

    }

    //press e button
    public void InteractE2()
    {
        if (!TipActive)
        {
            Tip.GetComponent<Text>().text = "Press 'E' on worksheet " + Environment.NewLine + "to do worksheet.";
            TipDisplay.SetActive(true);
            TipActive = true;
        }

    }

    //symbol rearrange
    public void SymbolOrder(){
        if (!TipActive)
        {
            Tip.GetComponent<Text>().text = "Click on two symbols to rearrange the order.";
            TipDisplay.SetActive(true);
            TipActive = true;
        }
       
    }

    //syllable number
    public void SyllableNumber()
    {
        if (!TipActive)
        {
            Tip.GetComponent<Text>().text = "Select 'correct' number " + Environment.NewLine + " of syllables.";
            TipDisplay.SetActive(true);
            TipActive = true;
        }

    }

    //disable tip
    public void DisableTip(){
        if(TipActive){
            TipDisplay.SetActive(false);
            TipActive = false;
        }

    }



}

