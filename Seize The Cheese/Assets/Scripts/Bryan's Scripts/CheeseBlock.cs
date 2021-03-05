using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBlock : MonoBehaviour
{
    public bool pickedUp = false;
    public bool isSecondCheese = false;
    public byte pickedUpDirection = 0;
    public GameObject collidedObject = null;
    public Vector3 tempTransform = Vector3.zero;
    public bool rayHit = false;
    public RaycastHit hit;

    PlayerMechanics player = null;

    private void Awake()
    {
        //reference player's script
        player = GameObject.Find("Player").GetComponent<PlayerMechanics>();
    }
    private void FixedUpdate()
    {
        //only use raycast if cheese is picked up
        if (pickedUp && isSecondCheese == false)
        {
            Debug.Log("FixedUpdate: pickUp is true");

            tempTransform = transform.position;
            tempTransform.y -= 0.5f;

            //right side
            if (pickedUpDirection == 0)
            {
                tempTransform.x += 0.35f;
                Debug.DrawRay(tempTransform, Vector2.right);
                //if raycast is hitting anything
                if (Physics.Raycast(tempTransform, transform.TransformDirection(Vector3.right), out  hit, 0.1f))
                {
                    if (hit.distance < 0.05f)
                    {
                        if (hit.collider.name.CompareTo("Cheese") == 0)
                        {
                            //indicate to reference cheese if not already
                            player.SecondCheeseBlock(hit.collider.gameObject, 0);
                        }
                        
                        rayHit = true;
                        //Debug.Log("Distance = " + hit.distance);
                        GetComponentInParent<PlayerMechanics>().PlayerMovement(4);
                    }
                }
                else
                {
                    rayHit = false;
                    GetComponentInParent<PlayerMechanics>().PlayerMovement(5);
                    //remove cheese from player if not touched
                    if (hit.collider != null && hit.collider.name.CompareTo("Cheese") == 0)
                    {
                        player.SecondCheeseBlock(null, 2);
                    }
                }
            }
            //left side
            else
            {
                tempTransform.x -= 0.35f;
                Debug.DrawRay(tempTransform, Vector2.left);
                //if raycast is hitting anything
                if (Physics.Raycast(tempTransform, transform.TransformDirection(Vector3.left), out  hit, 0.1f))
                {
                    if (hit.distance < 0.05f)
                    {
                        if (hit.collider.name.CompareTo("Cheese") == 0)
                        {
                            //indicate to reference cheese if not already
                            player.SecondCheeseBlock(hit.collider.gameObject, 1);
                        }

                        rayHit = true;
                        //Debug.Log("Distance = " + hit.distance);
                        GetComponentInParent<PlayerMechanics>().PlayerMovement(3);
                    }
                }
                else
                {
                    rayHit = false;
                    GetComponentInParent<PlayerMechanics>().PlayerMovement(5);
                    //remove cheese from player if not touched
                    if (hit.collider != null && hit.collider.name.CompareTo("Cheese") == 0)
                    {
                        player.SecondCheeseBlock(null, 2);
                    }
                }
            }
        }
    }

    public void PickedUp(byte direction, bool isSecondCheese)
    {
        Debug.Log("Cheese: PickUp() called with direction = " + direction + " and isSecondCheese = " + isSecondCheese);
        pickedUp = true;
        this.isSecondCheese = isSecondCheese;
        //place block on right side
        if (direction == 1)
        {
            //if second block
            if (isSecondCheese == true)
            {
                Debug.Log("Second cheese pickUp right side");
                transform.localPosition = new Vector3(1.5f, 2.3f, 0);
                pickedUpDirection = 0;
            }
            else
            {
                Debug.Log("cheese pickUp right side");
                transform.localPosition = new Vector3(1.5f, 0.5f, 0);
                pickedUpDirection = 0;
            }
        }
        //place block on left side
        else if (direction == 2)
        {
            //if second block
            if (isSecondCheese == true)
            {
                Debug.Log("Second cheese pickUp left side");
                transform.localPosition = new Vector3(-1.5f, 2.3f, 0);
                pickedUpDirection = 1;
            }
            else
            {
                Debug.Log("cheese pickUp left side");
                transform.localPosition = new Vector3(-1.5f, 0.5f, 0);
                pickedUpDirection = 1;
            }
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
                //if hit a second cheese block then reference
                if (collision.gameObject.name.CompareTo("Cheese") == 0)
                {
                    //hit second cheese
                    player.SecondCheeseBlock(collision.gameObject, 0);
                }

            }
            //prevent left movement if cheese is hitting object
            else if (direction.x == 1 && pickedUp)
            {
                player.PlayerMovement(1);
                //if hit a second cheese block then reference
                if (collision.gameObject.name.CompareTo("Cheese") == 0)
                {
                    //hit second cheese
                    player.SecondCheeseBlock(collision.gameObject, 1);
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //unreference the collided object
        if(collidedObject != null && pickedUp)
        {
            collidedObject = null;
            player.PlayerMovement(2);
        }
        //remove cheese from player if not touched
        if (player.GetSecondCheeseBlock()!= null)
        {
            player.SecondCheeseBlock(null,2);
        }
    }
}
