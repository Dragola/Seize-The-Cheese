using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfLevel : MonoBehaviour
{
    public bool loadAsync = true;
    public string currentSceneName = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.CompareTo("Player") == 0)
        {
            //laod with async
            if (loadAsync)
            {
                Debug.Log("loadAsync was true");
                currentSceneName = SceneManager.GetActiveScene().name;


                //prevents script from being deleted when loading screen is loaded
                DontDestroyOnLoad(this.gameObject);

                //load next scene
                SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
            }
            //load directly
            else
            {
                //load next scene
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
            }
        }
    }
    public string GetSceneName()
    {
        return currentSceneName;
    }
}
