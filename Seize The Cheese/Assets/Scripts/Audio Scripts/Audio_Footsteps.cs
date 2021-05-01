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

    public AudioClip BunnyPoof;

    [SerializeField]
    private AudioClip[] BunnyDeathNotif;

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
        return Footstepclips[UnityEngine.Random.Range(0, (Footstepclips.Length) - 1)];
        //return Backpackclips[UnityEngine.Random.Range(0, (Backpackclips.Length) - 1)];

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
        return JumpClips[UnityEngine.Random.Range(0, (JumpClips.Length) - 1)];

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
        return LandClips[UnityEngine.Random.Range(0, (LandClips.Length) - 1)];

    }
    
    private void PickupStep()
    //This is the PickupStep event for Mousey's walk animation while holding cheese
    {
        AudioClip clip = GetRandomPickStepClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomPickStepClip()
    {
        return PickupStepclips[UnityEngine.Random.Range(0, (PickupStepclips.Length) - 1)];
    }

    private void BunnyDeath()
    //This is the Bunny Death sound, attached to Mousey until the Dust Bunnies' animations are sorted
    {
        AudioClip clip = GetRandomBunnyDeathNotif();
        audioSource.PlayOneShot(clip);
        AudioClip clip2 = BunnyPoof;
        audioSource.PlayOneShot(clip2);
    }

    private AudioClip GetRandomBunnyDeathNotif()
    {
        return BunnyDeathNotif[UnityEngine.Random.Range(0, (BunnyDeathNotif.Length) - 1)];
    }
}
