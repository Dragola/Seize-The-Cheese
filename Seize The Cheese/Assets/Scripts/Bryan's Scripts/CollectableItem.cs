using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //only run if the collider is the player (prevent enemy or other from triggering)
        if (other.gameObject.name.CompareTo("Player") == 0)
        {
            //signal to parent script that collectable was grabbed
            this.transform.parent.GetComponent<CollectableCounter>().addCollectable();
            
            //destroy collectable object
            Destroy(this.gameObject);
        }
    }
}
