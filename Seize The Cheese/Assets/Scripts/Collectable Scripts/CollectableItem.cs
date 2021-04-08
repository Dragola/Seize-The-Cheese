using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public GameObject CollectablePanel;
    private bool popupon = false;
    private void OnTriggerEnter(Collider other)
    {
        //only run if the collider is the player (prevent enemy or other from triggering)
        if (other.gameObject.name.CompareTo("Mousy") == 0)
        {
            //signal to parent script that collectable was grabbed
            other.GetComponent<CollectableCounter>().addCollectable();

            //Collectable popup
            //CollectablePanel.gameObject.SetActive(true);
            //popupon = true;
            

            //destroy collectable object
            Destroy(this.gameObject);

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && popupon == true)
        {
            Debug.Log("ENTER key pressed!");
            CollectablePanel.gameObject.SetActive(false);
        }
    }
}
