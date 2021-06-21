using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

namespace InvisibleNarva {
    [RequireComponent(typeof(VideoPlayer))]
    public class Character3D : MonoBehaviour {
        [SerializeField] private bool looping;
        [SerializeField] private float initialDelay;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Material material;

        private VideoPlayer _videoPlayer;
        private bool _isSoundPlaying;

        public void Play() {
            if (SoundManager.InstanceExists) {
                StartCoroutine(PlayVideoCor());
                SoundManager.Instance.Play(clip, initialDelay, looping);
            }
        }

        private void Start() {
            _videoPlayer = GetComponent<VideoPlayer>();
            _isSoundPlaying = SoundManager.Instance.AudioSource.isPlaying;
        }

        private IEnumerator PlayVideoCor() {
            material.DOFade(0, 0).OnComplete(() => material.DOFade(1, 1).OnComplete(() => _videoPlayer.Play())).SetDelay(initialDelay);
            yield return new WaitUntil(() => _isSoundPlaying == false);
            material.DOFade(0, 1).OnComplete(() => _videoPlayer.Stop());
        }
    }
}