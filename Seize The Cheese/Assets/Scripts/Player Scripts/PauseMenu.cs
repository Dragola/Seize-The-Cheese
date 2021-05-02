using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot paused;
    private PlayerMovement playerControllerScript = null;
    // Start is called before the first frame update
    void Start()
    {
        //locate and reference player's script
        playerControllerScript = GameObject.Find("Mousy").GetComponent<PlayerMovement>();
    }
    public void OnClickResume()
    {
        //resumes game
        ResumeGame();

        //indicate to player to hide main menu
        playerControllerScript.ResumePlayer();
    }
    public void OnClickQuit()
    {
        //quit game
        Application.Quit();
    } 
    public void OnClickRestart()
    {
        //re-load current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        unpaused.TransitionTo(.01f);
        ResumeGame();
    }
    public void PauseGame()
    {
        //pause game
        Time.timeScale = 0;
        paused.TransitionTo(.01f);
    }
    public void ResumeGame()
    {
        //resume game
        Time.timeScale = 1;
        unpaused.TransitionTo(.01f);
    }
}
