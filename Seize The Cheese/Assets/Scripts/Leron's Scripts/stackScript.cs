using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackScript : MonoBehaviour
{
    public GameObject player;
    public GameObject stackedCube;

    public float amount;
    public bool isAnotherBoxStacked = false;
    public bool didHitSomething = false;

    //once cube is stacked ontop of another cube it detaches from the player and becomes child of bottom cube.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger && !isAnotherBoxStacked)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.parent = transform.parent; // stacked cube becomes child of this cube and gets detached from player.
            other.tag = "ChildCube";
            player.GetComponent<Character_Movement>().didPickUpChildCube = false; //didPickUpChildCube gets set to false as player doesnt have a cube in hand.
            player.GetComponent<Character_Movement>().didPickUpParentCube = false; //didPickUpParentCube gets set to false as player doesnt have a cube in hand.
            player.GetComponent<Character_Movement>().pickedUpOnRightSide = false;//pickedUpOnRightSide gets set to false as player doesnt have a cube in hand.
            player.GetComponent<Character_Movement>().pickedUpOnLeftSide = false;//pickedUpOnLeftSide gets set to false as player doesnt have a cube in hand.
            other.GetComponent<Rigidbody>().useGravity = false; // gravity gets set to false because the cube is now ontop of this cube and thus wont move when jumping.
            stackedCube = other.gameObject;
            isAnotherBoxStacked = true; //sets isAnotherBoxStacked true
            Debug.Log("5");

        }
    }



    private void OnTriggerExit(Collider other)
    {
        //when top cube gets hit off by obstacle while on top of another cube and the player IS NOT holding a cube(s).
        if (other.tag == "ChildCube" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUpParentCube == false)
        {
            other.transform.parent = null; // stacked cube's parent (which is this object) become null thus detaching the stacked cube from the current cube (which is its parent).
            other.tag = "CubeCheese";
            other.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
            isAnotherBoxStacked = false;
            Debug.Log("6");
        }

        //when any stacked cube gets hit off by obstacle while the player IS holding onto the most bottom cube.
        if (other.tag == "ChildCube" && !other.isTrigger && player.GetComponent<Character_Movement>().didPickUpParentCube == true)
        {
            other.transform.parent = null;
            other.tag = "CubeCheese";
            other.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
            isAnotherBoxStacked = false; ;

            Debug.Log("7");
        }


    }

    //private void Update()
    //{
    //    if (transform.parent.tag == "ChildCube" && transform.parent.parent == null)
    //    {
    //        transform.parent.tag = "CubeCheese";

    //    }
    //}

    //private void Update()
    //{

    //    if (isAnotherBoxStacked && stackedCube.transform.hasChanged)
    //    {
    //        //when top cube gets hit off by obstacle while on top of another cube and the player IS NOT holding a cube(s).
    //        if (player.GetComponent<Character_Movement>().didPickUpChildCube == false && player.GetComponent<Character_Movement>().didPickUpParentCube == false) //didPickUp gets set to false as player doesnt have a cube in hand.
    //        {
    //            if ((Vector3.Distance(stackedCube.transform.position, transform.position) > amount))
    //            {
    //                stackedCube.transform.parent = null; // stacked cube's parent (which is this object) become null thus detaching the stacked cube from the current cube (which is its parent).
    //                stackedCube.tag = "CubeCheese";
    //                stackedCube.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
    //                isAnotherBoxStacked = false;
    //                Debug.Log("4");

    //            }
    //        }

    //        //when any stacked cube gets hit off by obstacle while the player IS holding onto the most bottom cube.
    //        if (player.GetComponent<Character_Movement>().didPickUpChildCube == false && player.GetComponent<Character_Movement>().didPickUpParentCube == true) //didPickUp gets set to false as player doesnt have a cube in hand.
    //        {
    //            if ((Vector3.Distance(stackedCube.transform.position, transform.position) > amount))
    //            {
    //                stackedCube.transform.parent = null; // stacked cube's parent (which is this object) become null thus detaching the stacked cube from the current cube (which is its parent).
    //                stackedCube.tag = "CubeCheese";
    //                stackedCube.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
    //                isAnotherBoxStacked = false;
    //                Debug.Log("4");

    //            }
    //        }



    //    }
    //}


}
