using UnityEngine;

namespace InvisibleNarva {
    public class AudioPlayer : MonoBehaviour {
        [SerializeField] private bool looping;
        [SerializeField] private float initialDelay;
        [SerializeField] private AudioClip clip;

        public void Play() {
            if (SoundManager.InstanceExists) {
                SoundManager.Instance.Play(clip, initialDelay, looping);
            }
        }
    }
}