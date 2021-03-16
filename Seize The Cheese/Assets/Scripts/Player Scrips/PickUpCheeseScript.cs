using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCheeseScript : MonoBehaviour
{

    public GameObject infoText;


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            infoText.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            infoText.SetActive(false);

        }
    }



}
