using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject Player;
    private AudioSource audioS;
    public AudioClip Death;
    // Start is called before the first frame update
    void Start()
    {
        // DeathAudio = this.transform.Find("DeathAudio").gameObject.GetComponent<AudioSource>();
        // Debug.Log(DeathAudio);
        audioS = GetComponent<AudioSource>();
        Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<CharacterState>().isDead){
            audioS.clip = Death;
            Debug.Log("Play Death Clip");
            // AudioPlay(DeathAudio);
            audioS.Play();
        }
    }
 
    // public void AudioPlay(AudioSource source)
    // {
    //     // audioS.PlayOneShot(clip);
    //     source.Play();
    // }
}
