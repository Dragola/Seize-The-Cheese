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

    private Vector2 moveDirection = Vector2.zero;

    private Vector2 jumpVelocity = Vector2.zero;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        moveDirection = transform.TransformDirection(moveDirection);
        if (controller.isGrounded)
        {
            moveDirection *= groundSpeed;
            if (Input.GetButton("Jump"))
            {
                jumpVelocity = moveDirection/1.6f;
                jumpVelocity.y = jumpSpeed;
            }
            else
            {
                jumpVelocity = Vector2.zero;
            }
        }
        else
        {
            moveDirection *= midairSpeed;
            jumpVelocity.y -= gravity * Time.deltaTime;
        }
        controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);
    }
}

