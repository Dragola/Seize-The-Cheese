using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerper : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 currentPoint;

    public float speed = -1f;
    public bool canMove = true;
<<<<<<< HEAD
    //public bool canMoveRight = true;
    //public bool updateSpeed = false;
    //public bool updateRotation = false;
    [SerializeField]
    private int facing_dir_ = -1; // 1: facing right, -1: facing left
    private bool can_update_speed_ = true;
    private bool has_hit_obstable_ = false;
    private bool has_triggered_death_ = false;
    private Animator animator_;
    private Rigidbody rb_;
    private Collider collider_;

    private void Awake()
    {
        animator_ = transform.GetChild(0).GetComponent<Animator>();
        rb_ = GetComponent<Rigidbody>();
        collider_ = GetComponent<CapsuleCollider>();
        if (canMove)
=======
    public bool canMoveRight = true;
    public bool updateSpeed = false;
    public bool updateRotation = false;
    
    void Update()
    {
        //get current position of the bunny
        currentPoint = transform.position;

        //if the bunny has hit or gone past the startPoint.x
        if(transform.position.x >= startPoint.x)
>>>>>>> parent of 2d7ea6f (finished bunny anim)
        {
            //Debug.Log("Hit startPoint" + name);
            canMoveRight = false;
            updateSpeed = true;
            updateRotation = true;
        }
        //if the bunny has hit or gone past the endPoint.x
        else if (transform.position.x <= endPoint.x)
        {
            //Debug.Log("Hit endPoint" + name);
            canMoveRight = true;
            updateSpeed = true;
            updateRotation = true;
        }

        //only call movement function if bunny can move
        if (canMove)
        {
<<<<<<< HEAD
            if (can_update_speed_)
            {
                rb_.velocity = new Vector3(facing_dir_ * speed, rb_.velocity.y, 0);
            }

            if (facing_dir_ == -1) //facing left
            {
                animator_.SetBool("is_facing_right", false);
                if (transform.position.x <= endPoint.x || has_hit_obstable_)
                {
                    facing_dir_ = 1; //facing right
                    animator_.SetBool("isTurning", true);
                    can_update_speed_ = false;
                    has_hit_obstable_ = false;
                }
            }
            else
            {
                animator_.SetBool("is_facing_right", true);
                if (transform.position.x >= startPoint.x || has_hit_obstable_)
                {
                    facing_dir_ = -1; //facing left
                    animator_.SetBool("isTurning", true);
                    can_update_speed_ = false;
                    has_hit_obstable_ = false;
                }
=======
            Movement();
        }
        
    }
    private void Movement()
    {
        //change speed for left direction
        if (updateSpeed == true && canMoveRight == false)
        {
            updateSpeed = false;
        }
        //change speed for right direction
        else if (canMoveRight == true && updateSpeed == true)
        {
            updateSpeed = false;
        }
        //rotate bunny
        if (updateRotation)
        {
            //rotate to face the right direction
            if (canMoveRight)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                updateRotation = false;
>>>>>>> parent of 2d7ea6f (finished bunny anim)
            }
            //rotate to face the left direction
            else if (canMoveRight == false)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                updateRotation = false;
            }
        }
<<<<<<< HEAD

        if (animator_.GetCurrentAnimatorStateInfo(0).IsName("DBDeathAnimation") )
        {
            if (has_triggered_death_)
            {
                animator_.SetBool("has_triggered_death", true);
            }
            if (animator_.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                gameObject.SetActive(false);
            }
        }



        ////get current position of the bunny
        //currentPoint = transform.position;

        ////if the bunny has hit or gone past the startPoint.x
        //if(transform.position.x >= startPoint.x)
        //{
        //    Debug.Log("Hit startPoint" + name);
        //    canMoveRight = false;
        //    updateSpeed = true;
        //    updateRotation = true;
        //    facing_dir_ = 1;
        //}
        ////if the bunny has hit or gone past the endPoint.x
        //else if (transform.position.x <= endPoint.x)
        //{
        //    Debug.Log("Hit endPoint" + name);
        //    canMoveRight = true;
        //    updateSpeed = true;
        //    updateRotation = true;
        //    facing_dir_ = -1;
        //}


        ////only call movement function if bunny can move
        //if (canMove)
        //{
        //    Movement();
        //}

=======
        //move dust bunny
        transform.Translate(0, 0, speed * Time.deltaTime);
>>>>>>> parent of 2d7ea6f (finished bunny anim)
    }
    public void SetMovement(bool enabled)
    {
        canMoveRight = enabled;
    }
<<<<<<< HEAD

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CubeCheese") || collision.gameObject.CompareTag("HealthCheese"))
        {
            has_hit_obstable_ = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!has_triggered_death_)
            {
                animator_.SetBool("isDying", true);
                canMove = false;
                has_triggered_death_ = true;
                rb_.isKinematic = true;
                collider_.enabled = false;
            }
            
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Dust Bunny(" + gameObject.name + "): Collision detected");

    //    if (other.name.CompareTo("Cheese") == 0)
    //    {
    //        //Left side hit
    //        if (gameObject.transform.position.x > other.gameObject.transform.position.x)
    //        {
    //            Debug.Log("Dust Bunny(" + gameObject.name + "): Hit left side");
    //            canMoveRight = true;
    //            updateSpeed = true;
    //            updateRotation = true;
    //        }
    //        //Right side hit
    //        if (gameObject.transform.position.x < other.gameObject.transform.position.x)
    //        {
    //            Debug.Log("Dust Bunny(" + gameObject.name + "): Hit right side");
    //            canMoveRight = false;
    //            updateSpeed = true;
    //            updateRotation = true;
    //        }
    //    }
    //}

    private void OnDrawGizmos()
=======
    private void OnTriggerEnter(Collider other)
>>>>>>> parent of 2d7ea6f (finished bunny anim)
    {
        Debug.Log("Dust Bunny(" + gameObject.name + "): Collision detected");

        if (other.name.CompareTo("Cheese") == 0)
        {
            //Left side hit
            if (gameObject.transform.position.x > other.gameObject.transform.position.x)
            {
                Debug.Log("Dust Bunny(" + gameObject.name + "): Hit left side");
                canMoveRight = true;
                updateSpeed = true;
                updateRotation = true;
            }
            //Right side hit
            if (gameObject.transform.position.x < other.gameObject.transform.position.x)
            {
                Debug.Log("Dust Bunny(" + gameObject.name + "): Hit right side");
                canMoveRight = false;
                updateSpeed = true;
                updateRotation = true;
            }
        }
    }
}
