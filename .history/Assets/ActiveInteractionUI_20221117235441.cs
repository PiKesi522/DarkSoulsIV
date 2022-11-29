using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInteractionUI : MonoBehaviour
{
    // public GameObject Player;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
       UI = GameObject.Find("UI").transform.Find("Interaction").gameObject;
    //    Debug.Log(UI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            UI.transform.GetChild(0).enabled = true;
            UI.transform.GetChild(1).enabled = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            UI.transform.GetChild(0).enabled = false;
            UI.transform.GetChild(1).enabled = false;
        }
    }
}
