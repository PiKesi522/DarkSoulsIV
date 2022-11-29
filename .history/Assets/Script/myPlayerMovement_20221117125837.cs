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
        if(!Player.GetComponent<CharacterState>().isDead){
            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Input.GetButton("Fire3"))
                speedBoost = sprintSpeed;
            else 
                speedBoost = 1f;

            if(Player.GetComponent<CharacterState>().exhausted)
                speedBoost = 1f;

            if(Input.GetMouseButton(1))
                speedBoost = 1f;


            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        }
        
    }
}