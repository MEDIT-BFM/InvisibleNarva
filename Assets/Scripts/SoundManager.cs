using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
    public AudioSource BackgroundSource;
    public AudioSource VideoSource;
    public AudioSource NarrationSource;
    public UnityEngine.Video.VideoPlayer VideoPlayer;

    private void Update() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3) {
            NarrationSource = FindObjectOfType<Narrate.NarrationManager>().gameObject.GetComponent<AudioSource>();
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
    public void MakeNullAfterPlay() {
        StartCoroutine(IsPlaying());
    }
    private IEnumerator IsPlaying() {
        yield return new WaitUntil(() => VideoSource.isPlaying == false);
        PlayVideoSound(null);
        StopCoroutine(IsPlaying());
    }
}