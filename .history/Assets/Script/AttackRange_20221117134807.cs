using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerStay(Collider other){
        // Debug.Log("Follow");
        enemy.GetComponent<NPCevent>().AttackStart();
    }

    void OnTriggerExit(Collider other){
        enemy.GetComponent<NPCevent>().AttackStop();
        // Debug.Log("Not Follow");
    }
}
