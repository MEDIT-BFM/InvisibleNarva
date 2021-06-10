using UnityEngine;
using DG.Tweening;

public class SoundManager : Singleton<SoundManager> {
    [SerializeField] private float audioFadeEndValue = 0.1f;
    [SerializeField] private float audioFadeDuration = 0.5f;
    [SerializeField] private AudioSource backgroundSource;

    public AudioSource BackgroundSource { get => backgroundSource; }

    public void PlayBackground(AudioClip clip) {
        backgroundSource.clip = clip;
        backgroundSource.Play();
    }

    private void OnEnable() {
        CharacterManager.OnPlay += CharacterPlayHandler;
        CharacterManager.OnStop += CharacterStopHandler;
        NarrationManager.OnStop += CharacterStopHandler;
        NarrationManager.OnPlay += NarrationPlayHandler;
    }

    private void NarrationPlayHandler(Speech speech) {
        backgroundSource.DOFade(audioFadeEndValue, audioFadeDuration);
        backgroundSource.priority = 200;
    }

    private void CharacterStopHandler() {
        backgroundSource.DOFade(1, audioFadeDuration);
        backgroundSource.priority = 150;
    }

    private void CharacterPlayHandler(Character character) {
        backgroundSource.DOFade(audioFadeEndValue, audioFadeDuration);
        backgroundSource.priority = 200;
    }

    private void OnDisable() {
        CharacterManager.OnPlay -= CharacterPlayHandler;
        CharacterManager.OnStop -= CharacterStopHandler;
        NarrationManager.OnPlay -= NarrationPlayHandler;
        NarrationManager.OnStop -= CharacterStopHandler;
    }
}