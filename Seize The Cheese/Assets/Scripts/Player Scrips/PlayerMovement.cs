using System.Collections;
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
    public bool cheeseRayHit = false;
    public bool moveDirectionIsRight = true;

    //UI
    public bool pauseMenuActive = false; //used to prevent other controls + for closing/opening main menu
    private Canvas pauseMenu = null;     //used to reference the main menu's canvas to access 'MainMenu' script and make menu visible/invisible
    private Canvas dialog = null;

    public Vector2 moveDirection = Vector2.zero;

    public Vector2 jumpVelocity = Vector2.zero;

    public CharacterController controller = null;

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

        //moving right
        if(moveDirection.x > 0)
        {
            moveDirectionIsRight = true;
            GetComponent<PlayerMechanics>().UpdateCheeseDirection(moveDirectionIsRight);
        }
        //moving left
        else if(moveDirection.x < 0)
        {
            moveDirectionIsRight = false;
            GetComponent<PlayerMechanics>().UpdateCheeseDirection(moveDirectionIsRight);
        }

        //prevent player from moving if cheese is hitting wall and jumped
        if (controller.isGrounded)
        {
            moveDirection *= groundSpeed;

            if (Input.GetButton("Jump"))
            {
                //Debug.Log("controller.isGrounded + input 'Jump'");
                didJump = true;
                jumpVelocity = moveDirection / 1.6f;
                jumpVelocity.y = jumpSpeed;

            }
            else
            {
                //Debug.Log("controller.isGrounded - input 'Jump'");
                didJump = false;
                jumpVelocity = Vector2.zero;
            }
        }
        else
        {
            //Debug.Log("controller.isGrounded = false");
            moveDirection *= midairSpeed;
            jumpVelocity.y -= gravity * Time.deltaTime;

        }

        //if jumped and cheese's raycast and player is still moving then reduce x direction
        if (didJump && (cheeseRayHit || cheeseHittingWall) && (jumpVelocity.x != 0 || moveDirection.x != 0))
        {
            moveDirection.x = 0;
            jumpVelocity.x = 0;
            controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);
            //push player back if the cheese is in the wall
            if (cheeseHittingWall)
            {
                PushPlayer();
            }
        }
        //move player normally
        else
        {
            if (cheeseHittingWall)
            {
                //push player back if the cheese is in the wall
                if (cheeseHittingWall)
                {
                    PushPlayer();
                }
            }
            else
            {
                //if can move it set direction
                if ((canMoveRight && moveDirection.x > 0) || (canMoveLeft && moveDirection.x < 0))
                {
                    controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);
                }
                else
                {
                    jumpVelocity.x = 0;
                    controller.Move(jumpVelocity * Time.deltaTime);
                }
            }
        }
        //main menu key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if main menu isn't currently active
            if (pauseMenuActive == false)
            {
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
    public void PushPlayer()
    {
        jumpVelocity.x = 0;
        //left
        if (canMoveLeft == false)
        {
            moveDirection.x = 0.5f;
            controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);
        }
        //right
        else
        {
            moveDirection.x = -0.5f;
            controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);
        }
    }
    public void FreezePlayer()
    {
        canMoveLeft = false;
        canMoveRight = false;
    }
}