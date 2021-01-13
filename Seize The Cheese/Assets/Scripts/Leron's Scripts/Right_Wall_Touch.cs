using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Wall_Touch : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Wall")
        {
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
                    PlayerMovement controller = t.GetComponent<PlayerMovement>();
                    controller.canMoveRight = false;
                    controller.moveDirection *= 0;
                    controller.jumpVelocity.x *= 0;
                    controller.jumpVelocity.y -= controller.gravity * Time.deltaTime;
                    Debug.Log("Touched Wall While Jumping");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Wall")
        {
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero; // DO NOT DELETE THIS - Makes the cube not push off the player when colliding with a wall.
            //transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

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
                    PlayerMovement controller = t.GetComponent<PlayerMovement>();
                    controller.canMoveRight = true;
                }
            }
        }
    }
}
