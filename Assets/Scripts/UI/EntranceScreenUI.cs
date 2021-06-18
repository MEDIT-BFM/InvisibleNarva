using UnityEngine;
using DG.Tweening;

public class EntranceScreenUI : MonoBehaviour {
    [SerializeField] private GameObject skipButton;
    [SerializeField] private Transform tapButtonLabel;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private EntranceScreen entranceScreen;

    private Sequence _tapSequence;

    private void Start() {
        _tapSequence = DOTween.Sequence()
            .Append(tapButtonLabel.DOScale(1.1f, 0.75f))
            .Append(tapButtonLabel.DOScale(1f, 0.75f))
            .SetLoops(-1).SetAutoKill(false);
    }

    public void Initialize() {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => {
            canvasGroup.gameObject.SetActive(false);
            skipButton.SetActive(true);
            entranceScreen.PlayOrSkip();
            _tapSequence.Kill();
        });
    }
}