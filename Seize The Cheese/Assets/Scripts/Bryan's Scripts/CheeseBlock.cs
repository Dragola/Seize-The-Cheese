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

    public void PickedUp()
    {
        pickedUp = true;
        return;
    }
    public void Dropped()
    {
        pickedUp = false;
        return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("cheese collision = " + collision.gameObject.name);
        //if it collides with something other then the player
        if (collision.gameObject.name.CompareTo("Player") != 0)
        {
            collidedObject = collision.gameObject;

            //get contacts for collision
            Vector2 direction = collision.GetContact(0).normal;

            if (direction.y == 1 && pickedUp)
            {
                player.DropCheese();

            }
            //right
            else if (direction.x == -1 && pickedUp)
            {
                player.PlayerMovement(0);
            }
            //left
            else if (direction.x == 1 && pickedUp)
            {
                player.PlayerMovement(1);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //unreference the collided object
        if(collidedObject != null)
        {
            player.PlayerMovement(2);
            player.PlayerMovement(3);
            collidedObject = null;
        }
        
        if (collision.gameObject.name.CompareTo("Player") != 0)
        {
            //if there is a contact point
            if (collision.contactCount > 1)
            {
                //get contacts for collision
                Vector2 direction = collision.GetContact(0).normal;

                //right
                if (direction.x == -1 && pickedUp)
                {
                    player.PlayerMovement(2);
                }
                //left
                else if (direction.x == 1 && pickedUp)
                {
                    player.PlayerMovement(3);
                }
            }
        }
    }
}
