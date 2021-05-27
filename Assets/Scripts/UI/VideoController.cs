using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject[] videos;

    private RawImage[] raws;
    private VideoPlayer[] videoPlayers;
    private int totalVideoPlay = 0;
    private int isVideoPlay = -1;
    private int maxVideo = 3;
    private void Awake()
    {
        raws = new RawImage[videos.Length];
        videoPlayers = new VideoPlayer[videos.Length];

        for(int i = 0; i < videos.Length; i++)
        {
            raws[i] = videos[i].GetComponentInChildren<RawImage>();
            videoPlayers[i] = videos[i].GetComponentInChildren<VideoPlayer>();
            videoPlayers[i].playOnAwake = false;
        }
    }

    void Start()
    {
       
    }

    void Update()
    {

        for(int i = totalVideoPlay; i < maxVideo;i++)
        {
            if (totalVideoPlay == i)
            {
                if (!IsVideoPlaying(i) && isVideoPlay != i)
                {
                    isVideoPlay = i;
                    ShowVideo(i);
                }
                else if(!IsVideoPlaying(i))
                {
                    totalVideoPlay++;
                }
            }
        }

        
        
      

        Debug.Log("play : "+IsVideoPlaying(0));
        
        //  if (!IsVideoPlaying(0)) ShowVideo(1);
        //  else if (!IsVideoPlaying(1)) ShowVideo(2);
    }

    void ShowVideo(int index)
    {
        for(int i = 0; i < videos.Length; i++) 
        {
            raws[i].gameObject.SetActive(false);
            videoPlayers[i].gameObject.SetActive(false);
        } 

        raws[index].gameObject.SetActive(true);
        videoPlayers[index].gameObject.SetActive(true);
        videoPlayers[index].Play();
    }

    bool IsVideoPlaying(int index)
    {
        return videoPlayers[index].isPlaying;
    }
}
