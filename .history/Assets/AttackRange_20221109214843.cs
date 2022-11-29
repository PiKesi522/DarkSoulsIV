using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        Debug.Log("Follow");
    }

    void OnTriggerExit(Collider other){
        Debug.Log("Not Follow");
    }
}
