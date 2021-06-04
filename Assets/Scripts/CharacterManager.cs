using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CharacterManager : Singleton<CharacterManager> {
    public static event Action OnCharacterPlay = delegate { };
    public static event Action OnCharacterStop = delegate { };

    [SerializeField] private AudioSource videoSource;
    [SerializeField] private VideoPlayer videoPlayer;

    public AudioSource VideoSource { get => videoSource; }
    public VideoPlayer VideoPlayer { get => videoPlayer; }

    private Character _current;
    private WaitUntil _waitUntilVideoStop;
    private WaitUntil _waitUntilVideoPrepared;

    private void Start() {
        _waitUntilVideoStop = new WaitUntil(() => !videoSource.isPlaying);
        _waitUntilVideoPrepared = new WaitUntil(() => !videoPlayer.isPrepared);
    }

    public void PlayCharacter(Character character) {
        _current = character;

        StopCoroutine(PlayVideoUntilStop());
        StartCoroutine(PlayVideoUntilStop());
    }


    private IEnumerator PlayVideoUntilStop() {
        videoSource.clip = _current.Voice;
        videoPlayer.clip = _current.Clip;
        videoPlayer.targetTexture = _current.TargetTexture.texture as RenderTexture;
        videoPlayer.Prepare();

        yield return _waitUntilVideoPrepared;

        videoSource.Play();
        videoPlayer.Play();
        OnCharacterPlay?.Invoke();

        yield return null;
        yield return _waitUntilVideoStop;

        _current.End();
        videoSource.Stop();
        videoPlayer.Stop();
        videoPlayer.clip = null;
        videoSource.clip = null;
        videoPlayer.targetTexture = null;
        OnCharacterStop?.Invoke();
    }
}