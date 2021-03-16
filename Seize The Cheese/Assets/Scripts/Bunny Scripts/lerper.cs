using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerper : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 currentPoint;

    public float speed = -1f;
    public bool updateSpeed = false;
    public bool canMoveRight = false;
    public bool canMoveLeft = true;


    void Update()
    {
        currentPoint = transform.position;

        if(transform.position.x >= startPoint.x)
        {
            Debug.Log("Hit startPoint" + name);
            canMoveRight = false;
            canMoveLeft = true;
            updateSpeed = true;
        }
        else if (transform.position.x <= endPoint.x)
        {
            Debug.Log("Hit endPoint" + name);
            canMoveLeft = false;
            canMoveRight = true;
            updateSpeed = true;
        }
        //call movement function if movement is enabled
        if (canMoveLeft || canMoveRight)
        {
            Movement();
        }
    }
    private void Movement()
    {
        //change speed for left direction
        if (canMoveLeft && updateSpeed == true)
        {
            speed = speed - (2 * speed);
            updateSpeed = false;
        }
        //change speed for right direction
        else if (canMoveRight && updateSpeed == true)
        {
            speed = Mathf.Abs(speed);
            updateSpeed = false;
        }
        //move dust bunny
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
    public void SetMovement(bool enabled)
    {
        canMoveLeft = enabled;
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
            canMoveLeft = true;
            updateSpeed = true;
        }
        //Right side hit
        else if (direction.x == 1)
        {
            Debug.Log("Hit left" + name);
            canMoveLeft = false;
            canMoveRight = true;
            updateSpeed = true;
        }
    }
}
