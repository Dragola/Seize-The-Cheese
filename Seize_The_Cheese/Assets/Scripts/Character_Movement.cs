using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]

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

    private BoxCollider2D boxCollider;

    public Text txt;

    public bool touchedStrongCheese = false;
    public bool touchedHealthCheese = false;
    public bool touchedDust = false;
    public bool touchedSpider = false;


    public bool onStrongCheese = false;
    public bool dead = false;
    public float timeRemaining = 11;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Dust")
        {
            Destroy(collision.gameObject);

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
            Destroy(collision.gameObject);

        }


        if (collision.tag == "HealthCheese")
        {
            healthBar.value += 0.5f;
            Destroy(collision.gameObject);

            if (!touchedHealthCheese)
            {
                touchedHealthCheese = true;
                healthCheesePanel.SetActive(true);
                PauseGame();
            }
        }

        if (collision.tag == "Spider")
        {
            if (!touchedSpider)
            {
                touchedSpider = true;
                Debug.Log("Touched Spider");
                introPanel.SetActive(true);
                PauseGame();
            }


        }

        if (collision.tag == "StrongCheese")
        {

            onStrongCheese = true;
            Destroy(collision.gameObject);

            if (!touchedStrongCheese)
            {
                touchedStrongCheese = true;
                strongCheesePanel.SetActive(true);
                PauseGame();
            }
                       
        }



    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.X) && collision.tag == "CubeCheese")
        {
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collision.GetComponent<Rigidbody2D>().simulated = false;
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(7.18204f, 5.82f);
            boxCollider.offset = new Vector2(1.552225f, 0);
            collision.transform.parent = this.transform;
            didPickUp = true;
            Debug.Log("Scooped");
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

        txt.text = string.Format("Power Up Time: {00:0}", seconds);
    }
}