using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunnyAudio : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] DustBunnyDeathclips;

    [SerializeField]
    private AudioClip[] DustBunnyNotifclips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    //private void Poof()
    //{
     //   AudioClip clip = GetRandomPoofClip();
     //   audioSource.pitch = Random.Range(0.5f, 1.5f);
     //   audioSource.PlayOneShot(clip);
     //
     //   AudioClip clip1 = GetRandomNotifClip();
     //   audioSource.PlayOneShot(clip1);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mousy")
        {
            AudioClip clip = GetRandomPoofClip();
            audioSource.pitch = Random.Range(0.5f, 1.5f);
            audioSource.PlayOneShot(clip);

            AudioClip clip1 = GetRandomNotifClip();
            audioSource.PlayOneShot(clip1);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mousy")
        {
            AudioClip clip = GetRandomPoofClip();
            audioSource.pitch = Random.Range(0.5f, 1.5f);
            audioSource.PlayOneShot(clip);

            AudioClip clip1 = GetRandomNotifClip();
            audioSource.PlayOneShot(clip1);
        }
    }

    private AudioClip GetRandomPoofClip()
    {

        return DustBunnyDeathclips[UnityEngine.Random.Range(0, (DustBunnyDeathclips.Length) - 1)];

    }

    private AudioClip GetRandomNotifClip()
    {
        return DustBunnyNotifclips[UnityEngine.Random.Range(0, (DustBunnyNotifclips.Length) - 1)];
    }


// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
