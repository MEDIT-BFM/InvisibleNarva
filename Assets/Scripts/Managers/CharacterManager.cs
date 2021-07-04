using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace InvisibleNarva {
    [RequireComponent(typeof(AudioSource), typeof(VideoPlayer))]
    public class CharacterManager : Singleton<CharacterManager> {
        public static event Action<Character> OnPlay = delegate { };
        public static event Action OnStop = delegate { };

        private Character _current;
        private AudioSource _audioSource;
        private VideoPlayer _videoPlayer;
        private WaitUntil _waitUntilVideoStop;
        private WaitUntil _waitUntilVideoPrepared;

        private void OnEnable() {
            Task.OnSkip += TaskSkipHandler;
        }

        private void Start() {
            _audioSource = GetComponent<AudioSource>();
            _videoPlayer = GetComponent<VideoPlayer>();
            _waitUntilVideoStop = new WaitUntil(() => _audioSource.isPlaying == false);
            _waitUntilVideoPrepared = new WaitUntil(() => _videoPlayer.isPrepared == false);
        }

        public void Play(Character character) {
            _current = character;
            _videoPlayer.isLooping = character.IsLooping;
            StartCoroutine(PlayUntilStop());
        }

        private void TaskSkipHandler(Entity entity) {
            Stop();
        }
        private IEnumerator PlayUntilStop() {
            _audioSource.clip = _current.Voice;
            _videoPlayer.clip = _current.Clip;
            _videoPlayer.targetTexture.Release();
            _videoPlayer.targetTexture.width = (int)_current.Clip.width;
            _videoPlayer.targetTexture.height = (int)_current.Clip.height;
            _videoPlayer.Prepare();

            yield return _waitUntilVideoPrepared;

            _audioSource.Play();
            _videoPlayer.Play();
            OnPlay?.Invoke(_current);

            yield return null;
            yield return _waitUntilVideoStop;
            _current.End();

            Stop();
        }

        private void Stop() {
            _audioSource.Stop();
            _videoPlayer.Stop();
            _audioSource.clip = null;
            _videoPlayer.clip = null;

            OnStop?.Invoke();
            StopAllCoroutines();
        }

        private void OnDisable() {
            Stop();
            Task.OnSkip -= TaskSkipHandler;
        }
    }
}