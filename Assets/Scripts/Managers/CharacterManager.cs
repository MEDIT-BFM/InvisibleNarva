using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;


[RequireComponent(typeof(AudioSource), typeof(VideoPlayer))]
public class CharacterManager : Singleton<CharacterManager> {
    public static event Action<Character> OnPlay = delegate { };
    public static event Action OnStop = delegate { };

    public AudioSource AudioSource { get; private set; }
    public VideoPlayer VideoPlayer { get; private set; }

    private Character _current;
    private WaitUntil _waitUntilVideoStop;
    private WaitUntil _waitUntilVideoPrepared;

    private void Start() {
        AudioSource = GetComponent<AudioSource>();
        VideoPlayer = GetComponent<VideoPlayer>();
        _waitUntilVideoStop = new WaitUntil(() => !AudioSource.isPlaying);
        _waitUntilVideoPrepared = new WaitUntil(() => !VideoPlayer.isPrepared);
    }

    public void PlayCharacter(Character character) {
        _current = character;

        StopCoroutine(PlayVideoUntilStop());
        StartCoroutine(PlayVideoUntilStop());
    }

    private IEnumerator PlayVideoUntilStop() {
        AudioSource.clip = _current.Voice;
        VideoPlayer.clip = _current.Clip;
        VideoPlayer.targetTexture = _current.RenderTexture as RenderTexture;
        VideoPlayer.Prepare();

        yield return _waitUntilVideoPrepared;

        AudioSource.Play();
        VideoPlayer.Play();
        OnPlay?.Invoke(_current);

        yield return null;
        yield return _waitUntilVideoStop;

        _current.End();
        AudioSource.Stop();
        VideoPlayer.Stop();
        VideoPlayer.clip = null;
        AudioSource.clip = null;
        VideoPlayer.targetTexture = null;
        OnStop?.Invoke();
    }
}