using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager> {
    [SerializeField] private float audioFadeInValue = 0.1f;
    [SerializeField] private float audioFadeInDuration = 0.5f;
    [SerializeField] private AudioSource backgroundSource;

    public void Play(AudioClip clip, float delay = 0, bool loop = false) {
        backgroundSource.loop = loop;
        backgroundSource.clip = clip;
        backgroundSource.Play((ulong)delay);
    }

    public void FadeIn() {
        backgroundSource.DOFade(audioFadeInValue, audioFadeInDuration);
        backgroundSource.priority = 200;
    }

    public void FadeOut() {
        backgroundSource.DOFade(1, audioFadeInDuration);
        backgroundSource.priority = 150;
    }

    public void Stop() {
        backgroundSource.clip = null;
        backgroundSource.Stop();
    }

    private void Start() {
        DontDestroyOnLoad(this);
    }
}