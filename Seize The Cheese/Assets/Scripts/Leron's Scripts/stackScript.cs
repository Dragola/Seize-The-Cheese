using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackScript : MonoBehaviour
{
    public GameObject player;
    public int aamount = 1;
    public bool isAnotherBoxStacked = false;

    //once cube is stacked ontop of another cube it detaches from the player and becomes child of bottom cube.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger && !isAnotherBoxStacked) 
        {
            other.transform.parent = transform.parent; // stacked cube becomes child of this cube and gets detached from player.
            player.GetComponent<Character_Movement>().didPickUp = false; //didPickUp gets set to false as player doesnt have a cube in hand.
            player.GetComponent<Character_Movement>().pickedUpOnRightSide = false;//pickedUpOnRightSide gets set to false as player doesnt have a cube in hand.
            player.GetComponent<Character_Movement>().pickedUpOnLeftSide = false;//pickedUpOnLeftSide gets set to false as player doesnt have a cube in hand.
            other.GetComponent<Rigidbody>().useGravity = false; // gravity gets set to false because the cube is now ontop of this cube and thus wont move when jumping.
            isAnotherBoxStacked = true; //sets isAnotherBoxStacked true
            Debug.Log("3");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //when top cube gets hit off by obstacle while on top of another cube.
        if (other.tag == "CubeCheese" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUp == false) 
        {
           other.transform.parent = null; // stacked cube's parent (which is this object) become null thus detaching the stacked cube from the current cube (which is its parent).
           other.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
           isAnotherBoxStacked = false; 
           Debug.Log("4");
        }

        // allows player to pick up stacked cube
        if (other.tag == "CubeCheese" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUp == true) 
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            isAnotherBoxStacked = false;

            Debug.Log("5");
        }
    }

}
