using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertRange : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        // enemy = GameObject.Find("DogPBR");
        enemy = this.gameObject.transform.parent.gameObject;
        // Debug.Log(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        // Debug.Log("Follow");
        enemy.GetComponent<EnemyEvent>().ChaseStart();
        // this
    }

    void OnTriggerExit(Collider other){
        enemy.GetComponent<EnemyEvent>().ChaseStop();
        // Debug.Log("Not Follow");
    }
}
