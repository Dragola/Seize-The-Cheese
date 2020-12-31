using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Wall_Touch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Wall")
        {
            //touchingWall = true;

            //GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero; // DO NOT DELETE THIS - Makes the cube not push off the player when colliding with a wall.

            Transform[] ks = GetComponentsInParent<Transform>();
            foreach (Transform t in ks)
            {
                if (t.tag == "CubeCheese" || t.tag == "ChildCube")
                {
                    CheeseController cheeseController = t.GetComponent<CheeseController>();
                    cheeseController.touchingWall = true;
                }

                if (t.tag == "Player")
                {
                    PlayerController controller = t.GetComponent<PlayerController>();
                    controller.canMoveRight = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Wall")
        {
            //touchingWall = false;

            //GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero; // DO NOT DELETE THIS - Makes the cube not push off the player when colliding with a wall.

            Transform[] ks = GetComponentsInParent<Transform>();
            foreach (Transform t in ks)
            {
                if (t.tag == "CubeCheese" || t.tag == "ChildCube")
                {
                    CheeseController cheeseController = t.GetComponent<CheeseController>();
                    cheeseController.touchingWall = false;
                }

                if (t.tag == "Player")
                {
                    PlayerController controller = t.GetComponent<PlayerController>();
                    controller.canMoveRight = true;
                }
            }
        }   
    }

}
