using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechSortingLayer : MonoBehaviour {

    public string layerName;
	
	void Start () {
        //set speech to a layer
        GetComponent<Renderer>().sortingLayerName = layerName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
