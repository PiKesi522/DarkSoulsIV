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
        if(other.gameObject.name == "PlayerCore"){
            enemy.GetComponent<EnemyEvent>().AttackStart();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            enemy.GetComponent<EnemyEvent>().AttackStop();
        }
        // Debug.Log("Not Follow");
    }
}
