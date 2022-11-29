using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool attack;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackStart(){
        attack = true;
    }

    public void AttackEnd(){
        attack = false;
    }

    void OnTriggerStay(Collider other){
        if(attack){
            attack = false;
            Debug.Log("make damage");
        }
    }
}
