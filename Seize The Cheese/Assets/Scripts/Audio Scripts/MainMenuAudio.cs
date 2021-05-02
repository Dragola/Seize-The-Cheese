using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{

    [SerializeField]
    public AudioClip[] pageturnclips;
    public AudioClip bookopen;
    public AudioClip bookclose;
    private AudioSource audioSource;
    public GameObject NewGameButton;
    public GameObject LoadButton;
    public GameObject SettingsButton;
    public GameObject ExitButton;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //NewGameButton = GetComponentinChildren<NewGame>();
    }
    
    void NewGameBookOpen()
    {

    }
}
