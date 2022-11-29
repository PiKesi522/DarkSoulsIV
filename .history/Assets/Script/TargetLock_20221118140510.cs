using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    private List<GameObject> targetList;
    public GameObject closestTarget;
    
    void Start()
    {
        targetList = new List<GameObject>();
    }

    private void Update() {
        if(List.Count > 0){
            float distance = 100.0f;
            foreach (GameObject item in targetList)
            {
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "EnemyCore"){
            targetList.Add(other.gameObject);
            Debug.Log(targetList);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.name == "EnemyCore"){
            targetList.Remove(other.gameObject);
        }
    }
}
