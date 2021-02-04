using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerper : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    public float speed;
    public bool canMove = true;

    void Update()
    {
        //call movement function if movement is enabled
        if (canMove == true)
        {
            Movement();
        }
    }
    private void Movement()
    {
        transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.PingPong(Time.time / 2 * speed, 1 ));
    }
    public void SetMovement(bool enabled)
    {
        canMove = enabled;
    }
}
