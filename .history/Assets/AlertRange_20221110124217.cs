using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertRange : MonoBehaviour
{
    public static GameObject enemy; 
    public enum enemyState
    {
        idle = 0,
        follow = 1,
        attack = 2,
        die = 99,
    }
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("DogPBR");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        // Debug.Log("Follow");
        enemy.GetComponent<NPCevent>().ChaseStart();
    }

    void OnTriggerExit(Collider other){
        enemy.GetComponent<NPCevent>().ChaseStop    ();
        Debug.Log("Not Follow");
    }
}
