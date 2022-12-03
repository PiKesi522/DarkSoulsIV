using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertRange : MonoBehaviour
{
    public GameObject enemy;
    public bool isBoss;
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
        if(other.gameObject.name == "PlayerCore"){
            if(isBoss){
                enemy.GetComponent<BossEvent>().ChaseStart();
            }
            else{
                enemy.GetComponent<EnemyEvent>().ChaseStart();
            }
        }
        // this
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            if(isBoss){
                enemy.GetComponent<BossEvent>().ChaseStop();
            }
            else{
                enemy.GetComponent<EnemyEvent>().ChaseStop();
            }
        }
    }
}
