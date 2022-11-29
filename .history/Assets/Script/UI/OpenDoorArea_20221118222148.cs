using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoorArea : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    public Transform LeftBar, RightBar;
    void Start()
    {
       UI = GameObject.Find("UI").transform.Find("Interaction").gameObject;
       Transform Door = GameObject.Find("Door").transform;
       LeftBar = Door.Find("LeftBar").transform;
       RightBar = Door.Find("RightBar").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            if(Input.GetKeyDown(KeyCode.E)){
                // 门打开动画
                UI.GetComponent<Canvas>().enabled = false;
                Destroy(this.gameObject);
            }
        }
    }

    public void OpenDoor(){
        
        Vector3 angles = this.transform.localEulerAngles;
        angles.y += 0.5f;    
        this.transform.localEulerAngles=angles;
    }
}
