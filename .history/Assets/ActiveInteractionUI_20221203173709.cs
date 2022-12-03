using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInteractionUI : MonoBehaviour
{
    public GameObject UI;
    
    void Start()
    {
       UI = GameObject.Find("UI").transform.Find("Interaction").gameObject;
    }


    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            UI.GetComponent<Canvas>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            UI.GetComponent<Canvas>().enabled = false;
        }
    }
}
