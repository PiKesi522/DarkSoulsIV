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
        useRightHand = 41,
        getHit = 51,
        die = 99,
    }
    private motionState presentState;
    private bool attackSeq;  // volatile
    
    // Start is called before the first frame update
    void Start()
    {
        this.attackSeq = false; // volatile
        this.presentState = motionState.idle;
        Debug.Log(presentState);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            // Debug.Log("left click");
            attackSeq = !attackSeq;
            this.presentState = attackSeq ? motionState.useLeftHand_Attack1 : motionState.useLeftHand_Attack2;
        }
        else if(Input.GetMouseButtonDown(1)){
            // Debug.Log("right click");
            this.presentState = motionState.useRightHand;
        }
        else if(Input.GetKeyDown(KeyCode.W)){
            // Debug.Log("w");
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                this.presentState = motionState.runForward;
            }
            else{
                this.presentState = motionState.walkForward;
            }
        }

        // if(Input.GetMouseButtonUp(0)){
        //     // Debug.Log("left click up");
        //     this.presentState = motionState.idle;
        // }
        // else if(Input.GetMouseButtonUp(1)){
        //     // Debug.Log("right click up");
        //     this.presentState = motionState.idle;
        // }
        this.presentState = motionState.idle;
        Debug.Log(presentState);
    }
}
