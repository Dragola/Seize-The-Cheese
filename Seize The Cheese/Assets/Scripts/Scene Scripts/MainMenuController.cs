using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    LevelStorage levelStorage = null;

    //defining menus
    public GameObject BaseMenu;
    public GameObject SettingsMenu;
    public GameObject LoadgameMenu;

    //Audio for pressing the buttons
    public AudioClip NewGameSound;
    public AudioClip PageTurnSound;
    public AudioClip ExitGameSound;
    private AudioSource audioSource;
    public AudioSource levelaudiosource;

    private void Start()
    {
        levelStorage = GameObject.Find("LevelStorage").GetComponent<LevelStorage>();
        levelaudiosource = GameObject.Find("LevelStorage").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        Cursor.visible = true;

    }
    public void MoveToMainGame()
    {
        Cursor.visible = false;
        //don't destroy
        DontDestroyOnLoad(levelStorage.gameObject);

        //load MainGame (needs to be changed to level loading
        SceneManager.LoadScene(1);

        levelaudiosource.PlayOneShot(NewGameSound);

    }
    public void MoveToLoadGame()
    {
        AudioClip clip = PageTurnSound;
        audioSource.PlayOneShot(clip);

        BaseMenu.gameObject.SetActive(false);
        LoadgameMenu.gameObject.SetActive(true);
 
    }
    public void MoveToSettings()
    {
        AudioClip clip = PageTurnSound;
        audioSource.PlayOneShot(clip);

        BaseMenu.gameObject.SetActive(false);
        SettingsMenu.gameObject.SetActive(true);
    }
    public void MoveToAbout()
    {

    }
    public void ExitGame()
    {
        AudioClip clip = ExitGameSound;
        audioSource.PlayOneShot(clip);

        Invoke("ExitApplication", 1.5f);

    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void MovetoHome()
    {
        BaseMenu.gameObject.SetActive(true);
        SettingsMenu.gameObject.SetActive(false);
        LoadgameMenu.gameObject.SetActive(false);
    }
}
