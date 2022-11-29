using System.Diagnostics;
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
        if(Input.GetMouseButtonDown(1)){
            Debug.Log("left click");
        }
        else if(Input.GetMouseButtonDown(0)){
            Debug.Log("right click");
        }
    }
}
