using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class EndOfLevel : MonoBehaviour
{
    public MousyMovement playerMovementScript = null;
    private Canvas endOfLevelUI = null;
    public bool playerHitTrigger = false;
    private LevelStorage levelStorage = null;
    private VideoPlayer videoPlayer = null;
    private Canvas videoCanvas = null;
    public bool videoStarted = false;
    private float timer = 5;

    private void Awake()
    {
        videoCanvas = GameObject.Find("Video UI").GetComponent<Canvas>();
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        levelStorage = GameObject.Find("LevelStorage").GetComponent<LevelStorage>();
        playerMovementScript = GameObject.Find("Mousy").GetComponent<MousyMovement>();
        endOfLevelUI = GameObject.Find("EndOfLevel UI").GetComponent<Canvas>();
    }
    private void Start()
    {
        endOfLevelUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (playerHitTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoadCreditVideo();
                //SwapToLoadingScene();
                playerHitTrigger = false;
            }
        }
        //subtract from timer
        if (videoStarted && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        //if video is done then go to main menu
        if (videoStarted && timer <= 0 && videoPlayer.isPlaying == false)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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
    private void LoadCreditVideo()
    {
        //prevent mousy from moving
        GameObject.Find("Mousy").GetComponent<MousyMovement>().FreezePlayer(true);

        //load clip
        videoPlayer.clip = (VideoClip)AssetDatabase.LoadAssetAtPath("Assets/Cutscenes/Credits.mp4", typeof(VideoClip));

        //play video
        videoPlayer.Play();

        videoStarted = true;

        //enable canvas and video player
        videoCanvas.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(true);
    }
}
