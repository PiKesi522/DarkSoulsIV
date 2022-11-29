using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverNewArea : MonoBehaviour
{
    public bool discover;
    public static GameObject AM;
    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.Find("AudioManager").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            beDiscovered();
            // Player.GetComponent<CharacterState>().injury(damage, false);
            // Debug.Log("hit");
        }
    }

    void beDiscovered(){
        AM.GetComponent<AudioManager>().AudioPlayDiscovery();
        this.discover = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
