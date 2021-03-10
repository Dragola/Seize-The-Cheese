﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMechanics : MonoBehaviour
{
    public float health; // health variable
    public Slider healthBar; // slider gameobject

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

    // boolean variables associated to various powerups and enemies.
    public bool touchedStrongCheese = false;
    public bool touchedHealthCheese = false;
    public bool touchedDust = false;
    public bool touchedSpider = false;

    public bool onStrongCheese = false; // checks to see if the player who at the time of eating strong cheese, is currently on it 
    public bool dead = false; // checks to see if the player is dead
    public float timeRemaining = 6; // sets the countdown timer value from 6 seconds
    public bool notInDialog = true; //prevents strong cheese from counting down if in dialog

    //bryan's cheese block variables
    public bool pickedUpCheese = false;
    public bool pickedUpCheese2 = false;
    public GameObject cheeseBlock = null;
    public GameObject secondCheeseBlock = null;
    public byte collisionDirection = 0;     //0 = neither, 1 = right, 2 = left


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("player collision = " + collision.gameObject.name);

        //if hit block of cheese then call function to pick it up
        if (collision.gameObject.name.CompareTo("Cheese") == 0 && cheeseBlock == null)
        {
            //set the cheese block one touch one
            cheeseBlock = collision.gameObject;

            //get contacts for collision
            Vector2 direction = collision.GetContact(0).normal;

            //hit from right side- put block on left side
            if (direction.x == 1)
            {
                collisionDirection = 2;
            }
            //hit from left side- put block on right side
            else if (direction.x == -1)
            {
                collisionDirection = 1;
            }
        }
        else if (collision.gameObject.name.CompareTo("Cheese") == 0 && secondCheeseBlock == null)
        {
            //set the cheese block one touch one
            secondCheeseBlock = collision.gameObject;

            //get contacts for collision
            Vector2 direction = collision.GetContact(0).normal;

            //hit from right side- put block on left side
            if (direction.x == 1)
            {
                collisionDirection = 2;
            }
            //hit from left side- put block on right side
            else if (direction.x == -1)
            {
                collisionDirection = 1;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //set the cheese block to null if not touching
        if (cheeseBlock != null && pickedUpCheese == false)
        {
            cheeseBlock = null;
        }
        else if (secondCheeseBlock != null && pickedUpCheese2 == false)
        {
            secondCheeseBlock = null;
        }
        collisionDirection = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dust") // when player touches a 'Dust Bunnie'
        {
            Destroy(other.gameObject); // destorys the dust bunnie
            if (!onStrongCheese)
            {
                healthBar.value -= 0.5f; // lowers current health by half

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
        }

        if (other.tag == "StrongCheese")  // when the player interacts with 'StrongCheese' 
        {
            onStrongCheese = true;
            Destroy(other.gameObject);

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
            //Debug.Log("Return key was pressed.");
            ResumeGame();
        }

        //pickup cheese
        if (cheeseBlock != null && pickedUpCheese == false && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Picked up first cheese");
            //indicate cheese was picked up
            pickedUpCheese = true;

            //prevent rigidbody from moving block while being carried
            cheeseBlock.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            //attach gameobject to player
            cheeseBlock.transform.parent = this.gameObject.transform;

            //tell block it's been picked up
            cheeseBlock.GetComponent<CheeseBlock>().PickedUp(collisionDirection, false);

            //reset collision drection
            collisionDirection = 0;

            PlayerMovement(2);
            PlayerMovement(5);
        }
        else if (secondCheeseBlock != null && pickedUpCheese == true && pickedUpCheese2 == false && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Picked up second cheese");

            pickedUpCheese2 = true;

            Debug.Log("Second Cheese pickup");

            //prevent rigidbody from moving block while being carried
            secondCheeseBlock.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            //attach gameobject to player
            secondCheeseBlock.transform.parent = this.gameObject.transform;

            //tell block it's been picked up
            secondCheeseBlock.GetComponent<CheeseBlock>().PickedUp(collisionDirection, true);

            //reset collision drection
            collisionDirection = 0;

            PlayerMovement(2);
            PlayerMovement(5);
        }
        //release cheese if holding one
        if(cheeseBlock!= null && pickedUpCheese == true && Input.GetKeyDown(KeyCode.R))
        {
            DropCheese();
        }

        //if strong cheese effect is active
        if (onStrongCheese)
        { // when the player picks up strong cheese a count down timer aprears inside a panel
            if (timeRemaining > 0 && notInDialog == true)
            {
                //Debug.Log(timeRemaining);
                holder.SetActive(true);
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            else if (timeRemaining <= 0) // once the time reaches 0 on the timer the panel disappears
            {
                //Debug.Log("Done");
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
    public void DropCheese()
    {
        pickedUpCheese = false;

        //indicate to cheese that it has been dropped
        cheeseBlock.GetComponent<CheeseBlock>().Dropped();

        //set constraints for rigidbody
        Rigidbody cheeseRigid = cheeseBlock.GetComponent<Rigidbody>();
        cheeseRigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        //remove parent for cheese
        cheeseBlock.transform.parent = null;

        //drop second cheese
        if (secondCheeseBlock != null && pickedUpCheese2 == true)
        {
            pickedUpCheese2 = false;

            //indicate to cheese that it has been dropped
            secondCheeseBlock.GetComponent<CheeseBlock>().Dropped();

            //set constraints for rigidbody
            cheeseRigid = secondCheeseBlock.GetComponent<Rigidbody>();
            cheeseRigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            //remove parent for cheese
            secondCheeseBlock.transform.parent = null;

            //null reference
            secondCheeseBlock = null;
        }

        //enable movement and uncheck cheese hitting wall
        PlayerMovement(2);
        PlayerMovement(5);
    }
    public void PlayerMovement(byte direction)
    {
        //prevent right movement
        if(direction == 0)
        {
            this.GetComponent<PlayerMovement>().canMoveRight = false;
            this.GetComponent<PlayerMovement>().cheeseHittingWall = true;
        }
        //prevent left movement
        else if (direction == 1)
        {
            this.GetComponent<PlayerMovement>().canMoveLeft = false;
            this.GetComponent<PlayerMovement>().cheeseHittingWall = true;
        }
        //enable movment
        else if (direction == 2)
        {
            //only enable movment if ray isn't hitting
            if (this.GetComponent<PlayerMovement>().cheeseRayHit == false)
            {
                this.GetComponent<PlayerMovement>().canMoveLeft = true;
                this.GetComponent<PlayerMovement>().canMoveRight = true;
            }
            this.GetComponent<PlayerMovement>().cheeseHittingWall = false;
        }
        //raycast hit left
        else if (direction == 3)
        {
            this.GetComponent<PlayerMovement>().cheeseRayHit = true;
            this.GetComponent<PlayerMovement>().canMoveLeft = false;
        }
        //raycast hity right
        else if(direction == 4)
        {
            this.GetComponent<PlayerMovement>().cheeseRayHit = true;
            this.GetComponent<PlayerMovement>().canMoveRight = false;
        }
        //ray not hitting
        else if (direction == 5)
        {
            //only enable movment if cheese is not touching wall
            if (this.GetComponent<PlayerMovement>().cheeseHittingWall == false)
            {
                this.GetComponent<PlayerMovement>().canMoveLeft = true;
                this.GetComponent<PlayerMovement>().canMoveRight = true;
            }
            this.GetComponent<PlayerMovement>().cheeseRayHit = false;
        }
    }
    public void CheeseBlockHit(GameObject cheese, byte status) 
    {
        //reference cheese block if hit
        if (status == 0 && secondCheeseBlock == null)
        {
            secondCheeseBlock = cheese;
        }
        else if (status == 1)
        {
            secondCheeseBlock = cheese;
        }
    }
    public GameObject GetSecondCheeseBlock()
    {
        return secondCheeseBlock;
    }
    public void UpdateCheeseDirection(bool isFacingRight)
    {
        //if holding first block
        if(cheeseBlock !=null && pickedUpCheese == true)
        {
            cheeseBlock.GetComponent<CheeseBlock>().UpdateCheeseDirection(isFacingRight);
        }
        //if holding a second cheese block
        if(secondCheeseBlock != null && pickedUpCheese2 == true)
        {
            secondCheeseBlock.GetComponent<CheeseBlock>().UpdateCheeseDirection(isFacingRight);
        } 
    }
}

