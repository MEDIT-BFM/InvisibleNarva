using UnityEngine;
using DG.Tweening;

namespace InvisibleNarva {
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Singleton<SoundManager> {
        [SerializeField] private float audioFadeInValue = 0.1f;
        [SerializeField] private float audioFadeInDuration = 0.5f;
        [SerializeField] private AudioSource audioSource;

        public AudioSource AudioSource { get => audioSource; }

        public void Play(AudioClip clip, float delay = 0, bool loop = false) {
            audioSource.DOFade(1, 0);
            audioSource.loop = loop;
            audioSource.clip = clip;
            audioSource.PlayDelayed(delay);
        }

        public void FadeIn() {
            audioSource.DOFade(audioFadeInValue, audioFadeInDuration);
            audioSource.priority = 200;
        }

        public void FadeOut() {
            audioSource.DOFade(1, audioFadeInDuration);
            audioSource.priority = 150;
        }

        public void Stop() {
            audioSource.clip = null;
            audioSource.Stop();
        }

        public void Stop(float delay) {
            audioSource.DOFade(0, delay).OnComplete(() => {
                audioSource.clip = null;
                audioSource.Stop();
            });
        }

        private void Start() {
            DontDestroyOnLoad(this);
        }
    }
}