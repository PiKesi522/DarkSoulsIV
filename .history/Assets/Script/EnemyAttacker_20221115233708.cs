using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public float damage;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        damage = 20.0f;
        Player = GameObject.Find("DogPolyart");
        _self = this.gameObject.transform.gameObject.transform.gameObject.transform.gameObject;
        Debug.Log(_self)
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
        if(other.gameObject.name == "PlayerCore"){
            // Debug.Log(other.gameObject.name);
            // Debug.Log(Player.GetComponent<CharacterState>().readyToAttack);
            if(Player.GetComponent<CharacterState>().readyToAttack){
                if(Input.GetMouseButtonDown(0)){
                    // Debug.Log("Make Damage");
                    other.gameObject.transform.parent.GetComponent<NPCevent>().injury(damage);
                }
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
