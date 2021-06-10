using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NarrationManager : Singleton<NarrationManager> {
    public static event Action<Speech> OnPlay = delegate { };
    public static event Action OnStop = delegate { };

    private Speech _current;
    private AudioSource _audioSource;
    private WaitUntil _waitUntilNarrationStop;

    protected override void Awake() {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        _waitUntilNarrationStop = new WaitUntil(() => !_audioSource.isPlaying);
    }

    public void Play(Speech speech) {
        _current = speech;
        StopCoroutine(PlayNarrationUntilStop());
        StartCoroutine(PlayNarrationUntilStop());
    }

    private IEnumerator PlayNarrationUntilStop() {
        _audioSource.clip = _current.Voice;
        _audioSource.Play();

        OnPlay?.Invoke(_current);

        yield return null;
        yield return _waitUntilNarrationStop;

        _audioSource.Stop();
        _audioSource.clip = null;
        _current.End();

        OnStop?.Invoke();
    }
}