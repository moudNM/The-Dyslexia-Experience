using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
   
    public string itemType = null;
    public GameObject myTable;

    //item does something
    public void action()
    {
        if (itemType == "trigger")
        {
            
        }
        else if (itemType == "playerTable")
        {
            GameObject.Find("MasterInputControl").GetComponent<MasterInputControlScript>().triggerTableMode();
   

        }
    }
}
