using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EndLevelChime : MonoBehaviour
{
    public AudioClip Chime;
    private AudioSource audioSource;
    private bool MouseyEntered;

    public AudioMixerSnapshot LevelEnd;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        MouseyEntered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && MouseyEntered == false)
        {
            Debug.Log("has collided");
            audioSource.PlayOneShot(Chime);
            MouseyEntered = true;

            LevelEnd.TransitionTo(.01f);
        }
    }
}