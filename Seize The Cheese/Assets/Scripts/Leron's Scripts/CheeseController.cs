using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseController : MonoBehaviour
{
    public bool touchingWall = false;
    public bool didPickUp = false;
    public GameObject player;
    public float amount;

    PlayerMovement playercontroller;
    PlayerMechanics character_Movement;

    private void Start()
    {
        playercontroller = player.GetComponent<PlayerMovement>();
        character_Movement = player.GetComponent<PlayerMechanics>();
    }

    private void Update()
    {
        if (didPickUp)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > amount)
            {
                transform.parent = null; // detaches from the pickUpPosition_Right gameobject
                transform.parent = null; // detaches from the player gameobject

                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<CheeseController>().didPickUp = false;

                character_Movement.didPickUpChildCube = false;
                character_Movement.didPickUpParentCube = false;
                character_Movement.pickedUpOnLeftSide = false;
                character_Movement.pickedUpOnRightSide = false;

                playercontroller.canMoveLeft = true;
                playercontroller.canMoveRight = true;

                Debug.Log("oof2");
            }
        }

    }
}
