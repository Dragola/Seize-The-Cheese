using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateIncrement : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

                Debug.Log("enteredGate");
                GameObject camera = GameObject.Find("Main Camera");
                CameraLerp cameraLerp = camera.GetComponent<CameraLerp>();
            //    cameraLerp.MoveCamera(other.GetComponent<Character_Movement>().gateAt);


        }
    }
}
