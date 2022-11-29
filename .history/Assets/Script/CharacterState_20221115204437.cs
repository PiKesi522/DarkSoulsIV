using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour
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

    // public motionState presentState;
    private Animator animator;
    private motionState presentState;
    private string currentState;
    // Animation State
    const string PLAYER_IDLE    = "Idle_Battle";
    const string PLAYER_WALK    = "WalkForwardBattle";
    const string PLAYER_RUN     = "RunForwardBattle";
    const string PLAYER_ATTACK1 = "Attack01";
    const string PLAYER_ATTACK2 = "Attack02";
    const string PLAYER_DEFEND  = "Defend";
    const string PLAYER_DIE     = "Die";


    private bool attackSeq;  // volatile

    private float motionInterval;
    public float motionIntervalMAX;


    public static GameObject healthBar;
    public static GameObject manaBar;
    // public static GameObject endurance;
    // public static GameObject enduranceBackground;
    // public static GameObject HP;

    public float recoveryEnduranceSpeed;
    private float dt_forRecovery;
    private bool useEndurance;
    public bool exhausted;
    public float attackEnduranceCost;
    public float runEnduranceCost;
    public bool readyToAttack;

    public float playerHP;
    public float playerEndurance;
    public float playerHPMax;
    public float playerEnduranceMax;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        // endurance = GameObject.Find("Endurance");
        // enduranceBackground = GameObject.Find("EnduranceBarBackground");
        // endurance.GetComponent<Slider>().value = 100f;

        // HP = GameObject.Find("HP");
        // HP.GetComponent<Slider>().value = 400f;

        healthBar = GameObject.Find("Healthbar");
        healthBar.GetComponent<Image>().fillAmount = 1.0f;
        manaBar = GameObject.Find("Manabar");
        manaBar.GetComponent<Image>().fillAmount = 1.0f;

        this.playerEnduranceMax = 150f;
        this.playerEndurance = playerEnduranceMax;
        this.playerHPMax = 200f;
        this.playerHP = playerHPMax;

        this.recoveryEnduranceSpeed = 0.03f;
        this.dt_forRecovery = 0f;
        this.useEndurance = false;

        this.attackEnduranceCost = 20f;
        this.runEnduranceCost = 0.1f;

        this.motionIntervalMAX = 10.0f;
        this.motionInterval = motionIntervalMAX;

        this.attackSeq = false; // volatile
        this.exhausted = false;
        this.readyToAttack = true;
        // this.presentState = motionState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        useEndurance = false;  
        motionInterval = ((motionInterval - 0.1f) < 0f) ? 0f : (motionInterval - 0.1f);
        if(motionInterval < 0.1f){
            readyToAttack = true;
        }

        if(Input.GetMouseButtonDown(0)){
            // Debug.Log("left click");
            if(readyToAttack){
                // Debug.Log("left");
                motionInterval = motionIntervalMAX;
                attackSeq = !attackSeq;
                if(hasEnoughEndurance(attackEnduranceCost)){
                    if(attackSeq){
                        this.presentState = motionState.useLeftHand_Attack1;
                        // animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack1));
                        // ChangeAnimationState(PLAYER_ATTACKLEFT);
                    }
                    else{
                        this.presentState = motionState.useLeftHand_Attack2;
                        // ChangeAnimationState(PLAYER_ATTACKRIGHT);
                        // animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack2));
                    }               
                    decreaseEndurance(attackEnduranceCost);
                    useEndurance = true;
                    readyToAttack = false;
                }
            }else{
                Debug.Log("Too fast");
            }
        }
        else if(Input.GetMouseButton(1)){
            ChangeAnimationState(PLAYER_DEFEND,0,0f);
            // animator.SetInteger("MotionState", (int)(motionState.useRightHand_Defend));
        }
        else if(Input.GetKey(KeyCode.W)){
            if(Input.GetKey(KeyCode.LeftShift) && !exhausted){
                // Debug.Log("run");
                if(exhausted){
                    this.presentState = motionState.idle;
                    // ChangeAnimationState(PLAYER_IDLE);
                    // animator.SetInteger("MotionState", (int)(motionState.idle));
                }else{
                    this.presentState = motionState.run;
                    // ChangeAnimationState(PLAYER_RUN,0,0f);
                    // animator.SetInteger("MotionState", (int)(motionState.runForward));
                    decreaseEndurance(runEnduranceCost);
                    useEndurance = true;
                }
            }
            else{
                this.presentState = motionState.walkForward
                // ChangeAnimationState(PLAYER_WALK,0,0f);
                // animator.SetInteger("MotionState", (int)(motionState.walkForward));
            }
        }else{
            // ChangeAnimationState(PLAYER_IDLE);
            // animator.SetInteger("MotionState", (int)(motionState.idle));
        }

        if(Input.GetKeyDown(KeyCode.J)){
            decreaseHP(30f);
        }else if(Input.GetKeyDown(KeyCode.K)){
            recoveryHP(10f);
        }

        if(!useEndurance){
            dt_forRecovery += 0.1f;
            float recoveryRate = dt_forRecovery * recoveryEnduranceSpeed; 
            if(recoveryRate > 1.2f){
                recoveryRate = 1.2f;
            }
            recoveryEndurance(recoveryRate);
        }
        // changeEnduranceBarColor();

        AnimationStateConfirm();
    }

    void recoveryEndurance(float cost){
        if(playerEndurance + cost > playerEnduranceMax){
            playerEndurance = playerEnduranceMax;
            this.exhausted = false;
        }
        else{
            playerEndurance += cost;
        }
        manaBar.GetComponent<Image>().fillAmount = playerEndurance / playerEnduranceMax;
    }

    void decreaseEndurance(float cost){
        this.dt_forRecovery = 0f;
        if(playerEndurance - cost < 0.1f){
            playerEndurance = 0f;
            this.exhausted = true;
        }
        else{
            playerEndurance -= cost;
        }
        manaBar.GetComponent<Image>().fillAmount = playerEndurance / playerEnduranceMax;
    }

    // void changeEnduranceBarColor(){
    //     float eValue = endurance.GetComponent<Slider>().value;
    //     if(eValue > 75){
    //         enduranceBackground.GetComponent<Image>().color = new Color32(92, 255, 0, 255);
    //     }
    //     else if(eValue < 25){
    //         enduranceBackground.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    //     }
    //     else{
    //         enduranceBackground.GetComponent<Image>().color = new Color32(249, 255, 0, 255);
    //     }
    // }

    bool hasEnoughEndurance(float cost){
        return playerEndurance > cost;
    }

    void decreaseHP(float cost){
        if(playerHP - cost < 0.1f){
            // Debug.Log("Dead");
            playerHP = 0;
            this.presentState = motionState.dead;
            // ChangeAnimationState(PLAYER_DIE);
            // animator.SetInteger("MotionState", (int)(motionState.die));
        }
        else{
            playerHP -= cost;
        }
        
        healthBar.GetComponent<Image>().fillAmount = playerHP / playerHPMax;
    }

    void recoveryHP(float cost){
        if(playerHP + cost > playerEnduranceMax - 0.1f){
            playerHP = playerEnduranceMax;
        }
        else{
            playerHP += cost;
        }
        healthBar.GetComponent<Image>().fillAmount = playerHP / playerHPMax;
    }

    void AnimationStateConfirm(){

    }

    void ChangeAnimationState(string newState){
        if(currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    void ChangeAnimationState(string newState, int arg1, float arg2){
        if(currentState == newState) return;
        animator.Play(newState, arg1, arg2);
        currentState = newState;
    }
}
