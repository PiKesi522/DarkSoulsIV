using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Death; 
    // Start is called before the first frame update
    void Start()
    {
        Death = transform.Find("Death").GetComponent<AudioSource>();
        PlayAudio("Die");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAudio(string name){
        AudioClip audio = (AudioClip)Resources.Load("Audio/" + name, typeof(AudioClip));//"Scenes"为音频在工程中存放的位置
        Death.clip = audio;  //播放
    }
}
