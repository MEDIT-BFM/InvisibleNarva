using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class IntroImageShifter : MonoBehaviour
{
    public SceneShifter sceneChange;
    public Transform introCanvas;
    public bool isTapped;
    public VideoClip[] videoClips;

    private int videoClipIndex;
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        isTapped = false;
        videoClipIndex = 0;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        EventManager.OnEnd += ForwardNarration;
    }

    private void OnDisable()
    {
        EventManager.OnEnd -= ForwardNarration;
    }

    private void Update()
    {
        if (!videoPlayer.isPlaying && isTapped == true)
        {
            ForwardNarration();
        }
    }

    public void ForwardNarration()
    {
        if (videoPlayer.isPrepared)
        {
            for (int i = 0; i < videoClips.Length; i++)
            {
                if (videoPlayer.clip.name == videoClips[i].name)
                {
                    videoClipIndex = i;
                }
            }
            videoClipIndex++;
            if (videoClipIndex < videoClips.Length)
            {
                PlayVideo(videoClips[videoClipIndex]);
            }
            else
            {
                sceneChange.FadeTransition();
            }            
        }        
    }

    void PlayVideo(VideoClip video)
    {
        videoPlayer.clip = video;
        videoPlayer.Play();
    }

    private IEnumerator PressedAnimation(bool isStarted)
    {
        yield return new WaitUntil(() => isStarted == true);
        if (isStarted)
        {
            introCanvas.gameObject.SetActive(false);
            SoundManager.Instance.PlayVideoSound(null);
            SoundManager.Instance.PlayBackgroundSound(null);
            videoPlayer.Play();
            isTapped = true;
        }
    }

    public void StartNarrate(bool isStarted)
    {
        StartCoroutine(PressedAnimation(isStarted));
    }
}
