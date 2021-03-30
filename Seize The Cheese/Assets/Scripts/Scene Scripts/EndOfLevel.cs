using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfLevel : MonoBehaviour
{
    private PlayerMovement playerMovementScript = null;
    private Canvas endOfLevelUI = null;
    private bool playerHitTrigger = false;
    private LevelStorage levelStorage = null;

    private void Start()
    {
        levelStorage = GameObject.Find("LevelStorage").GetComponent<LevelStorage>();
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        endOfLevelUI = GameObject.Find("EndOfLevel UI").GetComponent<Canvas>();
        endOfLevelUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (playerHitTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SwapToLoadingScene();
                playerHitTrigger = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //once player hits
        if (other.gameObject.name.CompareTo("Player") == 0)
        {
            //stop player from moving
            playerMovementScript.FreezePlayer();

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
}
