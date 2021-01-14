using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMechanics : MonoBehaviour
{

    public float health; // health variable
    public Slider healthBar; // slider gameobject

    public bool didPickUpParentCube = false;
    public bool didPickUpChildCube = false;
    public bool notInDialog = true; //prevents strong cheese from counting down if in dialog

    //various panels that appear once the player makes contact a specific object or has died
    public GameObject introPanel;
    public GameObject DustPanel;
    public GameObject healthCheesePanel;
    public GameObject strongCheesePanel;
    public GameObject deathPanel;
    public GameObject holder;
    public GameObject emptySlot;

    public float amount;

    public Text txt; // text field gameobject which is used in the countdown timer appears for the power up

    public GameObject pickUpPosition_Left; // gameobject that is found on the left side of the player, used to position a picked up cube
    public GameObject pickUpPosition_Right; // gameobject that is found on the right side of the player, used to position a picked up cube
    public bool pickedUpOnLeftSide = false; // boolean value created to check if the player has picked up the cube on his left side
    public bool pickedUpOnRightSide = false; // boolean value created to check if the player has picked up the cube on his right side


    public GameObject pickedUpMainCube; //empty gameobject created to be a placeholder in order to be linked to a future picked up cube.

    // boolean variables associated to various powerups and enemies.
    public bool touchedStrongCheese = false;
    public bool touchedHealthCheese = false;
    public bool touchedDust = false;
    public bool touchedSpider = false;


    public bool onStrongCheese = false; // checks to see if the player who at the time of eating strong cheese, is currently on it 
    public bool dead = false; // checks to see if the player is dead
    public float timeRemaining = 6; // sets the countdown timer value from 6 seconds

    private BoxCollider boxCollider;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dust") // when player touches a 'Dust Bunnie'
        {
            Destroy(other.gameObject); // destorys the dust bunnie

            if (!onStrongCheese)
            {
                healthBar.value -= 0.5f; // lowers current health by half

                if (!touchedDust) // when the player touches the dust bunny for the first time a message appears exaplaing what a dust bunnie does when interacted with.
                {
                    Debug.Log("Touched");
                    touchedDust = true;
                    DustPanel.SetActive(true);
                    PauseGame();
                }

                if (healthBar.value <= 0) // if health is equal to 0, player is dead thus cursor is visible and a death panel appears
                {
                    Debug.Log("Dead");
                    Cursor.visible = true;
                    deathPanel.SetActive(true);
                    PauseGame();
                    dead = true;

                }
            }
        }


        if (other.tag == "HealthCheese") // when the player interacts with a 'Health Cheese' power up
        {
            healthBar.value += 0.5f;
            Destroy(other.gameObject);

            if (!touchedHealthCheese)
            {
                touchedHealthCheese = true;
                healthCheesePanel.SetActive(true);
                PauseGame();
            }
        }

        if (other.tag == "Spider") // when the player interacts with 'Sir Bitsy' 
        {
            if (!touchedSpider)
            {
                touchedSpider = true;
                introPanel.SetActive(true);
                PauseGame();
            }
        }

        if (other.tag == "StrongCheese")  // when the player interacts with 'StrongCheese' 
        {
            onStrongCheese = true;
            Destroy(other.gameObject);

            if (!touchedStrongCheese)
            {
                touchedStrongCheese = true;
                strongCheesePanel.SetActive(true);
                PauseGame();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "touchPointLeft" && (!didPickUpParentCube && !didPickUpChildCube) && Input.GetKey(KeyCode.X)) // if button X is pressed and player is in a cubes touchPointLeft trigger
        {
            pickedUpMainCube = other.transform.parent.gameObject; // assigns an empty gameobject varaible to equal the picked up cube (allows the cube gameobject varaibles to be altered outside the trigger function)
            pickedUpMainCube.GetComponent<CheeseController>().didPickUp = true;

            if (pickedUpMainCube.GetComponent<CheeseController>().touchingWall)
            {
                GetComponent<PlayerMovement>().canMoveRight = false;
            }

            other.transform.parent.parent = pickUpPosition_Right.transform; // the picked up cubes parent is now the players pickUpPosition_Right gameobject which is found on the rightside of the player
            other.transform.parent.position = pickUpPosition_Right.transform.position; // sets the position of the picked up cube to equal the pickUpPosition_Right position
            other.GetComponentInParent<Rigidbody>().useGravity = false; // disables gravity for the picked up cube

            if (other.transform.parent.tag == "CubeCheese") // checks to see if the player is picking up the most parent cube (which will always be the most bottom cube in a stack)
            {
                didPickUpParentCube = true; // sets to true
                pickedUpOnLeftSide = true; // sets to true
                //Debug.Log("1"); 
            }

            if (other.transform.parent.tag == "ChildCube")
            {

                didPickUpChildCube = true; // sets to true
                pickedUpOnLeftSide = true; // sets to true
                //other.transform.parent.parent.GetComponent<stackScript>().isAnotherBoxStacked = false; // sets isAnotherBoxStacked in the cubes touchpoint gameobject's stackScript
                other.transform.parent.tag = "CubeCheese"; //sets the picked cube's tag to equal "CubeCheese"
                //Debug.Log("2");
            }
        }

        if (other.tag == "touchPointRight" && (!didPickUpParentCube && !didPickUpChildCube) && Input.GetKey(KeyCode.X))
        {
            pickedUpMainCube = other.transform.parent.gameObject; // assigns an empty gameobject varaible to equal the picked up cube (allows the cube gameobject varaibles to be altered outside the trigger function)
            pickedUpMainCube.GetComponent<CheeseController>().didPickUp = true;

            if (pickedUpMainCube.GetComponent<CheeseController>().touchingWall)
            {
                GetComponent<PlayerMovement>().canMoveLeft = false;
            }
            other.transform.parent.parent = pickUpPosition_Left.transform; // the picked up cubes parent is now the players pickUpPosition_Right gameobject which is found on the rightside of the player
            other.transform.parent.position = pickUpPosition_Left.transform.position; // sets the position of the picked up cube to equal the pickUpPosition_Right position
            other.GetComponentInParent<Rigidbody>().useGravity = false; // disables gravity for the picked up cube

            if (other.transform.parent.tag == "CubeCheese")
            {
                didPickUpParentCube = true; // sets to true
                pickedUpOnRightSide = true; // sets to true
                //Debug.Log("3");
            }

            if (other.transform.parent.tag == "ChildCube")
            {

                didPickUpChildCube = true;
                pickedUpOnRightSide = true;
                //other.transform.parent.parent.GetComponent<stackScript>().isAnotherBoxStacked = false; // sets isAnotherBoxStacked in the cubes touchpoint gameobject's stackScript
                other.transform.parent.tag = "CubeCheese"; //sets the picked cube's tag to equal "CubeCheese"
                //Debug.Log("4");
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0; //sets the time in game to 0, thus pausing the game
    }

    void ResumeGame()
    {
        introPanel.SetActive(false); // sets introPanel innactive
        strongCheesePanel.SetActive(false); // sets introPanel innactive
        healthCheesePanel.SetActive(false); // sets introPanel innactive
        DustPanel.SetActive(false); // sets introPanel innactive

        if (!dead) // if is not dead continue the game
        {
            Time.timeScale = 1;
        }
    }


    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?

        if (Input.GetKeyDown(KeyCode.Return)) // if enter is pressed the game continues unless the player is dead.
        {
            Debug.Log("Return key was pressed.");
            ResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.K)) // iterates through all CubeCheese and ChildCube and drops them whe K is pressed
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Transform[] ks = GetComponentsInChildren<Transform>();
            foreach (Transform t in ks)
            {
                if (t.tag == "CubeCheese" || t.tag == "ChildCube")
                {
                    t.transform.parent = null;
                    pickedUpMainCube.GetComponent<CheeseController>().didPickUp = false;
                    t.GetComponent<Rigidbody>().useGravity = true;

                    if (pickedUpMainCube.GetComponent<CheeseController>().touchingWall)
                        t.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                    else
                        t.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

                    //t.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    didPickUpChildCube = false;
                    didPickUpParentCube = false;
                    pickedUpOnLeftSide = false;
                    pickedUpOnRightSide = false;
                    Debug.Log("Dropped");
                }
            }
        }

        if (onStrongCheese)
        { // when the player picks up strong cheese a count down timer aprears inside a panel
            if (timeRemaining > 0)
            {
                Debug.Log(timeRemaining);
                holder.SetActive(true);
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            if (timeRemaining <= 0) // once the time reaches 0 on the timer the panel disappears
            {
                Debug.Log("Done");
                holder.SetActive(false);
                onStrongCheese = false;
                timeRemaining = 11;
            }
        }
    }

    // calculation for the timer
    void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        txt.text = string.Format("Power Timer: {00:0}", seconds);
    }

    public void SetInDialog(bool status)
    {
        notInDialog = status;
    }
}

