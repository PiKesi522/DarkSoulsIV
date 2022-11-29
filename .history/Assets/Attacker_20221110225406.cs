using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float damage;
    public string enemyList;
    // Start is called before the first frame update
    void Start()
    {
        damage = 30.0f;
        enemyList = ["DogPolyart", "Boss"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "DogPolyart"){

        }
    }
}
