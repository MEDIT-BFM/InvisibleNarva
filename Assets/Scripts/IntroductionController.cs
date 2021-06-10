using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroductionController : MonoBehaviour {
    [SerializeField] private VideoClip[] videoClips;

    private VideoPlayer videoPlayer;
    private Queue<VideoClip> clips;

    private void Start() {
        clips = new Queue<VideoClip>(videoClips);
        videoPlayer = CharacterManager.Instance.VideoPlayer;
    }

    private void OnEnable() {
        EventManager.OnEnd += ForwardNarration;
    }

    private void ForwardNarration() {
        if (videoPlayer.isPrepared) {
            var next = clips.Dequeue();
            if (next != null) {
                videoPlayer.clip = next;
                videoPlayer.Play();
            }
            else {
                SceneController.Instance.ChangeScene("QuestSelection", 0.5f);
            }
        }
    }

    public void PlayVideo() {
       // SoundManager.Instance.StopAudioSources();

        videoPlayer.clip = clips.Dequeue();
        videoPlayer.Play();
    }

    private void OnDisable() {
        EventManager.OnEnd -= ForwardNarration;
    }
}