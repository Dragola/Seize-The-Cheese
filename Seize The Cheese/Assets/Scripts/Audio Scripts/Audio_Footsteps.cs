using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Footsteps : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] Footstepclips;

    [SerializeField]
    private AudioClip[] Backpackclips;

    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip);

        AudioClip clip2 = GetRandomClip();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.PlayOneShot(clip2);
    }
    
    private AudioClip GetRandomClip()
    {
        return Footstepclips[UnityEngine.Random.Range(0, Footstepclips.Length)];
        return Backpackclips[UnityEngine.Random.Range(0, Backpackclips.Length)];

    }
}
