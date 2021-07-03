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
            StartCoroutine(PlayNarrationUntilStop(showSubtitle));
        }

        private void TaskSkipHandler(Entity entity) {
            Stop();
        }

        private IEnumerator PlayNarrationUntilStop(bool showSubtitle = false) {
            _audioSource.clip = _current.Voice;
            _audioSource.Play();

            OnPlay?.Invoke(_current, showSubtitle);

            yield return null;
            yield return _waitUntilNarrationStop;
            _current.End();
            Stop();
        }

        private void Stop() {
            _audioSource.Stop();
            _audioSource.clip = null;
            OnStop?.Invoke();
            StopAllCoroutines();
        }

        private void OnDisable() {
            Task.OnSkip -= TaskSkipHandler;
        }
    }
}