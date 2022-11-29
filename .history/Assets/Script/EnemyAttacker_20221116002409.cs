using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public float damage;
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        damage = 20.0f;
        // Player = GameObject.Find("DogPolyart");
        self = this.gameObject.transform.parent.gameObject;
        Debug.Log(self);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCore"){
            Debug.Log(other.gameObject.name);
        }
    }

}
