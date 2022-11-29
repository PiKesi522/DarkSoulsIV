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
    private bool useEndurance;
    // public float defendTime;
    
    // Start is called before the first frame update
    void Start()
    {
        endurance = GameObject.Find("Endurance");
        bar = GameObject.Find("EnduranceBarBackground");
        this.recoveryEnduranceSpeed = 1f;
        this.useEndurance = false;

        this.motionIntervalMAX = 3.0f;
        this.motionInterval = motionIntervalMAX;

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
                Debug.Log("left");
                motionInterval = motionIntervalMAX;
                attackSeq = !attackSeq;
                if(attackSeq){
                    animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack1));
                    decreaseEndurance(10f);
                    useEndurance = true;
                }else{
                    animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack2));
                    decreaseEndurance(8f);
                    useEndurance = true;
                }
            }else{
                Debug.Log("Too fast");
            }
        }
        else if(Input.GetMouseButton(1)){
            Debug.Log("right click");
            // this.presentState = motionState.useRightHand_Defend;
            animator.SetInteger("MotionState", (int)(motionState.useRightHand_Defend));
        }
        else if(Input.GetKey(KeyCode.W)){
            if(Input.GetKey(KeyCode.LeftShift)){
                Debug.Log("run");
                // runTime = runTime + 0.1f;
                decreaseEndurance(1);
                useEndurance = true;
                // this.presentState = motionState.runForward;
                animator.SetInteger("MotionState", (int)(motionState.runForward));
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
            recoveryEndurance();
        }
        changeEnduranceBarColor();
    }

    void recoveryEndurance(){
        if(endurance.GetComponent<Slider>().value + recoveryEnduranceSpeed > 99f){
            endurance.GetComponent<Slider>().value = 100f;
        }
        else{
            endurance.GetComponent<Slider>().value += recoveryEnduranceSpeed;
        }
    }

    void decreaseEndurance(float cost){
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
            bar.GetComponent<Image>.color = new Color32(92, 255, 0, 255);
        }else if(eValue < 25){
            bar.GetComponent<Image>.color = new Color32(92, 255, 0, 255);
        }
    }
}
