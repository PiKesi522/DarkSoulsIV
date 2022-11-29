using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CharacterState : MonoBehaviour, IPointerClickHandler
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

        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));
        middleClick.AddListener(new UnityAction(ButtonMiddleClick));

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
    }

    private void ButtonLeftClick()
    {
        Debug.LogError("Left Click");
    }
    private void ButtonRightClick()
    {
        Debug.LogError("Right Click");
    }
    private void ButtonMiddleClick()
    {
        Debug.LogError("Middle Click");
    }

}
