using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KH_PlayerController : MonoBehaviour
{
    private float move_speed = 10.0f;
    private float max_move_speed = 100.0f; //not used
    private float jump_force = 20.0f; //from https://youtu.be/vdOFUFMiPDU (How To Jump in Unity - Unity Jumping Tutorial | Make Your Characters Jump in Unity)
    private float fall_multiplier = 2.5f; //from https://youtu.be/7KiK0Aqtmzc (Better Jumping in Unity With Four Lines of Code)
    private float low_jump_multiplier = 2.0f;
    private int facing_dir = 1; // 1: facing right, -1: facing left
    private bool is_carrying = false; // is carrying obj or not
    private float carrying_obj_offset = 1.5f; // offset obj from player pos

    private Rigidbody rb;
    private CapsuleCollider player_collider;
    private Vector3 move_dir;

    private Animator animator;

    private GameObject curr_cheese = null; // cheese object being controlled by player

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player_collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        bool is_grounded = IsGrounded();
        float prev_facing_dir = facing_dir; // save previous facing dir before updating
        bool is_key_down = false; // if nothing pressed, velocity.x = 0 to stop player immediately, creates a tight control

        // CONTROLS
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(move_speed, rb.velocity.y, 0);
            facing_dir = 1; // facing right
            is_key_down = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-move_speed, rb.velocity.y, 0);
            facing_dir = -1; // facing left
            is_key_down = true;
        }
        if (!is_key_down)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // if nothing pressed, velocity.x = 0 to stop player immediately, creates a tight control
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (is_grounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jump_force, 0);
            }
        }

        if (is_carrying && curr_cheese != null)
        {
            if(prev_facing_dir != facing_dir) // update cheese position if facing direction has changed
            {
                curr_cheese.transform.GetComponent<Rigidbody>().MovePosition(new Vector3(curr_cheese.transform.position.x + facing_dir * carrying_obj_offset, curr_cheese.transform.position.y, curr_cheese.transform.position.z));
            }
            curr_cheese.transform.GetComponent<Rigidbody>().velocity = rb.velocity; // match cheese velocity to player's
        }

        if (Input.GetKey(KeyCode.E))
        {
            if(curr_cheese!= null)
            {
                curr_cheese.transform.GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x * facing_dir + carrying_obj_offset, transform.position.y, transform.position.z));
                curr_cheese.transform.GetComponent<Rigidbody>().useGravity = false;
                is_carrying = true;
            }
        }

        // JUMP MODIFIERS FOR BETTER FEEL
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fall_multiplier * Time.deltaTime; //using Time.deltaTime due to acceleration
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * low_jump_multiplier * Time.deltaTime; //using Time.deltaTime due to acceleration
        }


        //// ANIMATOR
        //animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        //animator.SetFloat("VelocityY", rb.velocity.y);
        //if (is_grounded)
        //{
        //    animator.SetBool("IsGrounded", true);
        //    animator.SetBool("IsJumping", false);
        //    if (rb.velocity.x != 0)
        //    {
        //        animator.SetBool("IsRunning", true);
        //    }
        //    else
        //    {
        //        animator.SetBool("IsRunning", false);
        //    }
        //}
        //else
        //{
        //    animator.SetBool("IsGrounded", false);
        //    if (rb.velocity.y > 0)
        //    {
        //        animator.SetBool("IsJumping", true);
        //    }
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CubeCheese")){
            curr_cheese = collision.gameObject;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(new Vector3(player_collider.transform.position.x, player_collider.bounds.min.y, 0), Vector3.down, Color.green);
        return Physics.Raycast(new Vector3(player_collider.transform.position.x, player_collider.bounds.min.y + 0.1f, 0), Vector3.down, 0.2f);
    }
}
