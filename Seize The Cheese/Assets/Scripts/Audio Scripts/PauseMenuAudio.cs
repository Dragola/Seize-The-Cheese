using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuAudio : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    private AudioSource audioSource;

    public AudioClip resumebuttonsound;
    public AudioClip quitbuttonsound;
    public AudioClip restartbuttonsound;

    public Button resumebutton;
    public Button restartbutton;

    void Awake()
    {
        resumebutton = GameObject.Find("Resume").GetComponent<Button>();
        restartbutton = GameObject.Find("Restart").GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
    }
    
    
    
    void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(.01f);
        }

        else
        {
            unpaused.TransitionTo(.01f);
        }
    }
}