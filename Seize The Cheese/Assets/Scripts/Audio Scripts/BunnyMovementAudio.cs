using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovementAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] BunnyMoveClips;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void BunnyStep()  //This is the Step event for Dust Bunnies standard walk animation
    {
        AudioClip clip = GetRandomStepClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomStepClip()
    {
        return BunnyMoveClips[UnityEngine.Random.Range(0, (BunnyMoveClips.Length) - 1)];
    }
}
