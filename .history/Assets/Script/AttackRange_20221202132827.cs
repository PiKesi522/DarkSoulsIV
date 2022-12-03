using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public GameObject enemy;
    public bool isBoss;
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject.transform.parent.gameObject;
        isBoss = (enemy.transform.parent.gameObject.name == "Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        // Debug.Log("Follow");
        if(other.gameObject.name == "PlayerCore"){
        // Debug.Log("Follow");
            if(isBoss){
                enemy.GetComponent<BossEvent>().AttackStart();
            }
            else{
                enemy.GetComponent<EnemyEvent>().AttackStart();
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name == "PlayerCore"){
        // Debug.Log("Not Follow");
            if(isBoss){
                enemy.GetComponent<BossEvent>().AttackStop();
            }
            else{
                enemy.GetComponent<EnemyEvent>().AttackStop();
            }
        }
        // Debug.Log("Not Follow");
    }
}
