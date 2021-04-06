using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousyMovement : MonoBehaviour
{
    //player
    public float movmentVelocity = 0f;
    public float jumpVelocity = 0;
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
    private RaycastHit hit;
    public GameObject floorhHitObject;
    public GameObject ceilingHitObject;
    public float hitDistanceMiddle;
    public float hitDistanceLeft;
    public float hitDistanceRight;
    public bool preventRightMovement = false;
    public bool preventLeftMovement = false;
    public bool preventJump = false;
    private CapsuleCollider playerCapsuleCollider = null;

    //Animation
    Animator animator;

    private void Awake()
    {
        //rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
        playerMechanicsScript = GetComponent<PlayerMechanics>();
        playerCapsuleCollider = GetComponent<CapsuleCollider>();

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
        //floor detection
        Debug.DrawRay(playerCapsuleCollider.bounds.center, Vector3.down, Color.yellow);
        Debug.DrawRay(new Vector3(playerCapsuleCollider.bounds.center.x - 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.down, Color.gray);
        Debug.DrawRay(new Vector3(playerCapsuleCollider.bounds.center.x + 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.down, Color.gray);
        //detect if player is hitting ground
        //middle bottom
        if (Physics.Raycast(playerCapsuleCollider.bounds.center, Vector3.down, out hit, playerCapsuleCollider.bounds.extents.y + 0.05f))
        {
            //reset jumpvelocity
            if ((inAir == false || didJump == false) && jumpVelocity != 0)
            {
                jumpVelocity = 0;
            }
            Debug.Log("Fit Floor via raycast middle");
            didJump = false;
            inAir = false;
        }
        //left bottom
        else if (Physics.Raycast(new Vector3(playerCapsuleCollider.bounds.center.x - 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.down, out hit, playerCapsuleCollider.bounds.extents.y))
        {
<<<<<<< HEAD
<<<<<<< HEAD
            //reset jumpvelocity
            if ((inAir == false || didJump == false) && jumpVelocity != 0)
=======
=======
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
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
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
            {
                jumpVelocity = 0;
            }
            Debug.Log("Fit Floor via raycast left");
            didJump = false;
            inAir = false;
        }
        //right bottom
        else if (Physics.Raycast(new Vector3(playerCapsuleCollider.bounds.center.x + 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.down, out hit, playerCapsuleCollider.bounds.extents.y))
        {
            //reset jumpvelocity
            if ((inAir == false || didJump == false) && jumpVelocity != 0)
            {
                jumpVelocity = 0;
            }
            Debug.Log("Fit Floor via raycast right");
            didJump = false;
            inAir = false;
        }
        //no ray hitting ground
        else
        {
            Debug.Log("Not touching floor");
            inAir = true;
<<<<<<< HEAD
<<<<<<< HEAD
        }

        //jump detection
        Debug.DrawRay(playerCapsuleCollider.bounds.center, Vector3.up, Color.yellow);
        Debug.DrawRay(new Vector3(playerCapsuleCollider.bounds.center.x - 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.up, Color.gray);
        Debug.DrawRay(new Vector3(playerCapsuleCollider.bounds.center.x + 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.up, Color.gray);
        //detect if player is hitting ground
        //middle bottom
        if (Physics.Raycast(playerCapsuleCollider.bounds.center, Vector3.up, out hit, playerCapsuleCollider.bounds.extents.y + 0.05f))
        {
            Debug.Log("Fit ceiling via raycast miidle");
            jumpVelocity = 0;
            ceilingHitObject = hit.collider.gameObject;
=======
=======
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
            if (jumpSpeed > -400)
            {
                jumpSpeed -= 9.81f;
            }
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
        }
        //left bottom
        else if (Physics.Raycast(new Vector3(playerCapsuleCollider.bounds.center.x +- 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.up, out hit, playerCapsuleCollider.bounds.extents.y))
        {
<<<<<<< HEAD
            Debug.Log("Fit ceiling via raycast left");
            jumpVelocity = 0;
            ceilingHitObject = hit.collider.gameObject;
        }
        //right bottom
        else if (Physics.Raycast(new Vector3(playerCapsuleCollider.bounds.center.x + 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.up, out hit, playerCapsuleCollider.bounds.extents.y))
        {
            Debug.Log("Fit ceiling via raycast right");
            jumpVelocity = 0;
            ceilingHitObject = hit.collider.gameObject;
        }
        else
        {
            ceilingHitObject = null;
        }

        //prevent floating on walls
        if (inAir || didJump)
        {
            Debug.Log("InAir = " + inAir + "|| didJump = " + didJump);
            //
            if (jumpVelocity > -200)
            {
                jumpVelocity -= 10;
            }
            else
            {
                jumpVelocity = -200;
=======
            inAir = false;
            jumpSpeed = 0;
            preventRightMovement = false;
            preventLeftMovement = false;

            if (didJump)
            {
                didJump = false;
<<<<<<< HEAD
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
=======
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
            }
        }
            //    //right side
            //    if (Physics.Raycast(new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 0.2f))
            //    {

            //        //stop any movement (prevent floating on wall)
            //        if (movmentVelocity > 0)
            //        {
            //            movmentVelocity = 0;
            //        }
            //        preventRightMovement = true;
            //        //jumpVelocity = -400;
            //        Debug.Log("Right side about to hit, Raycast!");
            //        Debug.DrawRay(new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), Color.magenta);
            //    }
            //    else
            //    {
            //        preventRightMovement = false;
            //    }
            //    //left side
            //    if (Physics.Raycast(new Vector3(transform.position.x - 0.1f, transform.position.y + 0.5f, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 0.2f))
            //    {
            //        //stop any movement (prevent floating on wall)
            //        if (movmentVelocity < 0)
            //        {
            //            movmentVelocity = 0;
            //        }
            //        //jumpVelocity = -400;
            //        preventLeftMovement = true;
            //        Debug.Log("Left side about to hit, Raycast!");
            //        Debug.DrawRay(new Vector3(transform.position.x - 0.1f, transform.position.y + 0.5f, transform.position.z), transform.TransformDirection(Vector3.back), Color.magenta);
            //    }
            //    else
            //    {
            //        preventLeftMovement = false;
            //    }
            //}

            //if distance from all raycasts indicates player is in the air
            //&& hitDistanceMiddle > 0.0055f && hitDistanceRight > 0.0055f
            //if (hitDistanceLeft > 0.006f)
            //{
            //    Debug.Log("In air!");
            //    inAir = true;

            //    //Animation control
            //    //animator.SetBool("isgrounded", false);

            //    if (jumpVelocity > -300)
            //    {
            //        jumpVelocity = -300;
            //    }
            //}
            ////if player touched the ground (at least 1 of the raycast distances is < 0.006)
            //else if (inAir)
            //{
            //    inAir = false;
            //    jumpVelocity = 0;
            //    preventRightMovement = false;
            //    preventLeftMovement = false;

            //    //Animation control
            //    //animator.SetBool("isgrounded", true);

            //    //Animation control
            //    animator.SetBool("isjumping", false);

            //    if (didJump)
            //    {
            //        didJump = false;
            //    }
            //}
            //move player
            playerRigidbody.velocity = new Vector3(movmentVelocity * Time.deltaTime, jumpVelocity * Time.deltaTime, 0);
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
            if (movmentVelocity < 0)
            {
                movmentVelocity = 0;
            }
            //increase movmentVelocity
            if (movmentVelocity < 300)
            {
                movmentVelocity += 10;
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
            if (movmentVelocity > 0)
            {
                movmentVelocity = 0;
            }
            //decrease movmentVelocity
            if (movmentVelocity > -300)
            {
                movmentVelocity -= 10;
            }
            playerMechanicsScript.UpdateCheeseDirection(false);
        }
        //neither movement key was hit
        else
        {
            //set walking animation false
            animator.SetBool("iswalking", false);

            movmentVelocity = 0f;
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && didJump == false && preventJump == false && inAir == false)
        {
            Debug.Log("Jump!");
            didJump = true;
<<<<<<< HEAD
            inAir = true;
            jumpVelocity = 310;
=======
            jumpSpeed = 300;
            
<<<<<<< HEAD
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
=======
>>>>>>> parent of 10a9cb5 (Implemented rudimentary jump and hit animations)
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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision Detected");
    //    //get contacts for collision

    //    if (collision.collider.name.CompareTo("Mousy") != 0 && inAir && hitCeiling == false)
    //    {
    //        Debug.Log("Collision Detected not mousy");
    //        //hit front of player
    //        if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
    //        {
    //            preventRightMovement = true;
    //        }
    //        else if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
    //        {
    //            preventLeftMovement = true;
    //        }
    //        movmentVelocity = 0;
    //    }
    //}
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision Staying");

        //make sure object it's the ceiling
        if (inAir) {
            if (ceilingHitObject != null && ceilingHitObject != collision.gameObject)
            {
                Debug.Log("Hitting ceiling so ignoring this collision");
                movmentVelocity = 0;
            }
            else
            {
                Debug.Log("Collision Staying not ceiling");
                //hit front of player
                if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
                {
                    preventRightMovement = true;
                }
                else if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
                {
                    preventLeftMovement = true;
                }
                movmentVelocity = 0;
            }
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision Exited");
        //get contacts for collision
       
        if (preventRightMovement)
        {
            preventRightMovement = false;
        }
        else if (preventLeftMovement)
        {
            preventLeftMovement = false;
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
