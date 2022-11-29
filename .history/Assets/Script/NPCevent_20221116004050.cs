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
        runForward = 21,
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
    private GameObject _self;

    public GameObject canvas;
    public GameObject weapon;
    // public static GameObject HPLeft;
    // public static GameObject HPRight;

    public bool isChase;
    public bool isAttack;
    public bool doAttack;

    public bool dead;
    public float motionGap;
    public float motionGapMax;
    public float HP;

    private bool attackSeq;  // volatile

    void Awake()
    {
        this.dead = false;
        this.motionGap = 0.0f;
        this.motionGapMax = 40.0f;
        this.HP = 100.0f;

        _self = this.gameObject.transform.gameObject;
        // Debug.Log(_self);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        canvas = this.gameObject.transform.Find("EnemyCanvas").gameObject;
        canvas.GetComponent<Canvas>().enabled = false;

        weapon = transform.Find("root/pelvis/Weapon").gameObject;
        weapon.GetComponent<BoxCollider>().enabled = false;
        
        canvas.transform.GetComponentsInChildren<Slider>()[0].value = HP;
        canvas.transform.GetComponentsInChildren<Slider>()[1].value = HP;

        // HPLeft.GetComponent<Slider>().value = 100f;

        // HPRight = GameObject.Find("EnemyHealthBarRight");
        // HPRight.GetComponent<Slider>().value = 100f;
    }

    // Update is called once per frame
    void Update()
    {

        if(dead){
            _navMeshAgent.isStopped = true;
            _animator.SetInteger("MotionState", (int)(motionState.die));
            canvas.GetComponent<Canvas>().enabled = false;
            Destroy(_self, 4f);
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
                    weapon.GetComponent<BoxCollider>().enabled = true;
                    Invoke("disableAttacker", 0.5f);
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
        // HPLeft.GetComponent<Slider>().value = HP;
        // HPRight.GetComponent<Slider>().value = HP;
        
        canvas.transform.GetComponentsInChildren<Slider>()[0].value = HP;
        canvas.transform.GetComponentsInChildren<Slider>()[1].value = HP;
        // healthSlider.value = HP;
        if(HP < 0.1f){
            dead = true;
        }
    }
    
    public void ChaseStart(){
        canvas.GetComponent<Canvas>().enabled = true;
        isChase = true;
    }

    public void ChaseStop(){
        canvas.GetComponent<Canvas>().enabled = false;
        isChase = false;
    }

    public void AttackStart(){
        isAttack = true;
    }

    public void AttackStop(){
        isAttack = false;
    }

    void disableAttacker(){
        weapon.GetComponent<BoxCollider>().enabled = false;
    }
}