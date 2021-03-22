using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void MoveToMainGame()
    {
        //load MainGame (needs to be changed to level loading
        SceneManager.LoadScene(2);
    }
    public void MoveToLoadGame()
    {

    }
    public void MoveToSettings()
    {

    }
    public void MoveToAbout()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
