using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
	

	void Start () {
        //calculate offset for camera with respect to user position
        transform.position = player.transform.position + new Vector3(0f,2.5f,-10f);
        offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
        //updates camera position based on user position
        transform.position = player.transform.position + offset;
	}
}
