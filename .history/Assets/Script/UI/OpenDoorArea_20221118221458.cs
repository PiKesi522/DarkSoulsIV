using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoorArea : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    void Start()
    {
       UI = GameObject.Find("UI").transform.Find("Interaction").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            if(Input.GetKeyDown(KeyCode.E)){
                // 门打开动画
                this.gameObject.GetComponent<ActiveInteractionUI>().enabled = false;
                Destroy(this.gameObject);
            }
        }
    }
}
