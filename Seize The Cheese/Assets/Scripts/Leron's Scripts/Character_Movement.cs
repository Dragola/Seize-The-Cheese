using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Movement : MonoBehaviour
{

    public float health;
    public Slider healthBar;

    public bool didPickUpParentCube = false;
    public bool didPickUpChildCube = false;

    public GameObject introPanel;
    public GameObject DustPanel;
    public GameObject healthCheesePanel;
    public GameObject strongCheesePanel;
    public GameObject deathPanel;
    public GameObject holder;
    public GameObject emptySlot;

    public Text txt;

    public GameObject pickUpPosition_Left;
    public GameObject pickUpPosition_Right;
    public bool pickedUpOnLeftSide = false;
    public bool pickedUpOnRightSide = false;

    public bool outOfPlace = false;

    public GameObject pickedUpMainCube;

    public bool touchedStrongCheese = false;
    public bool touchedHealthCheese = false;
    public bool touchedDust = false;
    public bool touchedSpider = false;


    public bool onStrongCheese = false;
    public bool dead = false;
    public float timeRemaining = 6;

    private BoxCollider boxCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dust")
        {
            Destroy(other.gameObject);

            if (!onStrongCheese) {
                healthBar.value -= 0.5f;

                 if (health <= 0) { 
               
                    Cursor.visible = true;
                }
                
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
        if (other.tag == "touchPointLeft" && Input.GetKey(KeyCode.X))
        {

            if (!didPickUpParentCube && other.transform.parent.tag == "CubeCheese")
            {
                other.transform.parent.parent = this.transform;
                other.transform.parent.parent = pickUpPosition_Right.transform;
                other.transform.parent.position = pickUpPosition_Right.transform.position;
                pickedUpMainCube = other.transform.parent.gameObject;
                other.GetComponentInParent<Rigidbody>().useGravity = false;

                didPickUpParentCube = true;
                pickedUpOnLeftSide = true;

                Debug.Log("1");
            }

            if (!didPickUpChildCube && other.transform.parent.tag == "ChildCube")
            {
                other.transform.parent.parent = this.transform;
                other.transform.parent.parent.GetComponent<stackScript>().isAnotherBoxStacked = false;
                other.transform.parent.tag = "CubeCheese";
                other.transform.parent.parent = pickUpPosition_Right.transform;
                pickedUpMainCube = other.transform.parent.gameObject;
                other.transform.parent.position = pickUpPosition_Right.transform.position;

                other.GetComponentInParent<Rigidbody>().useGravity = false;

                didPickUpChildCube = true;
                pickedUpOnLeftSide = true;

                Debug.Log("1.5");
            }

        }

        if (other.tag == "touchPointRight" && Input.GetKey(KeyCode.X))
        {


            if (!didPickUpParentCube && other.transform.parent.tag == "CubeCheese")
            {
                other.transform.parent.parent = this.transform;
                other.transform.parent.parent = pickUpPosition_Left.transform;
                other.transform.parent.position = pickUpPosition_Left.transform.position;
                pickedUpMainCube = other.transform.parent.gameObject;
                other.GetComponentInParent<Rigidbody>().useGravity = false;

                didPickUpParentCube = true;
                pickedUpOnRightSide = true;

                Debug.Log("2");
            }

            if (!didPickUpChildCube && other.transform.parent.tag == "ChildCube")
            {
                other.transform.parent.parent = this.transform;
                other.transform.parent.parent.GetComponent<stackScript>().isAnotherBoxStacked = false;
                other.transform.parent.tag = "CubeCheese";
                other.transform.parent.parent = pickUpPosition_Left.transform;
                other.transform.parent.position = pickUpPosition_Left.transform.position;
                pickedUpMainCube = other.transform.parent.gameObject;
                other.GetComponentInParent<Rigidbody>().useGravity = false;

                didPickUpChildCube = true;
                pickedUpOnRightSide = true;

                Debug.Log("2.5");
            }

        }
    }

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

        if (Input.GetKeyDown(KeyCode.K))
        {
            Transform[] ks = GetComponentsInChildren<Transform>();
            foreach (Transform t in ks)
            {
                if (t.tag == "CubeCheese")
                {
                    t.transform.parent = null;
                    t.GetComponent<Rigidbody>().useGravity = true;
                    //t.transform.position = pickUpPosition_Right.transform.position;
                    didPickUpChildCube = false;
                    didPickUpParentCube = false;
                    pickedUpOnLeftSide = false;
                    pickedUpOnRightSide = false;
                    Debug.Log("Dropped");
                }

                if (t.tag == "ChildCube")
                {
                    t.transform.parent = null;
                    t.GetComponent<Rigidbody>().useGravity = true;
                    //t.transform.position = pickUpPosition_Right.transform.position;
                    didPickUpChildCube = false;
                    didPickUpParentCube = false;
                    pickedUpOnLeftSide = false;
                    pickedUpOnRightSide = false;
                    Debug.Log("Dropped");
                }
            }
        }

        if (didPickUpChildCube || didPickUpParentCube)
        {

            if (pickedUpOnRightSide)
            {
                if (Vector3.Distance(pickedUpMainCube.transform.position, pickUpPosition_Right.transform.position) > 1.7f)
                {
                    pickedUpMainCube.GetComponent<Rigidbody>().useGravity = true;
                    pickedUpMainCube.transform.parent = null;
                    didPickUpChildCube = false;
                    didPickUpParentCube = false;
                    pickedUpOnLeftSide = false;
                    pickedUpOnRightSide = false;
                }

            }

            if (pickedUpOnLeftSide)
            {
                if (Vector3.Distance(pickedUpMainCube.transform.position, pickUpPosition_Left.transform.position) > 1.17f)
                {
                    pickedUpMainCube.GetComponent<Rigidbody>().useGravity = true;
                    pickedUpMainCube.transform.parent = null;
                    didPickUpChildCube = false;
                    didPickUpParentCube = false;
                    pickedUpOnLeftSide = false;
                    pickedUpOnRightSide = false;
                }

            }

        }

        //if (didPickUp)
        //{
        //    Transform[] ts = GetComponentsInChildren<Transform>();
        //    foreach (Transform t in ts)
        //    {

        //        if (t.tag == "CubeCheese" && Vector3.Distance(t.transform.position, transform.position) > amount)
        //        {
        //            //t.transform.FindChild("CubeCheese").parent = null;
        //            t.transform.parent = null;
        //            t.GetComponent<Rigidbody>().useGravity = true;
        //            //t.transform.position = pickUpPosition_Right.transform.position;
        //            didPickUp = false;
        //            Debug.Log("Dropped");

        //        }

        //    }
        //}


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

        txt.text = string.Format("Power Timer: {00:0}", seconds);
    }
}