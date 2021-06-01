using System.Collections;
using UnityEngine;

public class CharacterEntityController : MonoBehaviour {
    public Transform thisHasImage;
    public Transform nextEntity;
    public UnityEngine.Video.VideoClip videoClip;
    public AudioClip videoSound;
    public float StopDisplayAfterSeconds;

    private UnityEngine.Video.VideoPlayer videoPlayer;
    private double displayTime;

    private void Awake() {
        videoPlayer = SoundManager.Instance.VideoPlayer;
        if (videoSound != null) {
            displayTime = videoSound.length;
        }
        if (videoClip) {
            videoPlayer.clip = videoClip;
            videoPlayer.targetTexture = GetComponent<UnityEngine.UI.RawImage>().texture as RenderTexture;
            videoPlayer.Prepare();
        }
        if (nextEntity != null) {
            nextEntity.gameObject.SetActive(false);
        }
    }

    private void Start() {
        if (thisHasImage)
            thisHasImage.gameObject.SetActive(true);
    }

    private void OnEnable() {
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo() {
        videoPlayer.Play();
        if (videoSound != null) {
            SoundManager.Instance.PlayVideoSound(videoSound);
        }
        if (GetComponent<FeedbackEntityController>() == null) {
            Destroy(gameObject, (float)displayTime);
        }
        yield return new WaitForSeconds(StopDisplayAfterSeconds);
        VideoStoppedToPlay();
    }

    private void OnDestroy() {
        if (nextEntity != null) {
            videoPlayer.Stop();
            SoundManager.Instance.PlayVideoSound(null);
            videoPlayer.clip = null;
            videoPlayer.targetTexture = null;
            nextEntity.gameObject.SetActive(true);
        }
    }

    private void VideoStoppedToPlay() {
        Color color = new Color(1, 1, 1, 0);
        GetComponent<UnityEngine.UI.RawImage>().color = color;
        GetComponent<UnityEngine.UI.RawImage>().texture = null;
        videoPlayer.clip = null;
        videoPlayer.targetTexture = null;
    }

    public void Skip() {
        Destroy(gameObject);
        if (GetComponent<FeedbackEntityController>()) {
            Destroy(gameObject.GetComponentInParent<QuizManager>().gameObject);
        }
    }
}
