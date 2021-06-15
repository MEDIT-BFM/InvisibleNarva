using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NarrationUI : MonoBehaviour {
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private RectTransform content;
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private CanvasGroup canvasGroup;

    // private Tween _hideTween;
    private readonly Vector2 _textPaddingSize = new Vector2(60, 30);

    private void Start() {
        NarrationManager.OnPlay += NarrationPlayHandler;
        NarrationManager.OnStop += NarrationStopHander;

        //canvasGroup.alpha = 0;
        gameObject.SetActive(false);
        //_hideTween = canvasGroup.DOFade(0, fadeDuration).OnComplete(() => gameObject.SetActive(false)).SetAutoKill(false);
    }

    private void NarrationPlayHandler(Speech narration) {
        gameObject.SetActive(true);
        subtitle.SetDynamicText(content, narration.Subtitle, _textPaddingSize);
    }

    //private IEnumerator WaitUntilHide(Speech narration) {
    //    yield return new WaitUntil(() => !_hideTween.IsPlaying());

    //    gameObject.SetActive(true);
    //    subtitle.SetDynamicText(content, narration.Subtitle, _textPaddingSize);
    //    canvasGroup.DOFade(1, fadeDuration);
    //}

    private void NarrationStopHander() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        NarrationManager.OnPlay -= NarrationPlayHandler;
        NarrationManager.OnStop -= NarrationStopHander;
    }
}
