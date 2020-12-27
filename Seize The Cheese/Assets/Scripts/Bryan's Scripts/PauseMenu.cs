using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerController playerControllerScript = null;
    // Start is called before the first frame update
    void Start()
    {
        //locate and reference player's script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        //resumes game
        ResumeGame();
    }
    public void PauseGame()
    {
        //pause game
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        //resume game
        Time.timeScale = 1;
    }
}
