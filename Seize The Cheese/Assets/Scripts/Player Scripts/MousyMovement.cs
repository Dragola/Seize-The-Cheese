using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousyMovement : MonoBehaviour
{
    //player
    public float movementSpeed = 0f;
    public float jumpSpeed = 0;
    public float gravity = 1.0F;
    public bool didJump = false;
    public bool inAir = false;

    //cheeseblock indicators
    public bool cheeseHittingWall = false;
    public bool cheeseRayHit = false;

    //rigidbody
    private Rigidbody playerRigidbody = null;
    private PlayerMechanics playerMechanicsScript = null;

    //UI
    public bool pauseMenuActive = false; //used to prevent other controls + for closing/opening main menu
    private Canvas pauseMenu = null;     //used to reference the main menu's canvas to access 'MainMenu' script and make menu visible/invisible
    private Canvas dialog = null;

    //raycast
    RaycastHit hit;
    public float hitDistanceMiddle;
    public float hitDistanceLeft;
    public float hitDistanceRight;
    public bool preventRightMovement = false;
    public bool preventLeftMovement = false;
    public bool preventJump = false;

    //Animation
    Animator animator;

    private void Awake()
    {
        //rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
        playerMechanicsScript = GetComponent<PlayerMechanics>();

        //locate pause menu and make invisible
        pauseMenu = GameObject.Find("Pause Menu").GetComponent<Canvas>();
        pauseMenu.gameObject.SetActive(false);

        //locate dialog
        dialog = GameObject.Find("Dialog").GetComponent<Canvas>();

        //locate Animator
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //detect if player is hitting ground
        //middle bottom
        if(Physics.Raycast(new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), out hit, 5f))
        {
            hitDistanceMiddle = hit.distance;
            Debug.DrawRay(new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), Color.yellow);
        }
        //left bottom
        if(Physics.Raycast(new Vector3(transform.position.x - 0.29f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), out hit, 5f))
        { 
            hitDistanceLeft = hit.distance;
            Debug.DrawRay(new Vector3(transform.position.x - 0.29f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), Color.red);
        }
        //right bottom
        if(Physics.Raycast(new Vector3(transform.position.x + 0.07f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), out hit, 5f))
        {
            hitDistanceRight = hit.distance;
            Debug.DrawRay(new Vector3(transform.position.x + 0.07f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), Color.green);
        }

        //prevent floating on walls
        if (inAir || didJump)
        {
            //right side
            if (Physics.Raycast(new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 0.2f))
            {
                //stop any movement (prevent floating on wall)
                if (movementSpeed > 0)
                {
                    movementSpeed = 0;
                }
                preventRightMovement = true;
                //jumpSpeed = -400;
                Debug.Log("Right side about to hit, Raycast!");
                Debug.DrawRay(new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), Color.magenta);
            }
            else
            {
                preventRightMovement = false;
            }
            //left side
            if (Physics.Raycast(new Vector3(transform.position.x - 0.1f, transform.position.y + 0.5f, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 0.2f))
            {
                //stop any movement (prevent floating on wall)
                if (movementSpeed < 0)
                {
                    movementSpeed = 0;
                }
                //jumpSpeed = -400;
                preventLeftMovement = true;
                Debug.Log("Left side about to hit, Raycast!");
                Debug.DrawRay(new Vector3(transform.position.x - 0.1f, transform.position.y + 0.5f, transform.position.z), transform.TransformDirection(Vector3.back), Color.magenta);
            }
            else
            {
                preventLeftMovement = false;
            }
        }

        //if distance from all raycasts indicates player is in the air
        if (hitDistanceLeft > 0.006f && hitDistanceMiddle > 0.006f && hitDistanceRight > 0.006f)
        {
            Debug.Log("In air!");
            inAir = true;
            if (jumpSpeed > -400)
            {
                jumpSpeed -= 9.81f;
            }
        }
        //if player touched the ground (at least 1 of the raycast distances is < 0.006)
        else if (inAir)
        {
            inAir = false;
            jumpSpeed = 0;
            preventRightMovement = false;
            preventLeftMovement = false;

            if (didJump)
            {
                didJump = false;
            }
        }
        //move player
        playerRigidbody.velocity = new Vector3(movementSpeed * Time.deltaTime, jumpSpeed * Time.deltaTime, 0);
    }

    private void Update()
    {
        //move right
        if (Input.GetKey(KeyCode.D) && preventRightMovement == false)
        {
            //set walking animation true
            animator.SetBool("iswalking", true);

            //Turn Mousey right
            transform.eulerAngles = new Vector3(0, 90, 0);

            //if player was moving the other direction then stop movment
            if (movementSpeed < 0)
            {
                movementSpeed = 0;
            }
            //increase movementSpeed
            if (movementSpeed < 300)
            {
                movementSpeed += 10;
            }
            playerMechanicsScript.UpdateCheeseDirection(true);
        }
        //move left
        else if (Input.GetKey(KeyCode.A) && preventLeftMovement == false)
        {
            //set walking animation true
            animator.SetBool("iswalking", true);

            //turn Mousey left
            transform.eulerAngles = new Vector3(0, 270, 0);

            //if player was moving the other direction then stop movment
            if (movementSpeed > 0)
            {
                movementSpeed = 0;
            }
            //decrease movementSpeed
            if (movementSpeed > -300)
            {
                movementSpeed -= 10;
            }
            playerMechanicsScript.UpdateCheeseDirection(false);
        }
        //neither movement key was hit
        else
        {
            //set walking animation false
            animator.SetBool("iswalking", false);

            movementSpeed = 0f;
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && didJump == false && preventJump == false)
        {
            Debug.Log("Jump!");
            didJump = true;
            jumpSpeed = 300;
            
        }
        //main menu key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if main menu isn't currently active
            if (pauseMenuActive == false)
            {
                FreezePlayer(false);
                pauseMenuActive = true;
                //makes main menu visible
                pauseMenu.gameObject.SetActive(true);

                //pauses game
                pauseMenu.GetComponent<PauseMenu>().PauseGame();
            }
            //if main menu is currently active
            else
            {
                ResumePlayer();
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

        preventLeftMovement = false;
        preventLeftMovement = false;
        preventJump = false;
    }
    public void FreezePlayer(bool endOfLevel)
    {
        preventLeftMovement = true;
        preventRightMovement = true;
        preventJump = true;
        if (endOfLevel)
        {
            playerMechanicsScript.endOfLevel = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");

        //get contacts for collision
        Vector3 direction = collision.GetContact(0).normal;

        //Debug.Log("collision.contactCount.ToString() = " + collision.contactCount.ToString());

        //if bottom is hit
        if (direction.y > 0)
        {
            Debug.Log("Hit floor");
            inAir = false;
            didJump = false;
        }
        //if top is hit
        else if (direction.y < 0)
        {
            Debug.Log("Hit roof");
            jumpSpeed = 0;
        }
        //left side
        if(direction.x > 0)
        {
            Debug.Log("Hit object on left");
            movementSpeed = 0;
        }
        //right side
        else if(direction.x < 0)
        {
            Debug.Log("Hit object on right");
            movementSpeed = 0;
        }
    }

    //0 = right
    //1 = left
    public void PreventPlayerMovement(byte direction)
    {
        if(direction == 0)
        {
            preventRightMovement = true;
        }
        else if (direction == 1)
        {
            preventLeftMovement = true;
        }
    }
    public void UnPreventPlayerMovement(byte direction)
    {
        if (direction == 0)
        {
            preventRightMovement = false;
        }
        else if (direction == 1)
        {
            preventLeftMovement = false;
        }
    }
}
