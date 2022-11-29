using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource DeathAudio;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        DeathAudio = this.transform.Find("DeathAudio").gameObject.GetComponent<AudioSource>();
        // Debug.Log(DeathAudio);
        Player = GameObject.Find("MainCharacter").transform.GetChild(0);
        Debug.Log(Player);
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void AudioPlay(AudioSource source)
    {
        // audioS.PlayOneShot(clip);
        source.Play();
    }
}
