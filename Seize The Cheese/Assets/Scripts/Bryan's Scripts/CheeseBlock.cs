using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBlock : MonoBehaviour
{
    public bool pickedUp = false;
    public GameObject connectedCheese = null;
    public GameObject collidedObject = null;

    PlayerMechanics player = null;

    private void Awake()
    {
        //reference player's script
        player = GameObject.Find("Player").GetComponent<PlayerMechanics>();
    }

    public void PickedUp(byte direction)
    {
        pickedUp = true;

        //place block on right side
        if (direction == 1)
        {
            transform.localPosition = new Vector3(1.5f, 0.5f, 0);
        }
        //place block on left side
        else if (direction == 2)
        {
            transform.localPosition = new Vector3(-1.5f, 0.5f, 0);
        }
            return;
    }
    public void Dropped()
    {
        pickedUp = false;
        return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("cheese collision = " + collision.gameObject.name);
        //if it collides with something other then the player then determine where it was hit
        if (collision.gameObject.name.CompareTo("Player") != 0 && pickedUp)
        {
            collidedObject = collision.gameObject;

            //get contacts for collision
            Vector2 direction = collision.GetContact(0).normal;

            //if top or bottom of the cheese is hit then drop if player is holding
            if ((direction.y == 1 || direction.y == -1) && pickedUp)
            {
                player.DropCheese();
            }
            //prevent right movement if cheese is hitting object
            else if (direction.x == -1 && pickedUp)
            {
                player.PlayerMovement(0);
                player.PlayerMovement(3);
            }
            //[revent left movement if cheese is hitting object
            else if (direction.x == 1 && pickedUp)
            {
                player.PlayerMovement(1);
                player.PlayerMovement(3);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (pickedUp && collision.gameObject.name.CompareTo("Player") != 0)
        {
            //move player back if block is in the wall
            gameObject.GetComponentInParent<PlayerMovement>().MoveBack();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //unreference the collided object
        if(collidedObject != null && pickedUp)
        {
            collidedObject = null;
            player.PlayerMovement(2);
            player.PlayerMovement(4);
        }
    }
}
