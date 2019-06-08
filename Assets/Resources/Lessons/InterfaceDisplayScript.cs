using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceDisplayScript : MonoBehaviour {

    string lessonID;
    public GameObject lessonDisplay;
    GameObject scoring; //correct/wrong
    GameObject finalScore;
    GameObject correctAns;
    GameObject symbolAns;
    GameObject normalAns;
    string originalAns;
    string yourAns;
    int yourScore;

    public GameObject button;
    int startPos;
    int endPos;
    GameObject selectedButton;

    public GameObject clueButton;
    public GameObject clue;

    public void Setup(string newWord, string lessonID){

        //disable all
        clueButton.SetActive(false);
        GameObject InterfaceKeyboard2 = transform.Find("InterfaceKeyboard").gameObject;
        InterfaceKeyboard2.SetActive(true);
        GameObject KeyboardButtons2 = InterfaceKeyboard2.transform.Find("Buttons").gameObject;

        for (int i = 0; i <= 8; i++)
        {
            button = KeyboardButtons2.transform.Find("" + i).gameObject;
            button.SetActive(false);
        }

        this.lessonID = lessonID;
        if(this.lessonID == "L001")
        {
            List<char> charList = new List<char>(newWord.ToCharArray());
        
            startPos = 3;
            endPos = 5;

            if (newWord.Length > 3)
            {
                //increase no of words
                int diff = newWord.Length - 3;
                startPos = 3 - (diff / 2);
                endPos = 5 + (diff / 2);
            }

            //set only those keys active
            GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
            GameObject KeyboardButtons = InterfaceKeyboard.transform.Find("Buttons").gameObject;

            for (int i = startPos; i <= endPos; i++)
            {
                button = KeyboardButtons.transform.Find("" + i).gameObject;

                //set button sprite
                button.GetComponent<ButtonText>().setShapeCharacters(charList[0]);
                charList.RemoveAt(0);
                button.SetActive(true);
            }

            clueButton.SetActive(true);

        }
        else if(this.lessonID == "L002")
        {

            startPos = 2;
            endPos = 6;

            //set only those keys active/inactive
            GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
            GameObject KeyboardButtons = InterfaceKeyboard.transform.Find("Buttons").gameObject;
            GameObject EnterButton = InterfaceKeyboard.transform.Find("Enter").gameObject;
            EnterButton.SetActive(false);

            for (int i = startPos; i <= endPos; i++)
            {
                button = KeyboardButtons.transform.Find("" + i).gameObject;

                //set button sprite
                button.GetComponent<ButtonText>().setNumber(i);
                button.SetActive(true);
            }
        }

    }

    public void SelectButton(GameObject currentButton){

        if (lessonID == "L001")
        {
            if (selectedButton == null)
            {
                selectedButton = currentButton;
            }
            else
            {
                SwapLetters(currentButton);
            }
        }else if(lessonID == "L002"){
                selectedButton = currentButton;
                submitAnswer();
        }
    }

    public void SwapLetters(GameObject currentButton){
        //if a button alr selected, on click of next button
        //swaps the two buttons

        if(currentButton.GetComponent<ButtonText>().getButtonString() == null){
            char tempChar = currentButton.GetComponent<ButtonText>().getButtonCharacter();
            if(currentButton.GetComponent<ButtonText>().getShape()){
                //set new shape buttons
                currentButton.GetComponent<ButtonText>().setShapeCharacters(selectedButton.GetComponent<ButtonText>().getButtonCharacter());
                selectedButton.GetComponent<ButtonText>().setShapeCharacters(tempChar);
            }else{
                //set new letters buttons
                currentButton.GetComponent<ButtonText>().setCharacter(selectedButton.GetComponent<ButtonText>().getButtonCharacter());
                selectedButton.GetComponent<ButtonText>().setCharacter(tempChar);
            }
        }else{
            //set new big buttons
            string tempString = currentButton.GetComponent<ButtonText>().getButtonString();
            currentButton.GetComponent<ButtonText>().setBigCharacter(selectedButton.GetComponent<ButtonText>().getButtonString());
            selectedButton.GetComponent<ButtonText>().setBigCharacter(tempString);
        }
        selectedButton = null;
    }


    public void submitAnswer()
    {
        if (lessonID == "L001")
        {
            scoring = transform.Find("scoring").gameObject;
            correctAns = transform.Find("correctAns").gameObject;
            symbolAns = transform.Find("symbolAns").gameObject;

            //show correct/wrong and correct ans
            if (scoring.activeSelf == false)
            {
                //get ans
                GetYourAns();
                //show result and ans
                showAnswer();
                GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
                GameObject enter = InterfaceKeyboard.transform.Find("Enter").gameObject;
                enter.GetComponent<ButtonText>().setBigCharacter("next");

            }
            else
            {
                //hide
                hideAnswer();
                GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
                GameObject enter = InterfaceKeyboard.transform.Find("Enter").gameObject;
                enter.GetComponent<ButtonText>().setBigCharacter("enter");
                //change qns
                lessonDisplay.GetComponent<LessonDisplayScript>().changeWord();
               
            }
        }else if(lessonID == "L002"){
            scoring = transform.Find("scoring").gameObject;
            correctAns = transform.Find("correctAns").gameObject;
            normalAns = transform.Find("normalAns").gameObject;

            if (scoring.activeSelf == false)
            {
                //get ans
                GetYourAns();
                //show result and ans
                showAnswer();
                GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
                GameObject enter = InterfaceKeyboard.transform.Find("Enter").gameObject;
                enter.GetComponent<ButtonText>().setBigCharacter("next");

            }
            else
            {
                //hide
                hideAnswer();
                GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
                GameObject enter = InterfaceKeyboard.transform.Find("Enter").gameObject;
                enter.GetComponent<ButtonText>().setBigCharacter("enter");
                //change qns
                lessonDisplay.GetComponent<LessonDisplayScript>().changeWord();
               
            }
        }
    }

    public void GetYourAns()
    {
        if (lessonID == "L001")
        {
            //get ans from lesson display
            originalAns = lessonDisplay.GetComponent<LessonDisplayScript>().GetOriginalWord();

            //get values for each button
            GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
            GameObject KeyboardButtons = InterfaceKeyboard.transform.Find("Buttons").gameObject;

            //retrieve your ans
            yourAns = "";
            foreach (Transform child in KeyboardButtons.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    yourAns += child.GetComponent<ButtonText>().getButtonCharacter();
                }
            }
        } 
        else if(lessonID == "L002"){
            //get ans from lesson display
            //get ans from number of syllables
            originalAns = lessonDisplay.GetComponent<LessonDisplayScript>().GetOriginalWord();
            yourAns = selectedButton.name;
        }

    }

    void showAnswer()
    {
            if (originalAns == yourAns)
            {
            
                yourScore += 1;

                scoring.GetComponent<Text>().text = "Correct!";
                scoring.GetComponent<Text>().color = Color.green;
            }
            else
            {
                scoring.GetComponent<Text>().text = "Wrong!";
                scoring.GetComponent<Text>().color = Color.red;
            }

            correctAns.GetComponent<Text>().text = "Correct answer: ";
        if (lessonID == "L001")
        {
            symbolAns.GetComponent<Text>().text = originalAns;
            symbolAns.SetActive(true);
        }
        else if (lessonID == "L002"){
            normalAns.GetComponent<Text>().text = originalAns;
            normalAns.SetActive(true);

            //set enter active
            GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
            GameObject EnterButton = InterfaceKeyboard.transform.Find("Enter").gameObject;
            EnterButton.SetActive(true);
            
        }
            
            scoring.SetActive(true);
            correctAns.SetActive(true);
            

            
        }
    void hideAnswer()
    {
        scoring.SetActive(false);
        correctAns.SetActive(false);
        if(lessonID == "L001"){
            symbolAns.SetActive(false);   
        }
        if(lessonID == "L002"){
            normalAns.SetActive(false);
            //set enter active
            GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
            GameObject EnterButton = InterfaceKeyboard.transform.Find("Enter").gameObject;
            EnterButton.SetActive(false);  
        }

    }

    public void DisplayFinalScore(int count){

      
        //disable interface keyboard
        GameObject InterfaceKeyboard = transform.Find("InterfaceKeyboard").gameObject;
        InterfaceKeyboard.SetActive(false);

        //set continue button active
        GameObject ContinueButton = transform.Find("continue").gameObject;
        ContinueButton.SetActive(true);

        finalScore = transform.Find("finalScore").gameObject;
        finalScore.SetActive(true);
        //continueButton.SetActive(true);
        finalScore.GetComponent<Text>().text = "Final score: " + yourScore + "/" + count;

    }

    public void HideFinalScore()
    {
        finalScore = transform.Find("finalScore").gameObject;
        finalScore.SetActive(false);

        //disable continue button 
        GameObject ContinueButton = transform.Find("continue").gameObject;
        ContinueButton.SetActive(false);
        gameObject.transform.parent.gameObject.SetActive(false);
        lessonDisplay.GetComponent<LessonDisplayScript>().changeWord();
        yourScore = 0;
    }

    public void ToggleClue(){
       
        if(clue.activeSelf){
            clue.SetActive(false); 
        }else{
            clue.SetActive(true);
        }

    }

}
