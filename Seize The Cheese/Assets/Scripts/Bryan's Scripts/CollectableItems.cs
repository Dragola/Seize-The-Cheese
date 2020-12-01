using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    public int collectableCount = 0;
    public void addCollectable()
    {
        collectableCount += 1;
        Debug.Log("Collectable added");
    }
}
