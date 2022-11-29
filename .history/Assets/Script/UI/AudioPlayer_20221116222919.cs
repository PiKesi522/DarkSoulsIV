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
}
