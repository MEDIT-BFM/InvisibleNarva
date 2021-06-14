using UnityEngine;
using DG.Tweening;

public class EntranceScreenUI : MonoBehaviour {
    [SerializeField] private GameObject skipButton;
    [SerializeField] private Transform tapButtonLabel;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private EntranceScreen introductionController;

    private Sequence _tapSequence;

    private void Start() {
        _tapSequence = DOTween.Sequence()
            .Append(tapButtonLabel.DOScale(1.1f, 0.5f))
            .Append(tapButtonLabel.DOScale(1f, 0.5f))
            .SetLoops(-1).SetAutoKill(false);
    }

    public void Initialize() {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => {
            canvasGroup.gameObject.SetActive(false);
            skipButton.SetActive(true);
            introductionController.PlayOrSkip();
            _tapSequence.Kill();
        });
    }
}