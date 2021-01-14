using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Wall_Touch : MonoBehaviour
{

    //public GameObject player;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Transform[] ks = GetComponentsInParent<Transform>();
            foreach (Transform t in ks)
            {
                if (t.tag == "Player")
                {
                    PlayerMovement controller = t.GetComponent<PlayerMovement>();
                    controller.canMoveRight = false;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Transform[] ks = GetComponentsInParent<Transform>();
            foreach (Transform t in ks)
            {
                if (t.tag == "Player")
                {
                    PlayerMovement controller = t.GetComponent<PlayerMovement>();
                    controller.canMoveRight = true;
                }
            }

        }

    }
}
