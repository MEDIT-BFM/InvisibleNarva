using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer), typeof(AudioSource))]
public class IntroductionController : MonoBehaviour {
    [SerializeField] private float backgroundSoundDelay;
    [SerializeField] private AudioClip backgroundSound;
    [SerializeField] private VideoClip[] videoClips;

    private VideoPlayer _videoPlayer;
    private Queue<VideoClip> _clips;

    public void PlayOrSkip() {
        Play(_clips.Dequeue());
    }

    private void Play(VideoClip clip) {
        if (clip != null) {
            _videoPlayer.clip = clip;
            _videoPlayer.Prepare();
            _videoPlayer.prepareCompleted += (source) => _videoPlayer.Play();
        }
        else {
            SceneController.Instance.ChangeScene("QuestSelection", 0.5f);
        }
    }

    private void Start() {
        _clips = new Queue<VideoClip>(videoClips);
        _videoPlayer = GetComponent<VideoPlayer>();

        SoundManager.Instance.Play(backgroundSound, backgroundSoundDelay, true);
    }
}