using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoorArea : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    public GameObject LeftBar, RightBar;
    public int open = 0;

    void Start()
    {
       UI = GameObject.Find("UI").transform.Find("Interaction").gameObject;
       Transform Door = GameObject.Find("Door").transform;
       LeftBar = Door.Find("LeftHold").gameObject;
       RightBar = Door.Find("RightHold").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(open == 1){
            OpenDoor();
        }
        else if(open == 2){
            UI.GetComponent<Canvas>().enabled = false;
            Destroy(this.gameObject);
        } 
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            if(Input.GetKeyDown(KeyCode.E)){
                open = 1;
            }
        }
    }

    public void OpenDoor(){

        Vector3 LeftAngles = LeftBar.transform.localEulerAngles;
        Vector3 RightAngles = RightBar.transform.localEulerAngles;

        Debug.Log(LeftAngles);
        if(LeftAngles.y < -90f){
            open = 2;
        }
        else{    
            LeftAngles.y -= 5f * Time.deltaTime;    
            RightAngles.y += 5f * Time.deltaTime;

            LeftBar.transform.localEulerAngles = LeftAngles;
            RightBar.transform.localEulerAngles = RightAngles;
        }
    }
}
