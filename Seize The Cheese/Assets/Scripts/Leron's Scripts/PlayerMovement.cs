﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0F;
    public float jumpSpeed = 1.0F;
    public float gravity = 1.0F;
    public float midairSpeed = 1.0F;
    public float groundSpeed = 1.0F;

    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public bool didJump = false;
    public bool cheeseHittingWall = false;
    
    //UI
    public bool pauseMenuActive = false; //used to prevent other controls + for closing/opening main menu
    private Canvas pauseMenu = null;     //used to reference the main menu's canvas to access 'MainMenu' script and make menu visible/invisible
    private Canvas dialog = null;

    private Vector2 moveDirection = Vector2.zero;

    public Vector2 jumpVelocity = Vector2.zero;

    CharacterController controller = null;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        //locate pause menu and make invisible
        pauseMenu = GameObject.Find("Pause Menu").GetComponent<Canvas>();
        pauseMenu.gameObject.SetActive(false);

        //locate dialog
        dialog = GameObject.Find("Dialog").GetComponent<Canvas>();
    }
    private void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        moveDirection = transform.TransformDirection(moveDirection);

        //prevent player from moving if cheese is hitting wall and jumped
        if (didJump && cheeseHittingWall)
        {
            moveDirection *= 0;
            jumpVelocity.x *= 0;
            jumpVelocity.y -= gravity * Time.deltaTime;
            
            controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            moveDirection *= groundSpeed;
            if (Input.GetButton("Jump"))
            {
                Debug.Log("controller.isGrounded + input 'Jump'");
                didJump = true;
                jumpVelocity = moveDirection / 1.6f;
                jumpVelocity.y = jumpSpeed;

            }
            else
            {
                Debug.Log("controller.isGrounded - input 'Jump'");
                //didJump = true;
                didJump = false;
                jumpVelocity = Vector2.zero;
            }
        }
        else
        {
            Debug.Log("controller.isGrounded = false");
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
        //main menu key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if main menu isn't currently active
            if (pauseMenuActive == false) {
                pauseMenuActive = true;
                //makes main menu visible
                pauseMenu.gameObject.SetActive(true);

                //pauses game
                pauseMenu.GetComponent<PauseMenu>().PauseGame();
            }
            //if main menu is currently active
            else
            {
                pauseMenuActive = false;
                //makes main menu invisible
                pauseMenu.gameObject.SetActive(false);

                //resumes game
                pauseMenu.GetComponent<PauseMenu>().ResumeGame();
            }
        }
    }
    public void ResumePlayer() //called if player uses resume in main menu (closes menu for player)
    {
        pauseMenuActive = false;
        //makes main menu invisible
        pauseMenu.gameObject.SetActive(false);
    }
}