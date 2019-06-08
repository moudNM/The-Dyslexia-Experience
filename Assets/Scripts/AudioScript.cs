using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    // the component that Unity uses to play your clip
    public AudioSource MusicSource;

    //play sound
    public void Play(AudioClip ac){
        MusicSource.clip = ac;
        MusicSource.Play();
    }
}