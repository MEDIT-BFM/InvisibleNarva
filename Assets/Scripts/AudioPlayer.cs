using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    [SerializeField] private bool looping;
    [SerializeField] private float initialDelay;
    [SerializeField] private AudioClip clip;

    public void Play() {
        SoundManager.Instance.Play(clip, initialDelay, looping);
    }
}
