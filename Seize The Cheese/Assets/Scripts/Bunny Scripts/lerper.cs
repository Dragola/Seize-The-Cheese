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
    public bool canMoveRight = true;
    public bool updateSpeed = false;
    public bool updateRotation = false;
    
    void Update()
    {
        //get current position of the bunny
        currentPoint = transform.position;

        //if the bunny has hit or gone past the startPoint.x
        if(transform.position.x >= startPoint.x)
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
            Movement();
        }
        
    }
    private void Movement()
    {
        //change speed for left direction
        if (updateSpeed == true && canMoveRight == false)
        {
            transform.position = new Vector3(startPoint.x - 5, 0.45f, -6.5f);
            updateSpeed = false;
        }
        //change speed for right direction
        else if (canMoveRight == true && updateSpeed == true)
        {
            transform.position = new Vector3(endPoint.x + 5, 0.45f, -6.5f);
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
            }
            //rotate to face the left direction
            else if (canMoveRight == false)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                updateRotation = false;
            }
        }
        //move dust bunny
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
    public void SetMovement(bool enabled)
    {
        canMoveRight = enabled;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector2 direction = collision.GetContact(0).normal;

        //Left side hit
        if (direction.x == -1)
        {
            Debug.Log("Hit right" + name);
            canMoveRight = false;
            updateSpeed = true;
            updateRotation = true;
        }
        //Right side hit
        else if (direction.x == 1)
        {
            Debug.Log("Hit left" + name);
            canMoveRight = true;
            updateSpeed = true;
            updateRotation = true;
        }
    }
}
