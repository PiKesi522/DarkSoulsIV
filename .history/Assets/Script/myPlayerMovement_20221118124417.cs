using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject Player;
    public float baseSpeed = 5f;
    public float gravity = -9.81f;
    public float sprintSpeed = 5f;
    public float walkRecover = 10f;
    float speedBoost = 3f;

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
            speedBoost = 3f;

            walkRecover += Time.deltaTime;
            walkRecover = walkRecover > 10f ? 10f : walkRecover;

            if(walkRecover < 3f){
                speedBoost = 0f;
            }
            // if(Player.GetComponent<CharacterState>().exhausted || Input.GetMouseButton(1)){
            else if(Player.GetComponent<CharacterStateNew>().exhausted || Input.GetMouseButton(1)){
                speedBoost = 3f;
            }
            else if(Input.GetButton("Fire3")){
                speedBoost = sprintSpeed;
            }

            if(Input.GetMouseButtonDown(0)){
                walkRecover = 0f;
            }



            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        }
        
    }
}
