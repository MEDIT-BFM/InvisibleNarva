using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class EntranceScreen : MonoBehaviour {
    [SerializeField] private string nextSceneName;
    [SerializeField] private float entranceClipDelay;
    [SerializeField] private AudioClip entranceClip;
    [SerializeField] private VideoClip[] videoClips;

    private VideoPlayer _videoPlayer;
    private Queue<VideoClip> _clips;
    private const float _fadeOutDuration = 0.5f;

    public void PlayOrSkip() {
        if (_clips.Count > 0) {
            Play(_clips.Dequeue());
        }
        else {
            SceneController.Instance.ChangeScene(nextSceneName, _fadeOutDuration);
        }
    }

    private void Play(VideoClip clip) {
        _videoPlayer.clip = clip;
        _videoPlayer.Prepare();
        _videoPlayer.prepareCompleted += (source) => _videoPlayer.Play();
    }

    private void Start() {
        _clips = new Queue<VideoClip>(videoClips);
        _videoPlayer = GetComponent<VideoPlayer>();

        SoundManager.Instance.Play(entranceClip, entranceClipDelay);
    }
}