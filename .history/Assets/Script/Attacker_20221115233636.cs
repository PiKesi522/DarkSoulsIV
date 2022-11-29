using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float damage;
    public string[] enemyList;
    private GameObject _self;
    // Start is called before the first frame update
    void Start()
    {
        damage = 30.0f;
        enemyList = new string[2] {"EnemyCore", "Boss"};
        _self = this.gameObject.transform.gameObject.transform.gameObject.transform.gameObject;
        Debug.Log(_self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnTriggerEnter(Collider other){
    //     // Debug.Log(other.gameObject.name);
    //     for(int i = 0; i < enemyList.Length; i++){
    //         if(other.gameObject.name == enemyList[i]){
    //             Debug.Log(other.gameObject.name);
    //         }
    //     }
    // }

    void OnTriggerStay(Collider other){
        for(int i = 0; i < enemyList.Length; i++){
            if(other.gameObject.name == enemyList[i]){
                Debug.Log(other.gameObject.name);
                // Debug.Log(Player.GetComponent<CharacterState>().readyToAttack);
                // if(Player.GetComponent<CharacterState>().readyToAttack){
                //     if(Input.GetMouseButtonDown(0)){
                //         // Debug.Log("Make Damage");
                //         other.gameObject.transform.parent.GetComponent<NPCevent>().injury(damage);
                //     }
                // }
            }
        }
    }

    
    // void OnTriggerExit(Collider other){
    //     // Debug.Log(other.gameObject.name);
    //     for(int i = 0; i < enemyList.Length; i++){
    //         if(other.gameObject.name == enemyList[i]){
    //             Debug.Log(other.gameObject.name);
    //         }
    //     }
    // }
}
