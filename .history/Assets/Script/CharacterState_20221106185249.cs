using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    private enum motionState
    {
        idle = 0,
        walkForward = 11,
        walkBackward = 12,
        walkLeft = 13,
        walkRight = 14,
        runForward = 21,
        useLeftHand = 31,
        useRightHand = 41,
        getHit = 51,
        die = 99,
    }
    private motionState presentState;
    
    // Start is called before the first frame update
    void Start()
    {
        this.presentState = motionState.idle;
        Debug.Log(presentState);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("left click");
            this.presentState = motionState.useLeftHand;
        }
        else if(Input.GetMouseButtonDown(1)){
            Debug.Log("right click");
            this.presentState = motionState.useRightHand;
        }

        if(Input.GetMouseButtonUp(0)){
            Debug.Log("left click up");
            this.presentState = motionState.idle;
        }
        else if(Input.GetMouseButtonUp(1)){
            Debug.Log("right click up");
            this.presentState = motionState.idle;
        }
        
        Debug.Log(presentState);
    }
}
