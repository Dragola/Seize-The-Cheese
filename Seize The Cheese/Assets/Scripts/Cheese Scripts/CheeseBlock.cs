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
    private Rigidbody cheeseRigidbody = null;

    public PlayerMechanics player = null;


    Animator animator;
    private void Awake()
    {
        //reference player's script
        player = GameObject.Find("Mousy").GetComponent<PlayerMechanics>();
        cheeseRigidbody = GetComponent<Rigidbody>();

        animator = player.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        //raycast
        if (pickedUp == true)
        {
            if (cheeseDirection == 0)
            {
                Debug.DrawRay(transform.position, Vector3.right);
                //shoot raycast
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1))
                {
                    Debug.Log("Raycast right side hitting: " + hit.collider.name + ", distance= " + hit.distance);
                    //if the distance from the raycast is less then 0.05
                    if (hit.distance < 0.5f && hit.collider.name.CompareTo("Mousy") != 0)
                    {
                        Debug.Log("Raycast right side hitting distance < 0.36f");
                        //if ray hits cheese block
                        if (hit.collider.tag.CompareTo("CubeCheese") == 0)
                        {
                            //indicate raycast hit
                            rayHit = true;
                            player.PreventPlayerMovement(0);
                            //if this block is the first cheese block then indicate this is second cheese block
                            if (pickedUp)
                            {
                                Debug.Log("Second cheese hit");
                                //indicate to reference cheese if not already
                                player.CheeseBlockHit(hit.collider.gameObject, 0);
                            }
                        }
                    }
                }
                else
                {
                    //indicate raycast not hitting object on right
                    rayHit = false;
                    player.UnPreventPlayerMovement(1);

                    //remove cheese from player if not touched
                    if (hit.collider != null && hit.collider.tag.CompareTo("CubeCheese") == 0)
                    {
                        player.CheeseBlockHit(null, 1);
                    }
                }
            }
            else if (cheeseDirection == 1)
            {
                Debug.DrawRay(transform.position, Vector3.left);
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1))
                {
                    Debug.Log("Raycast left side hitting: " + hit.collider.name + ", distance= " + hit.distance);
                    //if the distance from the raycast is less then 0.05
                    if (hit.distance < 0.5f && hit.collider.name.CompareTo("Mousy") != 0)
                    {
                        Debug.Log("Raycast left side hitting distance < 0.36f");
                        //indicate raycast hit
                        rayHit = true;
                        player.PreventPlayerMovement(1);
                        //if ray hits cheese block
                        if (hit.collider.tag.CompareTo("CubeCheese") == 0)
                        {
                            //if this block is the first cheese block then indicate this is second cheese block
                            if (pickedUp)
                            {
                                Debug.Log("Second cheese hit");
                                //indicate to reference cheese if not already
                                player.CheeseBlockHit(hit.collider.gameObject, 0);
                            }
                        }
                    } 
                }
                else
                {
                    //indicate raycast not hitting object on right
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
        //remove rigidbody from cheese block
        Destroy(GetComponent<Rigidbody>());

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

                cheeseRigidbody.MovePosition(new Vector3(0, 1.4f, 0.45f));
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
                cheeseRigidbody.MovePosition(new Vector3(0, 1.4f, 0.45f));
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
        cheeseRigidbody.MovePosition(new Vector3(0, 0.4f, 0.45f));
        cheeseDirection = 0;
    }
    void PickupCheeseLeft()
    {
        //Debug.Log("cheese pickUp left side");
        cheeseRigidbody.MovePosition(new Vector3(0, 0.4f, 0.45f));
        cheeseDirection = 1;
    }
    public void Dropped()
    {
        pickedUp = false;
        isSecondCheese = false;
        return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            Debug.Log("CheeseBlock: OnCollisionStay");
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

                    //remove rigidbody from cheese block
                    gameObject.AddComponent<Rigidbody>();

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
