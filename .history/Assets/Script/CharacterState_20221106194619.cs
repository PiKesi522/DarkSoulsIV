using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    private enum motionState
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
    private motionState presentState;
    private bool attackSeq;  // volatile
    public float motionInterval;
    private float motionIntervalMAX;
    
    // Start is called before the first frame update
    void Start()
    {
        this.motionIntervalMAX = 3.0f;
        this.motionInterval = motionIntervalMAX;

        this.attackSeq = false; // volatile

        this.presentState = motionState.idle;
        Debug.Log(presentState);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(motionInterval);
        motionInterval = ((motionInterval - 0.1f) < 0f) ? 0f : (motionInterval - 0.1f);

        if(Input.GetMouseButtonDown(0)){
            // Debug.Log("left click");
            if(motionInterval < 0.1f){
                Debug.Log("left");
                motionInterval = motionIntervalMAX;
                attackSeq = !attackSeq;
                this.presentState = attackSeq ? motionState.useLeftHand_Attack1 : motionState.useLeftHand_Attack2;
            }else{
                Debug.Log("Too fast");
            }
        }
        else if(Input.GetMouseButton(1)){
            Debug.Log("right click");
            this.presentState = motionState.useRightHand_Defend;
        }
        else if(Input.GetKey(KeyCode.W)){
            if(Input.GetKey(KeyCode.LeftShift)){
                Debug.Log("walk");
                this.presentState = motionState.runForward;
            }
            else{
                Debug.Log("run");
                this.presentState = motionState.walkForward;
            }
        }else{
            this.presentState = motionState.idle;
        }

        // if(Input.GetMouseButtonUp(0)){
        //     // Debug.Log("left click up");
        //     this.presentState = motionState.idle;
        // }
        // else if(Input.GetMouseButtonUp(1)){
        //     // Debug.Log("right click up");
        //     this.presentState = motionState.idle;
        // }
        // this.presentState = motionState.idle;
    }
}
