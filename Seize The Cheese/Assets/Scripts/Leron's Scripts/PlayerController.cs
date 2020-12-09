using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0F;
    public float jumpSpeed = 1.0F;
    public float gravity = 1.0F;
    public float midairSpeed = 1.0F;
    public float groundSpeed = 1.0F;

    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public bool didJump = false;

    private Vector2 moveDirection = Vector2.zero;

    private Vector2 jumpVelocity = Vector2.zero;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // iterates through all CubeCheese and ChildCube and drops them whe K is pressed
        {
            canMoveLeft = true;
            canMoveRight = true;
        }
        
        CharacterController controller = GetComponent<CharacterController>();
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        moveDirection = transform.TransformDirection(moveDirection);

        if (controller.isGrounded)
        {
            moveDirection *= groundSpeed;
            if (Input.GetButton("Jump"))
            {
                didJump = true;
                jumpVelocity = moveDirection/1.6f;
                jumpVelocity.y = jumpSpeed;

            }
            else
            {
                didJump = false;
                jumpVelocity = Vector2.zero;

            }
        }
        else
        {
            moveDirection *= midairSpeed;
            jumpVelocity.y -= gravity * Time.deltaTime;

        }



        if (canMoveLeft && canMoveRight || Input.GetAxis("Horizontal") == 0)
                controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);

        else if (canMoveLeft && Input.GetAxis("Horizontal") < 0)
                   controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);

        else if (canMoveRight && Input.GetAxis("Horizontal") > 0)
                   controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);

        else
        {
            if (Input.GetAxis("Horizontal") < 0 && canMoveLeft)
                controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);


            if (Input.GetAxis("Horizontal") > 0 && canMoveRight)
                controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);
        }
    }
}

