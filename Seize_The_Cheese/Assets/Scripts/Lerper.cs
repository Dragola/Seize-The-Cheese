using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    public float speed;

    void Update()
    {
        transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.PingPong(Time.time/2, 1));
    }
}
