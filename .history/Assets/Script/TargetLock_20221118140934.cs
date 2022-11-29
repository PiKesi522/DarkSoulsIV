using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    private List<GameObject> targetList;
    public GameObject closestTarget;
    public GameObject Player;
    
    void Start()
    {
        Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
        targetList = new List<GameObject>();
    }

    private void Update() {
        if(targetList.Count > 0){
            float distance = 100.0f;
            foreach (GameObject item in targetList)
            {
                float newDistance = (Player.transform.position - item.transform.position).magnitude;
                distance = newDistance < distance ? newDistance : distance;
                Debug.Log(distance);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "EnemyCore"){
            targetList.Add(other.gameObject);
            // Debug.Log(targetList);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.name == "EnemyCore"){
            targetList.Remove(other.gameObject);
        }
    }
}
