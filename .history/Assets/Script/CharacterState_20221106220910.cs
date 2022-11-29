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
    }
    // public motionState presentState;
    private Animator animator;
    private bool attackSeq;  // volatile

    private float motionInterval;
    public float motionIntervalMAX;

    public static GameObject endurance;
    public static GameObject bar;
    public float recoveryEnduranceSpeed;
    private float dt_forRecovery;
    private bool useEndurance;
    // public float defendTime;

    public float attackEnduranceCost;
    public float runEnduranceCost;
    
    // Start is called before the first frame update
    void Start()
    {
        endurance = GameObject.Find("Endurance");
        endurance.GetComponent<Slider>().value = 100f;
        bar = GameObject.Find("EnduranceBarBackground");

        this.recoveryEnduranceSpeed = 0.5f;
        this.dt_forRecovery = 0f;
        this.useEndurance = false;

        this.motionIntervalMAX = 1.0f;
        this.motionInterval = motionIntervalMAX;

        this.attackEnduranceCost = 20f;
        this.runEnduranceCost = 1f;

        this.attackSeq = false; // volatile
        // this.presentState = motionState.idle;

        // characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();

        // Debug.Log(presentState);
    }

    // Update is called once per frame
    void Update()
    {
        useEndurance = false;

        motionInterval = ((motionInterval - 0.1f) < 0f) ? 0f : (motionInterval - 0.1f);

        if(Input.GetMouseButtonDown(0)){
            // Debug.Log("left click");
            if(motionInterval < 0.1f){
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
            if(Input.GetKey(KeyCode.LeftShift)){
                // Debug.Log("run");
                animator.SetInteger("MotionState", (int)(motionState.runForward));
                decreaseEndurance(runEnduranceCost);
                useEndurance = true;
            }
            else{
                Debug.Log("walk");
                // this.presentState = motionState.walkForward;
                animator.SetInteger("MotionState", (int)(motionState.walkForward));
            }
        }else{
            animator.SetInteger("MotionState", (int)(motionState.idle));
            // this.presentState = motionState.idle;
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
        }
        else{
            endurance.GetComponent<Slider>().value += cost;
        }
    }

    void decreaseEndurance(float cost){
        this.dt_forRecovery = 0f;
        if(endurance.GetComponent<Slider>().value - cost < 1f){
            endurance.GetComponent<Slider>().value = 0f;
        }
        else{
            endurance.GetComponent<Slider>().value -= cost;
        }
    }

    void changeEnduranceBarColor(){
        float eValue = endurance.GetComponent<Slider>().value;
        if(eValue > 75){
            bar.GetComponent<Image>().color = new Color32(92, 255, 0, 255);
        }else if(eValue < 25){
            bar.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }else{
            bar.GetComponent<Image>().color = new Color32(249, 255, 0, 255);
        }
    }

    bool hasEnoughEndurance(float cost){
        return endurance.GetComponent<Slider>().value > cost;
    }
}
