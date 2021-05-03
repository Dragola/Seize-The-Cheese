using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerLevel : MonoBehaviour
{
    private VideoPlayer videoPlayer = null;
    private Canvas videoCanvas = null;
    private MousyMovement player = null;
    public bool videoStarted = false;
    public float timer = 5;

    // Start is called before the first frame update
    //this code exists again
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        player = GameObject.Find("Mousy").GetComponent<MousyMovement>();
        videoCanvas = GameObject.Find("Video UI").GetComponent<Canvas>();
    }

    void Start()
    {
        videoCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //subtract from timer
        if (videoStarted && timer > 0)
        {
            Debug.Log("if (videoStarted && timer > 0)");
            timer -= Time.deltaTime;
        }
        //if video is done then resume game
        if (videoStarted && timer <= 0 && videoPlayer.isPlaying == false)
        {
            Debug.Log("Video is done playing");
            videoCanvas.gameObject.SetActive(false);
            player.ResumePlayerFromVideo();
            videoStarted = false;
            timer = 5;
        }
    }
    public void StartVideo()
    {
        videoCanvas.gameObject.SetActive(true);
        videoStarted = true;
        videoPlayer.Play();
    }
}
