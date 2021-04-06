using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    LevelStorage levelStorage = null;

    private void Start()
    {
        levelStorage = GameObject.Find("LevelStorage").GetComponent<LevelStorage>();
    }
    public void MoveToMainGame()
    {
        //don't destroy
        DontDestroyOnLoad(levelStorage.gameObject);

        //load MainGame (needs to be changed to level loading
        SceneManager.LoadScene(1);
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
