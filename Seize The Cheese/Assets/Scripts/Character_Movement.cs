using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Movement : MonoBehaviour
{


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

    public GameObject pickUpPosition;

    public bool outOfPlace = false;


    public bool touchedStrongCheese = false;
    public bool touchedHealthCheese = false;
    public bool touchedDust = false;
    public bool touchedSpider = false;


    public bool onStrongCheese = false;
    public bool dead = false;
    public float timeRemaining = 11;

    private BoxCollider boxCollider;

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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CubeCheese" && Input.GetKey(KeyCode.X))
        {
            //other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent = this.transform;
            other.transform.position = pickUpPosition.transform.position;
            other.GetComponent<Rigidbody>().useGravity = false;


            //boxCollider = GetComponent<BoxCollider>();
            //boxCollider.size = new Vector3(2.551425f, 0.866585f, 1);
            //boxCollider.center = new Vector3(0.7757122f, -0.07391864f, 0);
            didPickUp = true;

            Debug.Log("Scooped");

        }

        if (other.tag == "CubeCheese" && Input.GetKey(KeyCode.K))
        {
            other.transform.parent = null;
            //other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().useGravity = true;

            //boxCollider = GetComponent<BoxCollider>();
            //boxCollider.size = new Vector3(1, 0.866585f, 1);
            //boxCollider.center = new Vector3(0,-0.07391864f, 0);
            didPickUp = false;

            Debug.Log("Dropped");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CubeCheese")
        {
            other.transform.parent = null;
            //other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().useGravity = true;
            didPickUp = false;

            //boxCollider = GetComponent<BoxCollider>();
            //boxCollider.size = new Vector3(1, 0.866585f, 1);
            //boxCollider.center = new Vector3(0,-0.07391864f, 0);
            Debug.Log("Dropped");
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

    void OnTouchedChild(GameObject childObject)
    {
        Debug.Log("touched child " + childObject.name, childObject);
        // do whatever
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