using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Footsteps : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] Footstepclips;

    [SerializeField]
    private AudioClip[] Backpackclips;

    [SerializeField]
    private AudioClip[] JumpClips;

    [SerializeField]
    private AudioClip[] LandClips;

    [SerializeField]
    private AudioClip[] PickupStepclips;

    [SerializeField]
    private AudioClip[] CheesePickupclip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
        //This is the Step event for Mousey's standard walk animation
    {
        AudioClip clip = GetRandomStepClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip);

    }

    private AudioClip GetRandomStepClip()
    {
        return Footstepclips[UnityEngine.Random.Range(0, Footstepclips.Length)];
        return Backpackclips[UnityEngine.Random.Range(0, Backpackclips.Length)];

    }
    
    private void Jump()
        //This is the Jump event for Mousey's jumping animation
    {
        AudioClip clip = GetRandomJumpClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip);

    }

    private AudioClip GetRandomJumpClip()
    {
        return JumpClips[UnityEngine.Random.Range(0, JumpClips.Length)];

    }
    
    private void Land()
        //This is the Jump event for Mousey's jumping animation
    {
        AudioClip clip = GetRandomLandClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip);

    }

    private AudioClip GetRandomLandClip()
    {
        return LandClips[UnityEngine.Random.Range(0, LandClips.Length)];

    }
    
    //private void CheesePickup()
    //{
    //    AudioClip clip = GetRandomCheesePickClip();
    //    audioSource.PlayOneShot(clip);
    //}

    //private AudioClip GetRandomCheesePickClip()
    //{
    //    return CheesePickupclip[UnityEngine.Random.Range(0, Footstepclips.Length)];
        
    //}

    private void PickupStep()
    //This is the PickupStep event for Mousey's walk animation while holding cheese
    {
        AudioClip clip = GetRandomPickStepClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomPickStepClip()
    {
        return PickupStepclips[UnityEngine.Random.Range(0, PickupStepclips.Length)];
    }
}
