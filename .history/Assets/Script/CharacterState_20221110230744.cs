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
    private bool attackSeq;  // volatile

    private float motionInterval;
    public float motionIntervalMAX;

    public static GameObject endurance;
    public static GameObject enduranceBackground;
    public static GameObject HP;

    public float recoveryEnduranceSpeed;
    private float dt_forRecovery;
    private bool useEndurance;
    public bool exhausted;
    public float attackEnduranceCost;
    public float runEnduranceCost;
    public bool readyToAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        // characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();

        endurance = GameObject.Find("Endurance");
        enduranceBackground = GameObject.Find("EnduranceBarBackground");
        endurance.GetComponent<Slider>().value = 100f;

        HP = GameObject.Find("HP");
        HP.GetComponent<Slider>().value = 400f;


        this.recoveryEnduranceSpeed = 0.03f;
        this.dt_forRecovery = 0f;
        this.useEndurance = false;

        this.attackEnduranceCost = 20f;
        this.runEnduranceCost = 0.1f;

        this.motionIntervalMAX = 10.0f;
        this.motionInterval = motionIntervalMAX;

        this.attackSeq = false; // volatile
        this.exhausted = false;
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
                        animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack1));
                    }
                    else{
                        animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack2));
                    }               
                    decreaseEndurance(attackEnduranceCost);
                    useEndurance = true;
                }
                readyToAttack = false;
            }else{
                Debug.Log("Too fast");
            }
        }
        else if(Input.GetMouseButton(1)){
            // Debug.Log("right click");
            // this.presentState = motionState.useRightHand_Defend;
            animator.SetInteger("MotionState", (int)(motionState.useRightHand_Defend));
        }
        else if(Input.GetKey(KeyCode.W)){
            if(Input.GetKey(KeyCode.LeftShift) && !exhausted){
                // Debug.Log("run");
                if(exhausted){
                    animator.SetInteger("MotionState", (int)(motionState.idle));
                }else{
                    animator.SetInteger("MotionState", (int)(motionState.runForward));
                    decreaseEndurance(runEnduranceCost);
                    useEndurance = true;
                }
            }
            else{
                // Debug.Log("walk");
                // this.presentState = motionState.walkForward;
                animator.SetInteger("MotionState", (int)(motionState.walkForward));
            }
        }else{
            animator.SetInteger("MotionState", (int)(motionState.idle));
            // this.presentState = motionState.idle;
        }

        if(Input.GetKeyDown(KeyCode.J)){
            decreaseHP(30f);
        }else if(Input.GetKeyDown(KeyCode.K)){
            recoveryHP(10f);
        }

        // if(Input.GetKeyUp(KeyCode.LeftShift)){
        //     runTime = 0.0f;
        // }

        // if(Input.GetMouseButtonUp())

        // if(Input.GetMouseButtonUp(0)){
        //     // Debug.Log("left click up");
        //     this.presentState = motionState.idle;
        // }
        // else if(Input.GetMouseButtonUp(1)){
        //     // Debug.Log("right click up");
        //     this.presentState = motionState.idle;
        // }
        // this.presentState = motionState.idle;

        if(!useEndurance){
            dt_forRecovery += 0.1f;
            float recoveryRate = dt_forRecovery * recoveryEnduranceSpeed; 
            if(recoveryRate > 1.2f){
                recoveryRate = 1.2f;
            }
            recoveryEndurance(recoveryRate);
        }
        changeEnduranceBarColor();
    }

    void recoveryEndurance(float cost){
        if(endurance.GetComponent<Slider>().value + cost > 99f){
            endurance.GetComponent<Slider>().value = 100f;
            this.exhausted = false;
        }
        else{
            endurance.GetComponent<Slider>().value += cost;
        }
    }

    void decreaseEndurance(float cost){
        this.dt_forRecovery = 0f;
        if(endurance.GetComponent<Slider>().value - cost < 1f){
            endurance.GetComponent<Slider>().value = 0f;
            this.exhausted = true;
        }
        else{
            endurance.GetComponent<Slider>().value -= cost;
        }
    }

    void changeEnduranceBarColor(){
        float eValue = endurance.GetComponent<Slider>().value;
        if(eValue > 75){
            enduranceBackground.GetComponent<Image>().color = new Color32(92, 255, 0, 255);
        }
        else if(eValue < 25){
            enduranceBackground.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        else{
            enduranceBackground.GetComponent<Image>().color = new Color32(249, 255, 0, 255);
        }
    }

    bool hasEnoughEndurance(float cost){
        return endurance.GetComponent<Slider>().value > cost;
    }

    void decreaseHP(float cost){
        if(HP.GetComponent<Slider>().value - cost < 0){
            Debug.Log("Dead");
            HP.GetComponent<Slider>().value = 0;
            animator.SetInteger("MotionState", (int)(motionState.die));
        }
        else{
            HP.GetComponent<Slider>().value -= cost;
        }
    }

    void recoveryHP(float cost){
        if(HP.GetComponent<Slider>().value + cost > 400){
            HP.GetComponent<Slider>().value = 400;
        }
        else{
            HP.GetComponent<Slider>().value += cost;
        }
    }
}
