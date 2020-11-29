using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackScript : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger)
        {
            other.transform.parent = transform.parent;
            player.GetComponent<Character_Movement>().didPickUp = false;
            other.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log("On top Of Cheese");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUp == false)
        {
            other.transform.parent = null;
            other.GetComponent<Rigidbody>().useGravity = true;
            Debug.Log("Not On top Of Cheese");
        }

        if (other.tag == "CubeCheese" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUp == true)
        {
            other.transform.parent = this.transform.parent;
            other.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log("Not On top Of Cheese");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
