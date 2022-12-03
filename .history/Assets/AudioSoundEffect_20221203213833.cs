using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSoundEffect : MonoBehaviour
{
    private AudioSource audioS;
    public AudioClip Attack1, Attack2, LevelUp, Alert;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        // Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
        // GM = GameObject.Find("GameManager").gameObject;
    }
    
    public void AudioPlayAttack(){
        if(Random.Range(0f,2f) > 0.9f){
            audioS.clip = Attack1;
        }
        else{
            audioS.clip = Attack2;
        }
        audioS.loop = false;
        audioS.Play();
    }

    public void AudioPlayLevelUp(){
        audioS.clip = LevelUp;
        audioS.loop = false;
        audioS.Play();
    }
}