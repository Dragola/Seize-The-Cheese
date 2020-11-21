using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector2 moveDirection = Vector2.zero;
    private float turner;
    private float looker;
    public float sensitivity;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate1")
        {
            Debug.Log("Hit1");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 1;
        }
        if (other.tag == "Gate2")
        {
            Debug.Log("Hit2");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 2;
        }
        if (other.tag == "Gate3")
        {
            Debug.Log("Hit3");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 3;
        }
        if (other.tag == "Gate4")
        {
            Debug.Log("Hit4");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 4;
        }
    
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}