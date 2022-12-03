using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
 
public class BossEvent : MonoBehaviour
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
    
    public static GameObject GM;
    public static GameObject AM;
    public static GameObject UI;

    public bool isChase;
    public bool isAttack;
    public bool doAttack;

    public bool bossDead;
    public float motionGap;
    public float motionGapMax;
    public float HP;

    private bool attackSeq;  // volatile

    void Awake()
    {
        this.bossDead = false;
        this.motionGap = 0.0f;
        this.motionGapMax = 30.0f;
        this.HP = 500.0f;

        _self = this.gameObject.transform.gameObject;
        target = GameObject.Find("MainCharacter").transform.Find("RPG-Character").transform;
        // Debug.Log(_self);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        GM = GameObject.Find("GameManager").gameObject;
        AM = GameObject.Find("AudioManager").gameObject;
        UI = GameObject.Find("UI").gameObject;

        weapon.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(bossDead){
            _navMeshAgent.isStopped = true;
            _animator.SetInteger("MotionState", (int)(motionState.die));
            canvas.GetComponent<Canvas>().enabled = false;
            // Destroy(_self, 4f);
        }
        else{

            motionGap = (motionGap - 0.1f < 0) ? 0 : (motionGap - 0.1f);

            if (_navMeshAgent.enabled){
                _navMeshAgent.SetDestination(target.position);
                _navMeshAgent.isStopped = !isChase;
            }


            if(isAttack){
                if(motionGap < 0.2f){
                    motionGap = Random.Range(10f, motionGapMax);
                    attackSeq = !attackSeq;
                    weapon.GetComponent<BoxCollider>().enabled = true;
                    // Invoke("ShutDownTrigger", 0.5f);
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
        canvas.transform.Find("BossHealthBar").gameObject.GetComponent<Slider>().value = HP;
        if(HP < 0.1f){
            bossDead = true;
            PlayerGetMoney(5000);
            // GameManager需要记录Boss已被击败不刷新
            // AudioManager播放Boss死亡音乐
            AM.GetComponent<AudioManager>().AudioPlayBossDefeat();
            // UI播放Boss死亡动画
        }
    }

    private void PlayerGetMoney(int m){
        target.gameObject.GetComponent<LevelAndMoney>().addMoney(m);
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

    private void ShutDownTrigger(){
        weapon.GetComponent<BoxCollider>().enabled = false;
    }

}