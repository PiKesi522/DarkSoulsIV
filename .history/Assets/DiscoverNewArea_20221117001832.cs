using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverNewArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            // Player.GetComponent<CharacterState>().injury(damage, false);
            // Debug.Log("hit");
        }
    }
}
