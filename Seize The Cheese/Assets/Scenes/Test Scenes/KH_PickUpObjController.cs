using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KH_PickUpObjController : MonoBehaviour
{
    private bool is_pickable = false;
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            is_pickable = true;
        }
    }
}
