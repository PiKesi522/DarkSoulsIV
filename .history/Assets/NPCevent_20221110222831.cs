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
        useRightHand_Defend = 41,
        getHit = 51,
        die = 99,
    };

    public Transform target;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    public bool isChase;
    public bool isAttack;

    private float motionGap;
    public float motionGapMax;

    private bool attackSeq;  // volatile

    void Start()
    {
        motionGap = 0.0f;
        motionGapMax = 40.0f;
        // target = GameObject.Find("MainCharacter").GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        // Player = GameObject.Find("MainCharacter");
        // Invoke("ChaseStart", 2);
    }

    // Update is called once per frame
    void Update()
    {
        motionGap = (motionGap - 0.1f < 0) ? 0 : (motionGap - 0.1f);

        if (_navMeshAgent.enabled){
            _navMeshAgent.SetDestination(target.position);
            _navMeshAgent.isStopped = !isChase;
        }


        if(isAttack){
            if(motionGap < 0.1f){
                motionGap = motionGapMax;
                attackSeq = !attackSeq;
                if(attackSeq){
                    _animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack1));
                }
                else{
                    _animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack2));
                }            
            }else{
                _animator.SetInteger("MotionState", (int)(motionState.idle));
            }
        }    
        else if(isChase){
            _animator.SetInteger("MotionState", (int)(motionState.walkForward));
        }
        else{
            _animator.SetInteger("MotionState", (int)(motionState.idle));
        }   
    }
    

    
    public void ChaseStart(){
        isChase = true;
    }

    public void ChaseStop(){
        isChase = false;
    }

    public void AttackStart(){
        isAttack = true;
    }

    public void AttackStop(){
        isAttack = false;
    }
}