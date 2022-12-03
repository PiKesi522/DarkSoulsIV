using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiderEvent : MonoBehaviour
{
    public GameObject UI;
    public GameObject Interaction;
    public GameObject Dialog;

    void Start()
    {
       UI = GameObject.Find("UI").gameObject;
       Interaction = UI.transform.Find("Interaction").gameObject;
    }


    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            UI.GetComponent<Canvas>().enabled = true;
            if(Input.GetKeyDown(KeyCode.F)){
                Dialog();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            UI.GetComponent<Canvas>().enabled = false;
        }
    }

    void Dialog(){

    }
}
