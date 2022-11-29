using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject GM;
    private AudioSource audioS;
    public AudioClip Death, Discovery;
    // Start is called before the first frame update
    void Start()
    {
        // DeathAudio = this.transform.Find("DeathAudio").gameObject.GetComponent<AudioSource>();
        // Debug.Log(DeathAudio);
        audioS = GetComponent<AudioSource>();
        Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
        GM = GameObject.Find("GameManager").gameObject;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && GM.GetComponent<Pause>().isPaused){
            audioS.Stop();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !GM.GetComponent<Pause>().isPaused){
            audioS.Play();
        }
    }

    
    public void AudioPlayDeath(){
        audioS.clip = Death;
        audioS.Play();
    }

    public void AudioPlayDiscovery(){
        audioS.clip = Discovery;
        audioS.Play();
    }
    // public void AudioPlay(AudioSource source)
    // {
    //     // audioS.PlayOneShot(clip);
    //     source.Play();
    // }
}
