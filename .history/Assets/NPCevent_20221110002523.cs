
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class NPCevent : MonoBehaviour
{
    public Transform target;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    // public GameObject Player;
    private bool _isChase;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        // Player = GameObject.Find("MainCharacter");
    }
 
    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.enabled){
            _navMeshAgent.SetDestination(target.position);
            _navMeshAgent.isStopped = !_isChase;
        }
}