using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace InvisibleNarva {
    [RequireComponent(typeof(VideoPlayer))]
    public class EntranceScreen : MonoBehaviour {
        [SerializeField] private string nextSceneName;
        [SerializeField] private float entranceClipDelay;
        [SerializeField] private AudioClip entranceClip;
        [SerializeField] private VideoClip[] videoClips;
        [SerializeField] private Quest[] quests;

        private VideoPlayer _videoPlayer;
        private Queue<VideoClip> _clips;

        public void PlayOrSkip() {
            if (_clips.Count > 0) {
                Play(_clips.Dequeue());
            }
            else {
                SceneController.Instance.ChangeScene(nextSceneName);
            }
        }

        private void Play(VideoClip clip) {
            _videoPlayer.clip = clip;
            _videoPlayer.Prepare();
            _videoPlayer.prepareCompleted += (source) => _videoPlayer.Play();
        }

        private void Start() {
            _clips = new Queue<VideoClip>(videoClips);
            _videoPlayer = GetComponent<VideoPlayer>();

            SoundManager.Instance.Play(entranceClip, entranceClipDelay);

            for (int i = 0; i < quests.Length; i++) {
                quests[i].IsCompleted = false;
            }
        }
    }
}