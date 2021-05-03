using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerper : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 currentPoint;

    public float speed = 1f;
    public bool canMove = true;
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
        {
            animator_.SetBool("isWalking", true);
            if (speed > 0)
            {
                facing_dir_ = 1;
            }
            else if (speed < 0)
            {
                facing_dir_ = -1;
            }
            speed = Mathf.Abs(speed);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, facing_dir_ * Mathf.Abs(transform.localScale.z));
        }
        else
        {
            animator_.SetBool("isIdle", true);
        }
    }

    void Update()
    {
        float prev_facing_dir = facing_dir_; // save previous facing dir before updating

        if (canMove)
        {
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
            }

            if ((animator_.GetCurrentAnimatorStateInfo(0).IsName("DBTurnAnimation") || 
                animator_.GetCurrentAnimatorStateInfo(0).IsName("DBTurnAnimation_Reverse")) &&
                animator_.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
                animator_.GetBool("isTurning"))
            {
                animator_.SetBool("isTurning", false);
                can_update_speed_ = true; 
                //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1 * transform.localScale.z);
            }
        }

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

    }


    //private void Movement()
    //{
    //    //change speed for left direction
    //    if (updateSpeed == true && canMoveRight == false)
    //    {
    //        updateSpeed = false;
    //    }
    //    //change speed for right direction
    //    else if (canMoveRight == true && updateSpeed == true)
    //    {
    //        updateSpeed = false;
    //    }
    //    //rotate bunny
    //    if (updateRotation)
    //    {
    //        animator_.SetBool("isTurning", true);
    //        if (animator_.GetCurrentAnimatorStateInfo(0).IsName("DBTurnAnimation") &&
    //            animator_.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
    //        {
    //            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //            //rotate to face the right direction
    //            if (canMoveRight)
    //            {
    //                //transform.eulerAngles = new Vector3(0, 90, 0);
    //                updateRotation = false;
    //            }
    //            //rotate to face the left direction
    //            else if (canMoveRight == false)
    //            {
    //                //transform.eulerAngles = new Vector3(0, 270, 0);
    //                updateRotation = false;
    //            }
    //            animator_.SetBool("isTurning", false);
    //        }
    //    }
    //    if(!updateRotation)
    //    {
    //        //move dust bunny
    //        transform.Translate(0, 0, facing_dir_ * speed * Time.deltaTime);
    //    }

    //}
    public void SetMovement(bool enabled)
    {
        canMove = enabled;
    }

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
    {
        Gizmos.DrawWireSphere(transform.position, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(startPoint.x,
            transform.position.y + 0.5f,
            transform.position.z),
            new Vector3(endPoint.x,
            transform.position.y + 0.5f,
            transform.position.z));

    }
}
