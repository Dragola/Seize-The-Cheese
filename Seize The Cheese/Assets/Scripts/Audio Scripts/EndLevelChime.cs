using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelChime : MonoBehaviour
{
    public AudioClip Chime;
    private AudioSource audioSource;
    private bool MouseyEntered;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        MouseyEntered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            Debug.Log("has collided");
            audioSource.PlayOneShot(Chime);
            MouseyEntered = true;
        }
    }
}