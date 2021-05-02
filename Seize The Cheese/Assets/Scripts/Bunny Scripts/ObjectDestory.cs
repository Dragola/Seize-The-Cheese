using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestory : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mousy")
        {
            Destroy(gameObject, 1f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mousy")
        {
           Destroy(gameObject, 1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}