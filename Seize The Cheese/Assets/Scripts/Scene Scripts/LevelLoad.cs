using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public Text progressText = null;
    public AsyncOperation asyncLoad = null;

    // Start is called before the first frame update
    void Awake()
    {
        progressText = GameObject.Find("ProgressText").GetComponent<Text>();
    }
    
    void Start()
    {
        Debug.Log("Start called");
        //get the name of the previous level
        EndOfLevel endofLevel = GameObject.Find("EndOfLevelTrigger").GetComponent<EndOfLevel>();
        
        //call function to load the next level
        LoadNextLevel(endofLevel.GetSceneName());

        Debug.Log("After LoadNextLevel was called");

        //destroy object as no longer needed
        //Destroy(endofLevel.gameObject);
    }

    void LoadNextLevel(string sceneName)
    {
        Debug.Log("LoadNextLevel called");

        string nextScene = GetNextLevel(sceneName);

        //
        if (nextScene.CompareTo("") != 0) {
            //load next scene asyncronously
            asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);

            //deny scene from activating as soon as it's done
            asyncLoad.allowSceneActivation = false;

            //while progress is less then 90% (seems to freeze at 0.9 if allowSceneActivation is false)
            while (asyncLoad.progress < 0.9f)
            {
                //while the scene is still being loaded
                progressText.text = "Waiting for scene to load..." + asyncLoad.progress + ".";
                Debug.Log("Waiting for scene to load..." + asyncLoad.progress + ".");
            }
            progressText.text = "Level ready! Press enter to continue...";
            Debug.Log("Level ready! Press enter to continue...");
        }
        else
        {
            Debug.Log("Can't load next level");
            progressText.text = "Unable to load next level...";
        }
    }
    void Update()
    {
        //only swap to the 
        if (Input.GetKeyDown(KeyCode.Return) && asyncLoad != null)
        {
            Debug.Log("Return key hit");

            //allow scene to be activated
            asyncLoad.allowSceneActivation = true;
            Debug.Log("Scene loaded");
        }
    }
    private string GetNextLevel(string sceneName)
    {
        string nextLevel = "";

        Debug.Log("GetNextLevel called: sceneName = " + sceneName);

        //level 1 -> level 2
        if (sceneName.Contains("Level 1"))
        {
            nextLevel = "Level2";
        }
        //level 2 -> level 3
        else if (sceneName.Contains("2"))
        {
            nextLevel = "Level3";
        }
        ///....

        return nextLevel;
    }
}
