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
    
    //UI
    public bool pauseMenuActive = false; //used to prevent other controls + for closing/opening main menu
    private Canvas pauseMenu = null;     //used to reference the main menu's canvas to access 'MainMenu' script and make menu visible/invisible
    private Canvas dialog = null;

    private Vector2 moveDirection = Vector2.zero;

    private Vector2 jumpVelocity = Vector2.zero;

    // KH_ParticleSystem
    private CapsuleCollider playerCollider;
    public ParticleSystem dustTrailPS;
    public ParticleSystem dustKickoffPS;

    void CreateDustTrail()
    {
        if (!dustTrailPS.isPlaying)
        {
            dustTrailPS.Play();
        }
    }
    void StopDustTrail()
    {
        if (dustTrailPS.isPlaying)
        {
            dustTrailPS.Stop();
        }
    }
     void CreateDustKickoff()
    {
        if (!dustKickoffPS.isPlaying)
        {
            dustKickoffPS.gameObject.transform.position = new Vector3(playerCollider.gameObject.transform.position.x, 
                                                    playerCollider.bounds.min.y, 
                                                    playerCollider.gameObject.transform.position.z);
            dustKickoffPS.Play();
        }
    }
    void StopDustKickoff()
    {
        if (dustKickoffPS.isPlaying)
        {
            dustKickoffPS.Stop();
        }
    }

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        //Debug.Log(playerCollider.radius);

        //locate pause menu and make invisible
        pauseMenu = GameObject.Find("Pause Menu").GetComponent<Canvas>();
        pauseMenu.gameObject.SetActive(false);

        //locate and make dialog invisible
        dialog = GameObject.Find("Dialog").GetComponent<Canvas>();
        dialog.gameObject.SetActive(false);
    }
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
            
            //CreateDustTrail();
            if (Input.GetButton("Jump"))
            {
                didJump = true;
                jumpVelocity = moveDirection/1.6f;
                jumpVelocity.y = jumpSpeed;

                CreateDustKickoff();
            }
            else
            {
                didJump = false;
                jumpVelocity = Vector2.zero;
            }
        }
        else
        {
            //StopDustTrail();
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