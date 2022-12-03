using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertRange : MonoBehaviour
{
    public GameObject enemy;
    public bool isBoss;
    public GameObject AM;
    // Start is called before the first frame update
    void Start()
    {
        // enemy = GameObject.Find("DogPBR");
        enemy = this.gameObject.transform.parent.gameObject;
        // Debug.Log(enemy.transform.parent.gameObject.name);
        isBoss = (enemy.transform.parent.gameObject.name == "Boss");
        // Debug.Log(enemy);
        AM = GameObject.Find("AudioManager/SoundEffect").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        // Debug.Log("Follow");
        if(other.gameObject.name == "PlayerCore"){

        }
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
