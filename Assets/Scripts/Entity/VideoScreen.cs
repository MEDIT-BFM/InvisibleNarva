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

        private void OnValidate() {
            _videoPlayer = GetComponent<VideoPlayer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Awake() {
            _waitUntilVideoStop = new WaitUntil(() => _audioSource.isPlaying == false);
            SetScreen();
        }
        private void Start() {
            character.OnBegin += BeginHandler;
            //NarrationManager.OnPlay += (speech, isDisplaying) => FadeIn();
            //NarrationManager.OnStop += () => FadeOut();
        }

        private void BeginHandler(object entity) {
            StartCoroutine(PlayUntilStop());
        }

        private void SetScreen() {
            _videoPlayer.clip = character.Clip;
           // _videoPlayer.targetTexture = character.RenderTexture;
            _videoPlayer.isLooping = character.IsLooping;
            //_videoPlayer.targetTexture.Release();
           // _videoPlayer.targetTexture.width = (int)character.Clip.width;
           // _videoPlayer.targetTexture.height = (int)character.Clip.height;
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

        private void FadeIn() {
            _audioSource.DOFade(0.4f, 0.5f);
            _audioSource.priority = 200;
        }

        private void FadeOut() {
            _audioSource.DOFade(1, 0.5f);
            _audioSource.priority = 150;
        }

        private void OnDestroy() {
            character.OnBegin -= BeginHandler;
        }
    }
}