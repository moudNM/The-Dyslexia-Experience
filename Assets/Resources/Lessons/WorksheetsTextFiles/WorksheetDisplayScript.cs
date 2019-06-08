using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WorksheetDisplayScript : MonoBehaviour {

    public GameObject MasterInputControl;
    GameObject title;
    GameObject qn;
    GameObject ans;
    GameObject ansBar;
    GameObject scoring;
    GameObject finalScore;
    GameObject correctAns;
    GameObject keyboard;
    GameObject keyboardButtons;
    GameObject continueButton;
    List<char> keys;
    string yourAns;
    int worksheetScore;

    string worksheetID;
    string worksheetTitle;
    List<string> worksheetQuestions;    //all qns in lesson
    List<string> worksheetAnswers;      //all ans in lesson
    string originalAns;

    int currentQn = 0;                  //qn line number      
    string originalQuestion;            //unchanged qn
    string newQuestion;                 //
    List<string> questionWords;         //output qn as words
    string outputQuestion;              //output qn

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.E)){

        }
	}

	public void setup(string worksheetID){

        title = transform.Find("title").gameObject;
        qn = transform.Find("qn").gameObject;
        ans = transform.Find("ans").gameObject;
        ansBar = transform.Find("ansBar").gameObject;
        scoring = transform.Find("scoring").gameObject;
        correctAns = transform.Find("correctAns").gameObject;
        finalScore = transform.Find("finalScore").gameObject;
        continueButton = transform.Find("continue").gameObject;
        worksheetScore = 0;

        this.worksheetID = worksheetID + "W";
        readText(this.worksheetID);
        if (worksheetID != null)
        {
            title.GetComponent<Text>().text = worksheetTitle;
            qn.GetComponent<Text>().text = worksheetQuestions[0];
            originalAns = worksheetAnswers[0].ToLower();
        }
        originalQuestion = worksheetQuestions[currentQn];
        newQuestion = originalQuestion;
        InvokeRepeating("MakeChanges", 0.0f, 0.15f);

        //setup keyboard
        keyboard = transform.Find("WorksheetKeyboard").gameObject;
        keyboard.SetActive(true);
        //randomise keyboard
        keyboardButtons = keyboard.transform.Find("Buttons").gameObject;
        keyboardButtonsSetup();
    }

    void readText(string filename)
    {
        //TITLE and qns
        string path = "Lessons/WorksheetsTextFiles/" + filename + "Q";
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        worksheetQuestions = new List<string>(textAsset.text.Split('\n'));

        worksheetTitle = worksheetQuestions[0];//set title
        worksheetQuestions.RemoveAt(0);

        //ans
        path = "Lessons/WorksheetsTextFiles/" + filename + "A";
        textAsset = Resources.Load<TextAsset>(path);
        worksheetAnswers = new List<string>(textAsset.text.Split('\n'));

        worksheetAnswers.RemoveAt(0);


    }

    //repeat and/or change sentence on action key
    public void action()
    {
       
    }

    //hide lesson
    public void HideWorksheet(){

        //title.SetActive(false);
        qn.SetActive(false);
        ans.SetActive(false);
        scoring.SetActive(false);
        correctAns.SetActive(false);
        ansBar.SetActive(false);

        //disable all keyboard
        keyboard.SetActive(false);
        scoring.SetActive(false);
        correctAns.SetActive(false);
        //finalScore.SetActive(false);
    }

    //terminates lesson display
    public void teardown()
    {
        CancelInvoke("MakeChanges");
        worksheetTitle = null;
        worksheetQuestions = null;
        worksheetAnswers = null;
        worksheetID = null;

        currentQn = 0;         
        originalQuestion = null;      
        newQuestion = null;          
        questionWords = null;     
        outputQuestion = null;

        //clear all text
        yourAns = "";
        ans.GetComponent<Text>().text = yourAns;
        title.GetComponent<Text>().text = "";
        qn.GetComponent<Text>().text = "";
        ans.GetComponent<Text>().text = "";
        scoring.GetComponent<Text>().text = "";
        correctAns.GetComponent<Text>().text = "";
    }

    public void changeQuestion()
    {
        
        if(currentQn < worksheetQuestions.Count){
            currentQn += 1;  
        }

        if (currentQn >= (worksheetQuestions.Count))
        {
            //terminate
            finalScore.SetActive(true);
            continueButton.SetActive(true);
            finalScore.GetComponent<Text>().text = "Final score: " + worksheetScore + "/" + worksheetAnswers.Count;
            HideWorksheet();//hide everything

        }
        else
        {
            //move to next sentence
            originalQuestion = worksheetQuestions[currentQn];
            newQuestion = originalQuestion;
            originalAns = worksheetAnswers[currentQn].ToLower();
            keyboardButtonsSetup();
        }
    }

    void MakeChanges()
    {
        newQuestion = SentenceLetterScrambler.fromPtoQ(newQuestion);
        newQuestion = SentenceLetterScrambler.fromBtoD(newQuestion);
        questionWords = splitSentence(newQuestion);
        outputQuestion = formSentences(questionWords);
        qn.GetComponent<Text>().text = outputQuestion;
    }

    //splits sentences into words
    List<string> splitSentence(string sentence)
    {
        char[] separators = { ',', '.', '?', ' ' };

        List<string> outputWords = new List<string>(sentence.Split(separators));

        while (outputWords.Contains(""))
        {
            outputWords.Remove("");
        }

        return outputWords;
    }




    string formSentences(List<string> outputWords)
    {

        string outputSentence = "";
        int noOfLettersOnCurrentLine = 0;
        for (int i = 0; i < outputWords.Count; i++)
        {
            if (!(noOfLettersOnCurrentLine + outputWords[i].Length <= 20))
            {
                outputSentence += Environment.NewLine;
                noOfLettersOnCurrentLine = 0;
            }

            outputSentence += outputWords[i];
            noOfLettersOnCurrentLine += outputWords[i].Length;
            if (i == outputWords.Count - 1)
            {
                outputSentence += ".";
            }
            else
            {
                outputSentence += " ";
            }



        }

        return outputSentence;
    }

    public void AddLetter(char c){
        yourAns += c;
        ans.GetComponent<Text>().text = yourAns;
    }


    public void DelLetter()
    {
        if(yourAns.Length>0){
            yourAns = yourAns.Remove(yourAns.Length - 1);
      
            ans.GetComponent<Text>().text = yourAns;   
        }
    }

    public void keyboardButtonsSetup()
    {
        //add ans to keyboard
        char[] c = originalAns.ToCharArray();
        keys = new List<char>(c);
        keys = keys.Distinct().ToList();

        //add other characters to keyboard
        List<char> otherKeys = new List<char>();
        for(int i = 97;i<= 122;i++){
            otherKeys.Add((char)i);   
        }

        //remove ans characters from other
        foreach(char character in keys){
            otherKeys.Remove(character);
        }


        System.Random rnd = new System.Random();
        int noOfButtonsLeft = keyboardButtons.transform.childCount;
        bool[] assignArray = { true, false, true, false, true, false };

        foreach (Transform item in keyboardButtons.transform)
        {
            bool assign = false;

            if (keys.Count >= noOfButtonsLeft)
            {
                assign = true;
            }
            else
            {
                int boolIndex = rnd.Next(0, assignArray.Length);
                assign = assignArray[boolIndex];
            }

            if (assign && keys.Count>0){
                    //assign ans letter
                int keyIndex = rnd.Next(0, keys.Count);
                item.GetComponent<ButtonText>().setCharacter(keys[keyIndex]);
                keys.RemoveAt(keyIndex);
            }
            else
            {
                //assign others
                int otherKeyIndex = rnd.Next(0, otherKeys.Count);
                item.GetComponent<ButtonText>().setCharacter(otherKeys[otherKeyIndex]);
                otherKeys.RemoveAt(otherKeyIndex);
            }
            noOfButtonsLeft--;
               
        }



    }

    public void submitAnswer(){

        //show correct/wrong and correct ans
        if(scoring.activeSelf == false){
            //show result and ans
            showAnswer();
            GameObject enter = keyboard.transform.Find("Enter").gameObject;
            enter.GetComponent<ButtonText>().setBigCharacter("next");

        }else{
            //hide
            hideAnswer();
            GameObject enter = keyboard.transform.Find("Enter").gameObject;
            enter.GetComponent<ButtonText>().setBigCharacter("enter");
        }

    }

    void showAnswer(){
        if(originalAns == yourAns){
            worksheetScore += 1;
            scoring.GetComponent<Text>().text = "Correct!";
            scoring.GetComponent<Text>().color = Color.green;
            //green 24B264 D92222 red
        }else{
            scoring.GetComponent<Text>().text = "Wrong!";
            scoring.GetComponent<Text>().color = Color.red;
        }

        correctAns.GetComponent<Text>().text = "Correct answer: " + originalAns;
        scoring.SetActive(true);
        correctAns.SetActive(true);

    }
    void hideAnswer(){
        scoring.SetActive(false);
        correctAns.SetActive(false);
        //change qns
        changeQuestion();
        //clear ans
        yourAns = "";
        ans.GetComponent<Text>().text = yourAns;
    }

    public void EndWorksheet(){

        //end worksheet

        //Depending on prev worksheet, different events/modes triggered

        switch(worksheetID){
          
            case "L000W":
                MasterInputControl.GetComponent<MasterInputControlScript>().changeMode("lessonMode","L001");
                teardown();
                break;

            default:
                MasterInputControl.GetComponent<MasterInputControlScript>().triggerGameMode();
                break;
        }

    }

}
