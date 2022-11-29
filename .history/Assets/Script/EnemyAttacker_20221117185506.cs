using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public float damage;
    public GameObject weapon;
    public GameObject Player;
    public bool makeDamage;
    // Start is called before the first frame update
    void Start()
    {
        damage = 30.0f;
        Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
        weapon = this.gameObject.transform.parent.gameObject;
        // Debug.Log(self);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            Player.GetComponent<CharacterState>().injury(damage, false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
