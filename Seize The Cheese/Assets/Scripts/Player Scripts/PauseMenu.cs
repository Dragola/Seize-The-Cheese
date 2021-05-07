using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot paused;
    private MousyMovement playerMovement = null;
    // Start is called before the first frame update
    void Start()
    {
        //locate and reference player's script
        playerMovement = GameObject.Find("Mousy").GetComponent<MousyMovement>();
    }
    public void OnClickResume()
    {
        //resumes game
        ResumeGame();

        //resume player
        playerMovement.ResumePlayer();
    }
    public void OnClickQuit()
    {
        //quit game
        Application.Quit();
    } 
    public void OnClickMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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
