using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Script removes Transparent Button Border
public class ButtonShapeScript : MonoBehaviour
{
    public float alphathreshhold = 0.1f;
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphathreshhold;
    }
}
