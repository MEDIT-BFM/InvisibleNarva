using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IntroductionUI : MonoBehaviour {
    [SerializeField] private bool isLoop;
    [SerializeField] private float delayTime;
    [SerializeField] private Button skipButton;
    [SerializeField] private AudioClip currentTrack;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private IntroductionController introductionController;

    private void OnEnable() {
        skipButton.onClick.AddListener(ClickSkipHandler);
    }

    private void ClickSkipHandler() {
        DOTween.Sequence().Append(canvasGroup.DOFade(0, 0.5f)).OnComplete(() => {
            gameObject.SetActive(false);
            introductionController.PlayVideo();
        });
    }

    private void Start() {
        SoundManager.Instance.BackgroundSource.loop = isLoop;
        DOTween.Sequence().SetDelay(delayTime).AppendCallback(() => {
            SoundManager.Instance.PlayBackground(currentTrack);
        });
    }

    private void OnDisable() {
        skipButton.onClick.RemoveAllListeners();
        //SoundManager.Instance.StopAudioSources();
    }

    //public void PlayAudio(AudioClip audioClip) {
    //    if (GetComponent<UnityEngine.UI.Toggle>().isOn) {
    //        SoundManager.Instance.PlayVideoSound(audioClip);
    //        SoundManager.Instance.MakeNullAfterPlay();
    //    }
    //}
}