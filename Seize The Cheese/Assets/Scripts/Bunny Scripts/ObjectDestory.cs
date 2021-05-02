using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestory : MonoBehaviour
{   
    [SerializeField]
    public AudioClip[] BunnyNotif;

    public AudioClip BunnyPoof;
    private AudioSource audioSource;
    private bool BunnyPoofed;
    private float PoofTimer = 5.0f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioClip clip = GetRandomNotifClip();
            audioSource.PlayOneShot(BunnyPoof);
            Invoke("BunnyDeath", PoofTimer);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(BunnyPoof);
            Invoke("BunnyDeath", PoofTimer);
        }
    }


    void BunnyDeath()
    {
        Destroy(gameObject);
    }

    private AudioClip GetRandomNotifClip()
    {
        return BunnyNotif[UnityEngine.Random.Range(0, (BunnyNotif.Length) - 1)];
    }
}