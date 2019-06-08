using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    Scene currentScene;

    void Start(){
        //track current scene
        currentScene = SceneManager.GetActiveScene();
    }

    //load scene based on current scene
    public void loadScene(){
        if(currentScene.name == "StartScreen"){
            SceneManager.LoadScene("Classroom");
        }else if (currentScene.name == "Classroom")
        {
            SceneManager.LoadScene("EndScreen");
        }else if (currentScene.name == "EndScreen")
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

    //open survey link
    public void externalLink(){
        Application.ExternalEval("window.open(\"//forms.gle/xGcsz54jvAkz9J6L8\")");
    }

}
