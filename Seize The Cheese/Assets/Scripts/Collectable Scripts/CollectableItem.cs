using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private GameObject collectableUI = null;
    private PauseMenu pauseMenu = null;
    private bool popupon = false;

    private void Awake()
    {
        collectableUI = GameObject.Find("Collectable UI");
        collectableUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //only run if the collider is the player (prevent enemy or other from triggering)
        if (other.gameObject.name.CompareTo("Mousy") == 0)
        {
            //signal to parent script that collectable was grabbed
            other.GetComponent<CollectableCounter>().addCollectable();

            //Collectable popup
            collectableUI.SetActive(true);
            popupon = true;

            //pause game
            Time.timeScale = 0;
        }
    }
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Return) && popupon == true)
        {
            //resume game
            Time.timeScale = 1;
            Debug.Log("ENTER key pressed!");
            collectableUI.SetActive(false);

            //destroy collectable object
            Destroy(this.gameObject);
        }
    }
}
