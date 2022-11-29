using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
 
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

    public static GameObject HPLeft;
    public static GameObject HPRight;

    public bool isChase;
    public bool isAttack;

    private float motionGap;
    public float motionGapMax;

    private bool attackSeq;  // volatile
    public float HP;
    public bool dead;

    void Start()
    {
        HP = 100.0f;
        dead = false;
        motionGap = 0.0f;
        motionGapMax = 40.0f;
        // target = GameObject.Find("MainCharacter").GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        // Player = GameObject.Find("MainCharacter");
        // Invoke("ChaseStart", 2);


        // healthSlider = this.gameObject.GetComponentInChildren<Slider>();
        HPLeft = GameObject.Find("EnemyHealthBarLeft");
        HPLeft.GetComponent<Slider>().value = 100f;

        HPRight = GameObject.Find("EnemyHealthBarRight");
        HPRight.GetComponent<Slider>().value = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead){
            _navMeshAgent.isStopped = true;
            _animator.SetInteger("MotionState", (int)(motionState.die));
        }
        else{

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
    }
    
    public void injury(float cost){
        HP = (HP - cost < 0.1f) ? 0 : (HP - cost);
        HPLeft.GetComponent<Slider>().value = HP;
        HPRight.GetComponent<Slider>().value = HP;
        // healthSlider.value = HP;
        if(HP < 0.1f){
            dead = true;
        }
    }
    
    public void ChaseStart(){
        this.gameObject.GetComponent<EnemyCanvas>().enabled = true;
        Debug.Log(this.gameObject);
        isChase = true;
    }

    public void ChaseStop(){
        // this.gameObject.GetComponent<Canvas>().enabled = false;
        isChase = false;
    }

    public void AttackStart(){
        isAttack = true;
    }

    public void AttackStop(){
        isAttack = false;
    }
}