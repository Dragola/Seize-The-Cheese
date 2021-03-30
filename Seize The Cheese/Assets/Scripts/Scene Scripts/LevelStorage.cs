using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStorage : MonoBehaviour
{
    public string currentSceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        //get current scenes name
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    
    public string GetSceneName()
    {
        return currentSceneName;
    }
}
