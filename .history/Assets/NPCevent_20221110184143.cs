using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class NPCevent : MonoBehaviour
{
    public enum motionState
    {
        idle = 0,
        walkForward = 11,
        useLeftHand_Attack1 = 31,
        useLeftHand_Attack2 = 32,
        getHit = 51,
        die = 99,
    };
    
    public Transform target;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    public bool isChase;
    void Start()
    {
        // target = GameObject.Find("MainCharacter").GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        // Player = GameObject.Find("MainCharacter");
        // Invoke("ChaseStart", 2);
    }

    public void ChaseStart(){
        isChase = true;
    }

    public void ChaseStop(){
        isChase = false;
    }

 
    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.enabled){
            _navMeshAgent.SetDestination(target.position);
            _navMeshAgent.isStopped = !isChase;
        }
    }
}