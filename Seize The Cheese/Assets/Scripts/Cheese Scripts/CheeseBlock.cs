using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBlock : MonoBehaviour
{
    public bool pickedUp = false;
    public bool isSecondCheese = false;
    public byte cheeseDirection = 0;
    public GameObject collidedObject = null;
    public bool rayHit = false;
    public RaycastHit hit;
    public PlayerMechanics player = null;


    Animator animator;
    private void Awake()
    {
        //reference player's script
        player = GameObject.Find("Mousy").GetComponent<PlayerMechanics>();

        animator = player.GetComponent<Animator>();
    }
    public void PickedUp(byte direction, bool isSecondCheese)
    {
        //remove rigidbody from cheese block
        GetComponent<Rigidbody>().useGravity = false;

        Debug.Log("PickedUp() called with direction = " + direction);
  
        pickedUp = true;
        this.isSecondCheese = isSecondCheese;
        //place block on right side
        if (direction == 1)
        {
            Debug.Log("PickedUp() where direction = 1");
            //if second block
            if (isSecondCheese == true)
            {
                //Debug.Log("Second cheese pickUp right side");
                cheeseDirection = 0;
            }
            else
            {
                Invoke("PickupCheeseRight", 1);
            }
        }
        //place block on left side
        else if (direction == 2)
        {
            Debug.Log("PickedUp() where direction = 2");
            //if second block
            if (isSecondCheese == true)
            {
                //Debug.Log("Second cheese pickUp left side");
                cheeseDirection = 1;
            }
            else
            {
                Invoke("PickupCheeseLeft", 1);
            }
        }
        return;
    }

    //delay pickup to match animation
    void PickupCheeseRight()
    {
        //Debug.Log("cheese pickUp right side");
        cheeseDirection = 0;
    }
    void PickupCheeseLeft()
    {
        //Debug.Log("cheese pickUp left side");
        cheeseDirection = 1;
    }
    public void Dropped()
    {
        pickedUp = false;
        isSecondCheese = false;
        GetComponent<Rigidbody>().useGravity = true;
        return;
    }
    private void OnCollisionStay(Collision collision)
    {
        //get contacts for collision
        Vector2 direction = collision.GetContact(0).normal;

        if (pickedUp)
        {
            Debug.Log("CheeseBlock: OnCollisionStay= " + collision.collider.name);
            //if it collides with something other then the player then determine where it was hit
            if (collision.gameObject.name.CompareTo("Mousy") != 0)
            {
                collidedObject = collision.gameObject;

                //if top or bottom of the cheese is hit then drop if player is holding
                if ((direction.y == 1 || direction.y == -1) && pickedUp)
                {
                    Debug.Log("CheeseBlock: OnCollisionStay dropping because " + collision.collider.name + "is below or above block");
                    player.DropCheese();

                    //stops animation
                    animator.SetBool("isholdingcheese", false);
                }
                //prevent right movement if cheese is hitting object
                else if (transform.position.x < collidedObject.transform.position.x && pickedUp)
                {
                    //if hit a second cheese block then reference
                    if (collision.gameObject.tag.CompareTo("CubeCheese") == 0)
                    {
                        //hit second cheese
                        player.CheeseBlockHit(collision.gameObject, 0);
                    }
                    player.PreventPlayerMovement(0);
                }
                //prevent left movement if cheese is hitting object
                else if (transform.position.x > collidedObject.transform.position.x && pickedUp)
                {
                    //if hit a second cheese block then reference
                    if (collision.gameObject.tag.CompareTo("CubeCheese") == 0)
                    {
                        //hit second cheese
                        player.CheeseBlockHit(collision.gameObject, 0);
                    }
                    player.PreventPlayerMovement(1);
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (pickedUp)
        {
            //unreference the collided object
            if (collidedObject != null)
            {
                collidedObject = null;
            }
            player.UnPreventPlayerMovement(0);
            player.UnPreventPlayerMovement(1);
        }
    }
    public void UpdateCheeseDirection(bool isFacingRight)
    {
        if (isFacingRight)
        {
            cheeseDirection = 0;
            //if second block
            if (isSecondCheese == true)
            {
                //Debug.Log("Second cheese pickUp right side");
                cheeseDirection = 0;
            }
            else
            {
                //Debug.Log("cheese pickUp right side");
                cheeseDirection = 0;
            }
        }
        else
        {
            cheeseDirection = 1;
            //if second block
            if (isSecondCheese == true)
            {
                //Debug.Log("Second cheese pickUp left side");
                cheeseDirection = 1;
            }
            else
            {
                //Debug.Log("cheese pickUp left side");
                cheeseDirection = 1;
            }
        }
    }
}
