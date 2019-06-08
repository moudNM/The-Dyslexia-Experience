using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonScript : MonoBehaviour
{
    public GameObject curr = null;
    public GameObject bubble = null;

    //NPC does action
    public void action()
    {
        speech();
    }

    //NPC talks
    void speech(){
        if (gameObject.transform.GetChild(0).name == "Bubble")
        {
            bubble = gameObject.transform.GetChild(0).gameObject;
            bubble.SetActive(!bubble.activeSelf);
        }
    }

    //NPC Collision
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            curr = obj.gameObject;
        }
    }

    //NPC exit collision
    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            if(bubble != null){
                bubble.SetActive(false);
                curr = null;
            }

        }
    }
}
