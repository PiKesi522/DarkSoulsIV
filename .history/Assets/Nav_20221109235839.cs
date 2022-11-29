using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavTest : MonoBehaviour {

    public Transform targetpos;
    private NavMeshAgent nav;
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(targetpos.position);
    }

    // Update is called once per frame
    void Update () {

    }
}