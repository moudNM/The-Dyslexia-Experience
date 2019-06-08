using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBoxManagerScript : MonoBehaviour {

    public GameObject MasterInputControl;
    public GameObject curr;
    public EventBox eventBox;
    bool inputEnabled = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update() {
        if(inputEnabled){
            if (Input.GetKeyDown(KeyCode.E))
            {
                action();
            }  
        }else{
            action();
        }

	}

    //trigger an event
    public void triggerEvent(string eventID){
        eventBox = new EventBox(eventID);
    }

    //this handles all inputs now
    public void action(){
        eventBox.action();
    }

    //toggle input
    public void setInput(bool b){
        inputEnabled = b;
    }

    //change mode
    public void changeMode(string mode,string id){
        setInput(false);
        eventBox = null;
        MasterInputControl.GetComponent<MasterInputControlScript>().changeMode(mode,id);
    }


}
