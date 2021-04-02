using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBlock : MonoBehaviour
{
    public bool pickedUp = false;
    public bool isSecondCheese = false;
    public byte cheeseDirection = 0;
    public GameObject collidedObject = null;
    public Vector3 tempTransform = Vector3.zero;
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
    private void FixedUpdate()
    {
        //right side raycast
        if (pickedUp == true)
        {
            tempTransform.y = transform.position.y - 0.05f;
            tempTransform.z = transform.position.z;
            Debug.Log("FixedUpdate- pickedUp == true");

            if (cheeseDirection == 0)
            {
                tempTransform.x = transform.position.x + 0.22f;
                Debug.DrawRay(tempTransform, Vector3.right);
                //shoot raycast
                if (Physics.Raycast(tempTransform, transform.TransformDirection(Vector3.right), out hit, 0.12f))
                {
                    //if the distance from the raycast is less then 0.05
                    if (hit.distance < 0.05f)
                    {
                        //if ray hits cheese blockf
                        if (hit.collider.tag.CompareTo("CubeCheese") == 0)
                        {
                            //if this block is the first cheese block then indicate this is second cheese block
                            if (pickedUp)
                            {
                                Debug.Log("Second cheese hit");
                                //indicate to reference cheese if not already
                                player.CheeseBlockHit(hit.collider.gameObject, 0);
                            }
                            //if this is the first block hit
                            else if (pickedUp == false && player.cheeseBlock == null)
                            {
                                player.cheeseBlock = hit.collider.gameObject;
                            }
                        }

                        //indicate raycast hit object on right
                        rayHit = true;
                        player.PreventPlayerMovement(0);
                    }
                }
                else
                {
                    //indicate raycast not hitting object on right
                    rayHit = false;
                    player.UnPreventPlayerMovement(0);

                    //remove cheese from player if not touched
                    if (hit.collider != null && hit.collider.tag.CompareTo("CubeCheese") == 0)
                    {
                        player.CheeseBlockHit(null, 1);
                    }
                }
            }
            //left side raycast
            else if (cheeseDirection == 1)
            {
                tempTransform.x = transform.position.x - 0.22f;
                Debug.DrawRay(tempTransform, Vector3.left);
                //shoot raycast
                if (Physics.Raycast(tempTransform, transform.TransformDirection(Vector3.left), out hit, 0.12f))
                {
                    //if the distance from the raycast is less then 0.05
                    if (hit.distance < 0.05f)
                    {
                        //if ray hits cheese block
                        if (hit.collider.tag.CompareTo("CubeCheese") == 0)
                        {
                            //if this block is the first cheese block then indicate this is second cheese block
                            if (pickedUp)
                            {
                                Debug.Log("Second cheese hit left side");
                                //indicate to reference cheese if not already
                                player.CheeseBlockHit(hit.collider.gameObject, 0);
                            }
                            //if this is the first block hit
                            else if (pickedUp == false && player.cheeseBlock == null)
                            {
                                player.cheeseBlock = hit.collider.gameObject;
                            }
                        }
                        //indicate raycast hit object on left
                        rayHit = true;
                        player.PreventPlayerMovement(1);
                    }
                }
                else
                {
                    //indicate raycast not hitting object on left
                    rayHit = false;
                    player.UnPreventPlayerMovement(1);

                    //remove cheese from player if not touched
                    if (hit.collider != null && hit.collider.tag.CompareTo("CubeCheese") == 0)
                    {
                        player.CheeseBlockHit(null, 1);
                    }
                }
            }
        }
    }

    public void PickedUp(byte direction, bool isSecondCheese)
    {
        Debug.Log("PickedUp() called with direction = " + direction);
        //Debug.Log("Cheese: PickUp() called with direction = " + direction + " and isSecondCheese = " + isSecondCheese);
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
                transform.localPosition = new Vector3(0, 1.4f, 0.45f);
                cheeseDirection = 0;
            }
            else
            {
                //Debug.Log("cheese pickUp right side");
                transform.localPosition = new Vector3(0, 0.4f, 0.45f);
                cheeseDirection = 0;
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
                transform.localPosition = new Vector3(0, 1.4f, 0.45f);
                cheeseDirection = 1;
            }
            else
            {
                //Debug.Log("cheese pickUp left side");
                transform.localPosition = new Vector3(0, 0.4f, 0.45f);
                cheeseDirection = 1;
            }
        }
        return;
    }
    public void Dropped()
    {
        pickedUp = false;
        isSecondCheese = false;
        return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if it collides with something other then the player then determine where it was hit
        if (collision.gameObject.name.CompareTo("Mousy") != 0)
        {
            collidedObject = collision.gameObject;

            //get contacts for collision
            Vector2 direction = collision.GetContact(0).normal;

            //if top or bottom of the cheese is hit then drop if player is holding
            if ((direction.y == 1 || direction.y == -1) && pickedUp)
            {
                player.DropCheese();

                //stops animation
                animator.SetBool("isholdingcheese", false);
            }
            //prevent right movement if cheese is hitting object
            else if (direction.x == -1 && pickedUp)
            {
                //if hit a second cheese block then reference
                if (collision.gameObject.tag.CompareTo("CubeCheese") == 0)
                {
                    //hit second cheese
                    player.CheeseBlockHit(collision.gameObject,0);
                }
                player.PreventPlayerMovement(0);
            }
            //prevent left movement if cheese is hitting object
            else if (direction.x == 1 && pickedUp)
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
    private void OnCollisionExit(Collision collision)
    {
        //unreference the collided object
        if(collidedObject != null)
        {
            collidedObject = null;
        }
        player.UnPreventPlayerMovement(0);
        player.UnPreventPlayerMovement(1);
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
                transform.localPosition = new Vector3(0, 1.4f, 0.45f);
                cheeseDirection = 0;
            }
            else
            {
                //Debug.Log("cheese pickUp right side");
                transform.localPosition = new Vector3(0, 0.4f, 0.45f);
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
                transform.localPosition = new Vector3(0, 1.4f, 0.45f);
                cheeseDirection = 1;
            }
            else
            {
                //Debug.Log("cheese pickUp left side");
                transform.localPosition = new Vector3(0, 0.4f, 0.45f);
                cheeseDirection = 1;
            }
        }
    }
}
