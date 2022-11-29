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
        Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<CharacterState>().isDead){
            Debug.Log("Play Death Clip");
            AudioPlay(DeathAudio);
        }
    }
 
    public void AudioPlay(AudioSource source)
    {
        // audioS.PlayOneShot(clip);
        source.Play();
    }
}
