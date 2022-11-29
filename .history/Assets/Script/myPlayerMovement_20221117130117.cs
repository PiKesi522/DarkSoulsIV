using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject Player;
    public float baseSpeed = 12f;
    public float gravity = -9.81f;
    public float sprintSpeed = 5f;
    float speedBoost = 1f;

    Vector3 velocity;
    void Start()
    {
        Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(!Player.GetComponent<CharacterState>().isDead){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            speedBoost = 1f;

            if(Player.GetComponent<CharacterState>().exhausted || Input.GetMouseButton(1)){
                speedBoost = 1f;
            }
            else if(Input.GetKeyDown(KeyCode.LeftShift)){
                speedBoost = sprintSpeed;
            }


            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        }
        
    }
}
