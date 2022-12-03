using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float damage;
    public string[] enemyList;
    // public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new string[2] {"EnemyCore", "Boss"};
        // Player = GameObject.Find("DogPolyart");
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider other){
        for(int i = 0; i < enemyList.Length; i++){
            if(other.gameObject.name == enemyList[i]){
                Debug.Log(enemyList[i]);
                if(other.transform.parent.parent.gameObject.name == "Boss"){
                    if(Input.GetKey(KeyCode.LeftShift)){
                        other.gameObject.transform.parent.GetComponent<BossEvent>().injury(damage * 1.5f);
                    }
                    else{
                        other.gameObject.transform.parent.GetComponent<BossEvent>().injury(damage);
                    }
                }
                else{
                    if(Input.GetKey(KeyCode.LeftShift)){
                        other.gameObject.transform.parent.GetComponent<EnemyEvent>().injury(damage * 1.5f);
                    }
                    else{
                        other.gameObject.transform.parent.GetComponent<EnemyEvent>().injury(damage);
                    }
                }
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

}
