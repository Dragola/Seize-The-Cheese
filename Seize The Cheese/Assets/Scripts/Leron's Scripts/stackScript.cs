using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackScript : MonoBehaviour
{
    public GameObject player;
    public GameObject stackedCube;

    PlayerMovement playercontroller;
    PlayerMechanics character_Movement;

    public float amount;
    public bool isAnotherBoxStacked = false;
    public bool didHitSomething = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger || other.tag == "ChildCube" && !other.isTrigger)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; //freezes all Rigidbody Constraints on child cube.
        }

    }
    //once cube is stacked ontop of another cube it detaches from the player and becomes child of bottom cube.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger || other.tag == "ChildCube" && !other.isTrigger)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            other.GetComponent<CheeseController>().didPickUp = false;
            other.transform.parent = transform.parent; // stacked cube becomes child of this cube and gets detached from player.
            other.tag = "ChildCube";
            character_Movement.didPickUpChildCube = false; //didPickUpChildCube gets set to false as player doesnt have a cube in hand.
            character_Movement.didPickUpParentCube = false; //didPickUpParentCube gets set to false as player doesnt have a cube in hand.
            character_Movement.pickedUpOnRightSide = false;//pickedUpOnRightSide gets set to false as player doesnt have a cube in hand.
            character_Movement.pickedUpOnLeftSide = false;//pickedUpOnLeftSide gets set to false as player doesnt have a cube in hand.
            other.GetComponent<Rigidbody>().useGravity = false; // gravity gets set to false because the cube is now ontop of this cube and thus wont move when jumping.
            stackedCube = other.gameObject;
            isAnotherBoxStacked = true; //sets isAnotherBoxStacked true
            //Debug.Log("5");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //when top cube gets hit off by obstacle while on top of another cube and the player IS NOT holding a cube(s).
        if (other.tag == "ChildCube" && !other.isTrigger && character_Movement.didPickUpParentCube == false)
        {
            other.transform.parent = null; // stacked cube's parent (which is this object) become null thus detaching the stacked cube from the current cube (which is its parent).
            other.tag = "CubeCheese";
            other.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
            isAnotherBoxStacked = false;
            //Debug.Log("6");
        }

        //when any stacked cube gets hit off by obstacle while the player IS holding onto the most bottom cube.
        if (other.tag == "ChildCube" && !other.isTrigger && character_Movement.didPickUpParentCube == true)
        {
            other.transform.parent = null;
            other.tag = "CubeCheese";
            other.GetComponent<Rigidbody>().useGravity = true; // enables gravity so that cube may fall.
            isAnotherBoxStacked = false; ;
            //Debug.Log("7");
        }


    }

    private void Start()
    {
        character_Movement = player.GetComponent<PlayerMechanics>();
    }

}