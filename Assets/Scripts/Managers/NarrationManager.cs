using System;
using System.Collections;
using UnityEngine;

namespace InvisibleNarva {
    [RequireComponent(typeof(AudioSource))]
    public class NarrationManager : Singleton<NarrationManager> {
        public static event Action<Speech, bool> OnPlay = delegate { };
        public static event Action OnStop = delegate { };

        private Speech _current;
        private AudioSource _audioSource;
        private WaitUntil _waitUntilNarrationStop;

        private void OnEnable() {
            Task.OnSkip += TaskSkipHandler;
        }

        private void Start() {
            _audioSource = GetComponent<AudioSource>();
            _waitUntilNarrationStop = new WaitUntil(() => !_audioSource.isPlaying);
        }

        public void Play(Speech speech, bool showSubtitle) {
            _current = speech;
            _current.OnEnd += SpeechEndHandler;
            StartCoroutine(PlayNarrationUntilStop(showSubtitle));
        }

        private void TaskSkipHandler(Entity entity) {
            Stop();
        }

        private void SpeechEndHandler(object entity) {
            Stop();
            _current.OnEnd -= SpeechEndHandler;
        }

        private IEnumerator PlayNarrationUntilStop(bool showSubtitle = false) {
            _audioSource.clip = _current.Voice;
            _audioSource.Play();

            OnPlay?.Invoke(_current, showSubtitle);
            SoundManager.Instance.FadeIn();

            yield return null;
            yield return _waitUntilNarrationStop;
            _current.OnEnd -= SpeechEndHandler;
            _current.End();
            Stop();
        }

        private void Stop() {
            _audioSource.Stop();
            _audioSource.clip = null;
            OnStop?.Invoke();
            SoundManager.Instance.FadeOut();
            StopAllCoroutines();
        }

        private void OnDisable() {
            Stop();
            Task.OnSkip -= TaskSkipHandler;
        }
    }
}