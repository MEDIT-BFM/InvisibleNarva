using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

namespace InvisibleNarva {
    [RequireComponent(typeof(VideoPlayer), typeof(AudioSource))]
    public class VideoScreen : MonoBehaviour {
        [SerializeField] private float displayVideoInDelay;
        [SerializeField] private Character character;
        [SerializeField] private Material material;

        private AudioSource _audioSource;
        private VideoPlayer _videoPlayer;
        private WaitUntil _waitUntilVideoStop;

        private void Awake() {
            _videoPlayer = GetComponent<VideoPlayer>();
            _audioSource = GetComponent<AudioSource>();
            _waitUntilVideoStop = new WaitUntil(() => _audioSource.isPlaying == false);
            SetScreen();
        }

        private void Start() {
            character.OnBegin += BeginHandler;
        }

        private void BeginHandler(object entity) {
            StartCoroutine(PlayUntilStop());
        }

        private void SetScreen() {
            _videoPlayer.clip = character.Clip;
            _videoPlayer.isLooping = character.IsLooping;
            _videoPlayer.Prepare();
            _videoPlayer.Pause();
            material.DOFade(0, 0);

            _audioSource.clip = character.Voice;
        }

        private IEnumerator PlayUntilStop() {
            _audioSource.Play();
            material.DOFade(1, 3);
            DOTween.Sequence().AppendCallback(_videoPlayer.Play).SetDelay(displayVideoInDelay);
            yield return null;
            yield return _waitUntilVideoStop;
            _audioSource.Stop();
            material.DOFade(0, 3).OnComplete(_videoPlayer.Stop);
        }

        private void OnDestroy() {
            character.OnBegin -= BeginHandler;
        }
    }
}