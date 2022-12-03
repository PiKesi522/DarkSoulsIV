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
       Dialog = UI.transform.Find("Dialog").gameObject;
    }


    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            Interaction.GetComponent<Canvas>().enabled = true;
            if(Input.GetKeyDown(KeyCode.F)){
                startDialog();
            }

            if(Input.GetKeyDown(KeyCode.Escape)){
                leaveDialog();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            Interaction.GetComponent<Canvas>().enabled = false;
            leaveDialog();
        }
    }

    void startDialog(){
        Dialog.transform.Find("Background").gameObject.SetActive(true);
        Dialog.transform.Find("GuiderDialog").gameObject.SetActive(true);
        if()
        // GameObject DialogList = Dialog.transform.Find("GuiderList").gameObject;

    }

    void leaveDialog(){
        // Dialog.transform.Find("Background").enabled = false;
    }
}
