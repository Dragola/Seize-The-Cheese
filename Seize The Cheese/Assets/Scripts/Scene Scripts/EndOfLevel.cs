using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfLevel : MonoBehaviour
{
    public MousyMovement playerMovementScript = null;
    private Canvas endOfLevelUI = null;
    public bool playerHitTrigger = false;
    private LevelStorage levelStorage = null;

    private void Start()
    {
        levelStorage = GameObject.Find("LevelStorage").GetComponent<LevelStorage>();
        playerMovementScript = GameObject.Find("Mousy").GetComponent<MousyMovement>();
        endOfLevelUI = GameObject.Find("EndOfLevel UI").GetComponent<Canvas>();
        endOfLevelUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (playerHitTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoopLevelScene();
                //SwapToLoadingScene();
                playerHitTrigger = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //once player hits
        if (other.gameObject.tag.CompareTo("Player") == 0)
        {
            //stop player from moving
            playerMovementScript.FreezePlayer(true);

            //show end of level UI
            endOfLevelUI.gameObject.SetActive(true);

            playerHitTrigger = true;
        }
    }
    private void SwapToLoadingScene()
    {
        //prevents script from being deleted when loading screen is loaded (so that loading scene can determine what scene it needs to load)
        DontDestroyOnLoad(levelStorage.gameObject);

        //switch to the loading screen
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
    private void LoopLevelScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
