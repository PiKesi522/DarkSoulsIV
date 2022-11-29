using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public float damage;
    public GameObject self;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        damage = 20.0f;
        Player = GameObject.Find("DogPolyart");
        self = this.gameObject.transform.parent.gameObject;
        // Debug.Log(self);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            Player.GetComponent<CharacterState>().injury(damage);
            Debug.Log("hit");
        }
    }

}
