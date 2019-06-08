using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class LessonDisplayScript : MonoBehaviour
{
    public GameObject lessonManager;
    string lessonFile;              //lesson to load
    string lessonID;
    bool end = false; //lesson ended

    bool tutorialEnabled = false;
    public GameObject tutorial;
    string tutorialTitle;
    Sprite[] tutorialImages;
    List<string> tutorialTexts;
    int current = 0;
    public GameObject LessonInterfaceCanvas;

    //interactability
    bool interactableLesson = false;
    bool interactable = false;
    //audio lesson
    AudioClip[] audioClips;
    public AudioSource MusicSource;

    //word lesson
    List<string> lessonWords;
    int currentWord = 0;
    string originalWord;
    string newWord;                 //output word
    bool displayFinalScore = true;

    //sentence lesson
    List<string> lessonSentences;   //all sentences in a lesson
    int currentSentence = 0;        //sentence line number  
    string originalSentence;        //unchanged sentence  
    string newSentence;             //unchanged sentence  
    List<string> sentenceAsWords;   //output sentence as words
    string outputSentence;          //output sentence

    //display sentence on board
    public void setup(string lessonID)
    {
        this.lessonID = lessonID;
        //if interactable, set lesson interactable
        interactableLesson = checkInteractable();
        //load all resources for the lesson
        LoadLessonResources();

        GetComponent<TextMesh>().text = "";
        lessonFile = lessonID;
        end = false;

        if (interactableLesson)
        {
            readTextWords(lessonFile);//load words from a lesson txt file
            originalWord = lessonWords[currentWord];
            //set font for lesson 1 to shapes
            if (lessonID == "L001")
            {
                Font f = Resources.Load<Font>("Fonts/Shapesfont");
                GetComponent<TextMesh>().font = f;
                GetComponent<MeshRenderer>().material = f.material;
            }


            GetComponent<TextMesh>().text = originalSentence;

        }
        else
        {
            readText(lessonFile);//load sentences from a lesson txt file
            originalSentence = lessonSentences[currentSentence];
            newSentence = originalSentence;

            Font f = Resources.Load<Font>("Fonts/Chalker-Regular");
            GetComponent<TextMesh>().font = f;
            GetComponent<MeshRenderer>().material = f.material;

            GetComponent<TextMesh>().text = originalSentence;
        }

    }

    //repeat and/or change sentence on action key
    public void action()
    {
        if (!tutorialEnabled && !tutorial.activeSelf)
        {
            //IF Tut exists....
            if (checkTutorial(lessonID))
            {
                TutorialSetup();
            }
            else
            {
                interactable = true;
            }
        }

        if (!tutorial.activeSelf && !end)
        {
            //only accessible if tutorial not active
            if (interactableLesson)
            {
                InteractAction();
            }
            else
            {
                if (currentSentence == 0)
                {
                    changeSentence(); //change sentence and split into words
                    InvokeRepeating("MakeChanges", 0.0f, 0.15f);
                }
                else
                {
                    changeSentence();
                }
            }
        }


    }

    //use this for interaction
    public void InteractAction()
    {

        if (interactable)
        {
            //LESSON L001
            if (lessonID == "L001")
            {
                if (currentWord == 0)
                {
                    interactable = false;
                    DisplayWord();//display symbols
                    PlaySound();//play sound

                    StartCoroutine(InterfaceDisplaySetup());

                }
                else
                {
                    if (LessonInterfaceCanvas.activeSelf)
                    {
                        LessonInterfaceCanvas.SetActive(false);
                        MakeChangesWord();
                        DisplayWord();//display symbols
                        PlaySound();//play sound

                        StartCoroutine(InterfaceDisplaySetup());
                    }
                }


            }

            //LESSON L002
            if (lessonID == "L002")
            {
                if (currentWord == 0)
                {
                    interactable = false;
                    PlaySoundFreq();//play sound

                    StartCoroutine(InterfaceDisplaySetup());

                }
                else
                {
                    if (LessonInterfaceCanvas.activeSelf)
                    {
                        LessonInterfaceCanvas.SetActive(false);
                        MakeChangesWord();
                        PlaySound();//play sound

                        StartCoroutine(InterfaceDisplaySetup());
                    }
                }


            }


        }


    }

    //use interface
    IEnumerator InterfaceDisplaySetup()
    {
        if (lessonID == "L001")
        {
            yield return new WaitForSeconds(2.0f);
            MakeChangesWord();
            GetComponent<TextMesh>().text = "";
            LessonInterfaceCanvas.transform.Find("InterfaceDisplay").gameObject.GetComponent<InterfaceDisplayScript>().Setup(newWord, lessonID);
            LessonInterfaceCanvas.SetActive(true);
        }
        else if (lessonID == "L002")
        {
            yield return new WaitForSeconds(2.0f);
            MakeChangesWord();
            GetComponent<TextMesh>().text = "";
            LessonInterfaceCanvas.transform.Find("InterfaceDisplay").gameObject.GetComponent<InterfaceDisplayScript>().Setup(newWord, lessonID);
            LessonInterfaceCanvas.SetActive(true);
        }


    }

    public string getNewWord()
    {
        return newWord;
    }


    //tutorial boolean markers
    public void DismissTutorial()
    {
        tutorialEnabled = true;
        tutorial.SetActive(false);
        interactable = true;
        action();
    }

    //terminates lesson display
    public void teardown()
    {
        CancelInvoke("MakeChanges");
        GetComponent<TextMesh>().text = "";
        lessonFile = null;
        lessonSentences = null;
        currentSentence = 0;
        originalSentence = null;
        newSentence = null;
        sentenceAsWords = null;
        outputSentence = null;
        displayFinalScore = true;

        if (lessonID == "L001")
        {
            lessonManager.GetComponent<LessonManagerScript>().setInput(false);
        }
        else if (lessonID == "L002")
        {
            gameObject.GetComponent<SceneLoader>().loadScene();

        }

        lessonID = null;
        interactableLesson = false;
        LessonInterfaceCanvas.SetActive(false);
        tutorialEnabled = false;
        end = true;
        currentWord = 0;

        lessonManager.GetComponent<LessonManagerScript>().action();

        enabled = false; //disables lessondisplayscript

    }


    public void changeWord()
    {
     
        if (currentWord >= (lessonWords.Count - 1))
        {

            if ((displayFinalScore && lessonID == "L001") || (displayFinalScore && lessonID == "L002"))
            {
                displayFinalScore = false;
                //terminate
                LessonInterfaceCanvas.transform.Find("InterfaceDisplay").gameObject.GetComponent<InterfaceDisplayScript>().DisplayFinalScore(lessonWords.Count);
           

            }else{
                //terminate         
                teardown(); 
          
            }

        }
        else
        {
           
            //move to next word
            currentWord += 1;
            originalWord = lessonWords[currentWord];
            interactable = true;
            action();
        }
    }

    public void changeSentence()
    {
        if (currentSentence >= (lessonSentences.Count - 1))
        {
         
            //terminate
            teardown();
        }
        else
        {
            //move to next sentence
            currentSentence += 1;
            originalSentence = lessonSentences[currentSentence];
            newSentence = originalSentence;




        }
    }

    void readTextWords(string filename)
    {
        string path = "Lessons/LessonsTextFiles/" + lessonID;
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        lessonWords = new List<string>(textAsset.text.Split('\n'));

      
    }


   


    void readText(string filename)
    {
        string path = "Lessons/LessonsTextFiles/" + lessonID;
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        lessonSentences = new List<string>(textAsset.text.Split('\n'));


    }

    void MakeChanges()
    {
        newSentence = SentenceLetterScrambler.fromPtoQ(newSentence);
        newSentence = SentenceLetterScrambler.fromBtoD(newSentence);
        sentenceAsWords = splitSentence(newSentence);
        outputSentence = formSentences(sentenceAsWords);
        GetComponent<TextMesh>().text = outputSentence;
    }

    void MakeChangesWord()
    {
        newWord = originalWord;
        newWord = SentenceLetterScrambler.scrambleWord(newWord);
    }

    void DisplayWord(){
        GetComponent<TextMesh>().text = originalWord;
    }

    void HideWord(){
        GetComponent<TextMesh>().text = "";
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

        outputSentence = "";
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

    public bool checkInteractable(){

        switch(lessonID){
            case "L001":
                return true;
            case "L002":
                return true;
            default:
                return false;
        }
    }

    public bool checkTutorial(string lessonID)
    {
        switch (lessonID)
        {
            case "L000":
                return true;
            case "L001":
                return true;
            case "L002":
                return true;
            default:
                return false;
        }
    }

    public void LoadLessonResources(){
        switch (lessonID)
        {
            case "L001":
                AudioFilesSetup();
                break;
            case "L002":
                AudioFilesSetup();
                break;
            default:
                break;
        }
    }

    public void AudioFilesSetup(){
        string audioName = "Audio/" + lessonID;
        audioClips = Resources.LoadAll<AudioClip>(audioName);
    }





    public void TutorialSetup(){

        //use lessonID to set up text
        string path = "Lessons/LessonsTextFiles/Tutorial/" + lessonID;

        TextAsset textAsset = Resources.Load<TextAsset>(path);

        tutorialTexts = new List<string>(textAsset.text.Split('\n'));

        SetTutorialTitle();


        current = 0;
        SetTutorialText(current);

        //use lessonID to set images
        string spriteName = "Sprites/Tutorial/" + lessonID;
        tutorialImages = Resources.LoadAll<Sprite>(spriteName);
        SetTutorialImage(current);

        //set slideNo
        SetTutorialSlideNo();

        //make visible
        tutorial.SetActive(true); 
    }

    public void SetTutorialTitle()
    {
        GameObject display = tutorial.transform.Find("TutorialDisplay").gameObject;
        GameObject tutName = display.transform.Find("TutorialName").gameObject;
        tutName.GetComponent<Text>().text = tutorialTitle;
    }

    public void SetTutorialImage(int i){
        GameObject display = tutorial.transform.Find("TutorialDisplay").gameObject;
        GameObject image = display.transform.Find("TutorialImage").gameObject;
        image.GetComponent<Image>().sprite = tutorialImages[i];
    }

    public void SetTutorialText(int i){
        GameObject display = tutorial.transform.Find("TutorialDisplay").gameObject;
        GameObject text = display.transform.Find("TutorialText").gameObject;
        text.GetComponent<Text>().text = tutorialTexts[i];
    }

    public void SetTutorialSlideNo()
    {
        GameObject display = tutorial.transform.Find("TutorialDisplay").gameObject;
        GameObject text = display.transform.Find("TutorialSlideNo").gameObject;
        text.GetComponent<Text>().text = (current+1) + "/" + (tutorialTexts.Count - 1);

    }

    public void PlaySound(){
        MusicSource.GetComponent<AudioScript>().Play(audioClips[currentWord]);
    }

    public void PlaySoundFreq()
    {
        //randomly generate a pitch
        float pitch = UnityEngine.Random.Range(1.4f, 2.0f);
        MusicSource.pitch = pitch;
        MusicSource.GetComponent<AudioScript>().Play(audioClips[currentWord]);
        //MusicSource.pitch = 1.0f;
    }

    public void Next(){

        current += 1;

        if (current >= tutorialImages.Length)
        {
            current = tutorialImages.Length - 1;
        }

        SetTutorialImage(current);
        SetTutorialText(current);
        SetTutorialSlideNo();
    }

    public void Prev()
    {
        current -= 1;

        if(current < 0){
            current = 0;
        }


        SetTutorialImage(current);
        SetTutorialText(current);
        SetTutorialSlideNo();
    }

    public string GetOriginalSentence(){
        return originalSentence;
    }

    public string GetOriginalWord()
    {
        return originalWord;
    }
}
