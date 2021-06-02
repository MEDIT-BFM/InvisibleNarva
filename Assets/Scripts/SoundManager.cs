using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class SoundManager : Singleton<SoundManager> {
    [SerializeField] private AudioSource narrationSource;
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource videoSource;
    [SerializeField] private VideoPlayer videoPlayer;

    public AudioSource NarrationSource { get => narrationSource; }
    public AudioSource BackgroundSource { get => backgroundSource; }
    public AudioSource VideoSource { get => videoSource; }
    public VideoPlayer VideoPlayer { get => videoPlayer; }

    private void Update() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3) {
            narrationSource = FindObjectOfType<Narrate.NarrationManager>().gameObject.GetComponent<AudioSource>();
            if (NarrationSource.clip != null || VideoSource.clip != null) {
                BackgroundSource.volume = 0.1f;
                BackgroundSource.priority = 200;
            }
            else {
                BackgroundSource.volume = 1f;
                BackgroundSource.priority = 150;
            }
        }
    }

    public void PlayBackgroundSound(AudioClip clip) {
        BackgroundSource.clip = clip;
        BackgroundSource.Play();
    }

    public void PlayVideoSound(AudioClip clip) {
        VideoSource.clip = clip;
        VideoSource.Play();
    }

    public void StopAudioSources() {
        BackgroundSource.clip = VideoSource.clip = null;
        BackgroundSource.Stop();
        VideoSource.Stop();
    }

    public void MakeNullAfterPlay() {
        StartCoroutine(IsPlaying());
    }

    private IEnumerator IsPlaying() {
        yield return new WaitUntil(() => VideoSource.isPlaying == false);
        PlayVideoSound(null);
        StopCoroutine(IsPlaying());
    }
}