using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    public float damage;
    public GameObject weapon;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        damage = 30.0f;
        Player = GameObject.Find("MainCharacter").transform.Find("RPG-Character").gameObject;
        weapon = this.gameObject;
        // Debug.Log(weapon);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            // Player.GetComponent<CharacterState>().injury(damage, false);
            Player.GetComponent<CharacterStateBeta>().injury(damage, false);
            Debug.Log("Do Damage");
            // Player.GetComponent<CharacterStateNew>().injury(damage, false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
