using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    private List<GameObject> targetList;
    void Start()
    {
        targetList = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other) 
    {
        
    }
}
