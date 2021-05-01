using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerLevel : MonoBehaviour
{
    private VideoPlayer videoPlayer = null;
    private Canvas videoCanvas = null;
    private MousyMovement player = null;
    private bool videoStarted = false;
    private Animator animation = null;
    private float timer = 5;
    // Start is called before the first frame update
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        player = GameObject.Find("Mousy").GetComponent<MousyMovement>();
        videoCanvas = GameObject.Find("Video UI").GetComponent<Canvas>();
        animation = GameObject.Find("Video Fade In").GetComponent<Animator>();
    }

    void Start()
    {
        videoCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (videoStarted && animation.GetCurrentAnimatorStateInfo(0).IsName("fadein"))
        {
            Debug.Log("Video Player: Fading in animation...");
        }
        else if (videoStarted)
        {
            Debug.Log("Video Player: Fading in animation done?");
        
            videoPlayer.Play();
            
            //wait 5 seconds before checking if video is playing
            if (videoStarted && timer > 0)
            {
                timer -= Time.deltaTime;
            }
            //if video is done then resume game
            else if (videoStarted && videoPlayer.isPlaying == false)
            {
                Debug.Log("Video is done playing");
                videoCanvas.gameObject.SetActive(false);
                player.ResumePlayerFromVideo();
                videoStarted = false;
                timer = 5;
            }
        }
    }
    public void StartVideo()
    {
        videoCanvas.gameObject.SetActive(true);
        videoStarted = true;
    }
}
