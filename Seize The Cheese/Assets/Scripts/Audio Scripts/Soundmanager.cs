using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{

    public static Soundmanager instance = null;
    public AudioClip JumpSound;
    public AudioSource AudSrc;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySoundOneShot(AudioClip snd, float volume)
    {
        AudSrc.volume = volume;
        AudSrc.PlayOneShot(snd);
    }

}
