using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            if(Input.GetKeyDown(KeyCode.E)){
                // 门打开动画
                this.gameObject.setActive(false);
            }
        }
    }
}
