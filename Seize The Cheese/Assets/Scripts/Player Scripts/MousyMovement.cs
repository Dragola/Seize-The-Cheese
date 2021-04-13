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
    public sbyte cheeseCollision = -1;

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
            animator.SetBool("isgrounded", true);
            animator.SetBool("isjumping", false);
        }
        //left bottom
        else if (Physics.Raycast(new Vector3(playerCapsuleCollider.bounds.center.x - 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.down, out hit, playerCapsuleCollider.bounds.extents.y))
        {
            //reset jumpvelocity
            if ((inAir == false || didJump == false) && jumpVelocity != 0)
            {
                jumpVelocity = 0;
            }
            Debug.Log("Fit Floor via raycast left");
            didJump = false;
            inAir = false;
            animator.SetBool("isjumping", false);
            animator.SetBool("isgrounded", true);
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
            animator.SetBool("isjumping", false);
            animator.SetBool("isgrounded", true);
        }
        //no ray hitting ground
        else
        {
            Debug.Log("Not touching floor");
            inAir = true;
            animator.SetBool("isgrounded", false);
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
        }
        //left bottom
        else if (Physics.Raycast(new Vector3(playerCapsuleCollider.bounds.center.x +- 0.18f, playerCapsuleCollider.bounds.center.y, playerCapsuleCollider.bounds.center.z), Vector3.up, out hit, playerCapsuleCollider.bounds.extents.y))
        {
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
            }
        }
        //move player
        playerRigidbody.velocity = new Vector3(movmentVelocity * Time.deltaTime, jumpVelocity * Time.deltaTime, 0);
    }
    private void Update()
    {
        if (cheeseCollision == 0)
        {
            preventRightMovement = true;
            if (movmentVelocity > 100)
            {
                movmentVelocity = 100;
            }
        }
        else if (cheeseCollision == 1)
        {
            preventLeftMovement = true;
            if (movmentVelocity > -100)
            {
                movmentVelocity = -100;
            }
        }
        else
        {
            preventRightMovement = false;
            preventLeftMovement = false;
        }
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
            else if (movmentVelocity > 100 && cheeseCollision != -1)
            {
                movmentVelocity = 10;
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
            if (movmentVelocity > -300 && cheeseCollision == -1)
            {
                movmentVelocity -= 10;
            }
            else if (movmentVelocity > -100 && cheeseCollision != -1)
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
            //Animation control
            animator.SetBool("isjumping", true);

            Debug.Log("Jump!");
            didJump = true;
            inAir = true;
            jumpVelocity = 310;
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
                Cursor.visible = true;

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
                Cursor.visible = false;

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
        Debug.Log("Player: Collision Enter: " + collision.collider.name);
        //make sure object it's the ceiling
        if ((inAir || cheeseCollision != -1) && collision.collider.name.CompareTo("Mousy") != 0) 
        {

            Debug.Log("Player: Collision Staying: " + collision.collider.name);

            if (ceilingHitObject != null && ceilingHitObject != collision.gameObject)
            {
                Debug.Log("Hitting ceiling so ignoring this collision");
                movmentVelocity = 0;
            }
            else
            {
                Debug.Log("Collision Staying not ceiling");
                //hit front of player (object to the right)
                if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
                {
                    preventRightMovement = true;
                }
                //hit front of player (object to the left)
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
        
        //release and movement prevention
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
    public void PreventPlayerMovement(sbyte direction)
    {
        cheeseCollision = direction;
    }
    public void UnPreventPlayerMovement()
    {
        cheeseCollision = -1;

        Debug.Log("Player: UnPreventPlayerMovement() called");
        preventRightMovement = false;
        preventLeftMovement = false;
    }
}
