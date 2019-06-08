using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LessonManagerScript : MonoBehaviour
{

    public GameObject MasterInputControl;
    bool inputEnabled = false;

    public string lessonID = "";


    //use this for interactivity
    Lesson lesson;
    public GameObject lessonDisplay;
    public GameObject teacher;
    public GameObject worksheetDisplay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (inputEnabled)
        {
            //while can interact
            if (Input.GetKeyDown(KeyCode.E))
            {
                action();
            }
        }
        else
        {
            action();
        }

    }

    //run lesson
    public void runLesson(string id)
    {
        lessonID = id;
        lesson = new Lesson(lessonID);
        lessonDisplay.GetComponent<LessonDisplayScript>().enabled = true;
        lessonDisplay.GetComponent<LessonDisplayScript>().setup(id);
    }

    //user action
    public void action()
    {
        //if enabled
        if (lessonDisplay.GetComponent<LessonDisplayScript>().enabled)
        {
            //if preless > 0 
            if (lesson.preLessonFunctions.Count > 0)
            {
                lesson.action();

                if(lesson.preLessonFunctions.Count == 0){
                    
                    lessonDisplay.GetComponent<LessonDisplayScript>().action();
                }
            }
            else
            {
                //if lesson has actions
                lessonDisplay.GetComponent<LessonDisplayScript>().action();
            }


        }
        //if disabled.
        if (!lessonDisplay.GetComponent<LessonDisplayScript>().enabled)
        {
            
           
            //if post > 0
            if (lesson.postLessonFunctions.Count > 0)
            {
                lesson.action();

                if (lesson.postLessonFunctions.Count == 0)
                {
                    if (lessonID == "L000")
                    {
                        addWorksheet(lessonID);//add worksheets etc.   
                        changeMode("gameMode", null);//terminate
                    }
   
                }
            }

        }
    }

    //add a worksheet
    public void addWorksheet(string lessonID){
        worksheetDisplay.SetActive(true);
        worksheetDisplay.GetComponent<WorksheetDisplayScript>().setup(lessonID);
    }

    //set input
    public void setInput(bool b)
    {
        inputEnabled = b;
    }

    //change game mode
    public void changeMode(string mode, string id)
    {
        setInput(false);
        lesson = null;
        MasterInputControl.GetComponent<MasterInputControlScript>().changeMode(mode, id);
    }

}

