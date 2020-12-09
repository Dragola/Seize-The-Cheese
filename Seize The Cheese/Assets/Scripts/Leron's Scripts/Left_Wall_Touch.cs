﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Wall_Touch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Transform[] ks = GetComponentsInParent<Transform>();
            foreach (Transform t in ks)
            {
                if (t.tag == "Player")
                {
                    PlayerController controller = t.GetComponent<PlayerController>();
                    controller.canMoveLeft = false;
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
                    PlayerController controller = t.GetComponent<PlayerController>();
                    controller.canMoveLeft = true;
                }
            }
        }

    }
}
