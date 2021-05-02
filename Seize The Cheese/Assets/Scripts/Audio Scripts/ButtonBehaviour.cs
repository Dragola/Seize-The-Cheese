using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    private bool buttonHover;
    public AudioClip HoverTick;
    private AudioSource audioSource;
    private float tickcooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        buttonHover = false;
        audioSource = GetComponent<AudioSource>();
        tickcooldown = 0;
    }

    public void ButtonEntered()
    {
        if ((!buttonHover) && tickcooldown == 0) // If button hover is false and cooldown = 0
        {
            buttonHover = true;
            audioSource.PlayOneShot(HoverTick);
            tickcooldown = 1;
            Debug.Log("Buttonhashovered");
        }
    }

    public void ButtonExited()
    {
        if (buttonHover) // If Button Hover is true
        {
            buttonHover = false;
            tickcooldown = 0;
        }
    }

    private void NewTickDelay()
    {
        if ((tickcooldown == 1) && (buttonHover = true))
        {
            Invoke("ButtonEntered", 2.0f);
        }
    }
}
