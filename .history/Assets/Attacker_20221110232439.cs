using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float damage;
    public string[] enemyList;
    public static GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        damage = 30.0f;
        enemyList = new string[2] {"EnemyCore", "Boss"};
        Player = GameObject.Find("DogPolyart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        // Debug.Log(other.gameObject.name);
        for(int i = 0; i < enemyList.Length; i++){
            if(other.gameObject.name == enemyList[i]){
                // Debug.Log(other.gameObject.name);
            }
        }
    }

    void OnTriggerStay(Collider other){
        // Debug.Log(other.gameObject.name);
        for(int i = 0; i < enemyList.Length; i++){
            if(other.gameObject.name == enemyList[i]){
                // Debug.Log(Player.GetComponent<CharacterState>().readyToAttack);
                if(Player.GetComponent<CharacterState>().readyToAttack){
                    if(Input.GetMouseButtonDown(0)){
                        Debug.Log("Make Damage");
                        Debug.Log(other.gameObject.transform.parent);
                    }
                }
            }
        }
    }
}
