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
        if(other.gameObject.name == "PlayerCore" && makeDamage){
            Player.GetComponent<CharacterState>().injury(damage, false);
            Debug.Log("hit");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore" && makeDamage){
            makeDamage = false;
            weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
