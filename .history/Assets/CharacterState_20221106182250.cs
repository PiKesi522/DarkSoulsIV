using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    private enum State
    {
        idle = 0,
        walkForward = 11,
        walkBackward = 12,
        walkLeft = 13,
        walkRight = 14,
        runForward = 21,
        attack = 31,
        defend = 41,
        getHit = 51,
        die = 99,
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
