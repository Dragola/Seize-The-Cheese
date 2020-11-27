using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCheeseScript : MonoBehaviour
{

    public GameObject player;

    private void OnCollisionExit(Collision collision)
    {
        if (player.GetComponent<Character_Movement>().didPickUp == true)
        {

            //player.GetComponent<Character_Movement>().outOfPlace = true;
            transform.position = player.GetComponent<Character_Movement>().pickUpPosition.transform.position;


        }

    }


}
