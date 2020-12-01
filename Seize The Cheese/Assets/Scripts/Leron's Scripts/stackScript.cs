using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackScript : MonoBehaviour
{
    public GameObject player;
    public int aamount = 1;
    public bool didLeave = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger && !didLeave)
        {
            other.transform.parent = transform.parent;
            player.GetComponent<Character_Movement>().didPickUp = false;
            player.GetComponent<Character_Movement>().pickedUpOnRightSide = false;
            player.GetComponent<Character_Movement>().pickedUpOnLeftSide = false;
            other.GetComponent<Rigidbody>().useGravity = false;
            didLeave = true;
            Debug.Log("3");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //when cube gets hit off by obstacle while on top of another cube.
        if (other.tag == "CubeCheese" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUp == false) 
        {
           other.transform.parent = null; // parent become null thus detaching from the cube current cube is stacked on
           other.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
           didLeave = false; 
           Debug.Log("4");
        }

        // allows player to pick up stacked cube
        if (other.tag == "CubeCheese" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUp == true) 
        {
            other.GetComponent<Rigidbody>().useGravity = false; 
            didLeave = false;

            Debug.Log("5");
        }
    }

}
