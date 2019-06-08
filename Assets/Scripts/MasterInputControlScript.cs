using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInputControlScript : MonoBehaviour {

    public GameObject EventBoxManager;
    public GameObject LessonManager;
    public GameObject tableCanvas;
    public GameObject player;
    public GameObject TipDisplay;

    int gameMode;

	// Use this for initialization
	void Start () {
        triggerGameMode();
	}
	
    //reset mode
    public void resetModes(){
        //disable or limit functionalities
        player.GetComponent<PlayerMovement>().enabled = false; //disable gameMode
        tableCanvas.SetActive(false); //disable tableMode
        EventBoxManager.SetActive(false);//disableEventMode
        LessonManager.SetActive(false);//disableLessonMode

    }

    //trigger game mode (player can move)
    public void triggerGameMode()
    {
        //update Mode
        resetModes();
        gameMode = 0;

        //set game to be active
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    //trigger table mode (table active)
    public void triggerTableMode(){

        //update Mode
        resetModes();
        gameMode = 2;

        //set table to be active
        tableCanvas.SetActive(true);
        TipDisplay.GetComponent<TipDisplayScript>().MovableCheck = true;
        TipDisplay.GetComponent<TipDisplayScript>().InteractCheck = true;
        TipDisplay.GetComponent<TipDisplayScript>().InteractCheck2 = true;
        TipDisplay.GetComponent<TipDisplayScript>().InteractCheck3 = true;
        TipDisplay.GetComponent<TipDisplayScript>().DisableTip();

    }

    //trigger event mode
    public void triggerEventMode(GameObject obj){
        string id = obj.GetComponent<ObjectID>().id;
        obj.SetActive(false);
        triggerEventMode(id);
    }

    //trigger event mode
    public void triggerEventMode(string eventID){
       
        //update Mode
        resetModes();
        gameMode = 3;

        //set event active
        EventBoxManager.SetActive(true);
        EventBoxManager.GetComponent<EventBoxManagerScript>().triggerEvent(eventID);
    }

    //trigger lesson mode
    public void triggerLessonMode(string lessonID)
    {
        //update Mode
        resetModes();
        gameMode = 4;

        //set lesson active
        LessonManager.SetActive(true);
        LessonManager.GetComponent<LessonManagerScript>().runLesson(lessonID);
    }

    //change of modes
    public void changeMode(string mode, string id){

        switch(mode){
            case "lessonMode":
                triggerLessonMode(id);
                break;
            case "eventMode":
                triggerEventMode(id);
                break;
            default:
                triggerGameMode();
                break;
        }
    }
}
