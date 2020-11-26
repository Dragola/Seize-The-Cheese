using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Movement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector2 moveDirection = Vector2.zero;
    private float turner;
    private float looker;
    public float sensitivity;

    public float health;
    public Slider healthBar;
    public bool didPickUp = false;

    public GameObject introPanel;
    public GameObject DustPanel;
    public GameObject healthCheesePanel;
    public GameObject strongCheesePanel;
    public GameObject deathPanel;
    public GameObject holder;
    public GameObject emptySlot;

    public Text txt;

    public bool touchedStrongCheese = false;
    public bool touchedHealthCheese = false;
    public bool touchedDust = false;
    public bool touchedSpider = false;


    public bool onStrongCheese = false;
    public bool dead = false;
    public float timeRemaining = 11;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dust")
        {
            Destroy(other.gameObject);

            if (!onStrongCheese) {
                healthBar.value -= 0.5f;

                /* if (health <= 0)
                {
                    Cursor.visible = true;
                }
                */

                if (!touchedDust)
                {
                    Debug.Log("Touched");
                    touchedDust = true;
                    DustPanel.SetActive(true);
                    PauseGame();
                }

                if (healthBar.value <= 0)
                {
                    Debug.Log("Dead");
                    Cursor.visible = true;
                    deathPanel.SetActive(true);
                    PauseGame();
                    dead = true;

                }
            }
        }
        

        if (other.tag == "HealthCheese")
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

        if (other.tag == "Spider")
        {
            if (!touchedSpider)
            {
                touchedSpider = true;
                introPanel.SetActive(true);
                PauseGame();
            }


        }

        if (other.tag == "StrongCheese")
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

        if (other.tag == "Gate1")
        {
            Debug.Log("Hit1");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 1;
        }
        if (other.tag == "Gate2")
        {
            Debug.Log("Hit2");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 2;
        }
        if (other.tag == "Gate3")
        {
            Debug.Log("Hit3");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 3;
        }
        if (other.tag == "Gate4")
        {
            Debug.Log("Hit4");
            GameObject camera = GameObject.Find("Main Camera");
            CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            cameraLerp.cameraPositionIndex = 4;
        }



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CubeCheese")
        {
            if (didPickUp)
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.parent = this.transform;
                Debug.Log("Scooped");
            }
            else
            {
                other.transform.parent = null;
                other.GetComponent<Rigidbody>().isKinematic = false;
                Debug.Log("Dropped");
            }
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CubeCheese")
        {
            other.transform.parent = null;
            other.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("Dropped");
        }
    }
    */

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        introPanel.SetActive(false);
        strongCheesePanel.SetActive(false);
        healthCheesePanel.SetActive(false);
        DustPanel.SetActive(false);

        if (!dead)
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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Return key was pressed.");
            ResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            didPickUp = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            didPickUp = false;

        }

        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);

        if (onStrongCheese) { 
            if (timeRemaining > 0)
            {
                Debug.Log(timeRemaining);
                holder.SetActive(true);
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            if (timeRemaining <= 0)
            {
                Debug.Log("Done");
                holder.SetActive(false);
                onStrongCheese = false;
                timeRemaining = 11;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        txt.text = string.Format("{00:0}", seconds);
    }
}