using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AM;
    public GameObject UI;

    void Start()
    {
        AM = GameObject.Find("AudioManager").gameObject;
        UI = GameObject.Find("Boss").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<MeshCollider>().enabled = true;
            AM.GetComponent<AudioManager>().AudioPlayBoss();
        }
    }
}
