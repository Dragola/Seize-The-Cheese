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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CubeCheese" && !other.isTrigger || other.tag == "ChildCube" && !other.isTrigger)
        {

            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


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
