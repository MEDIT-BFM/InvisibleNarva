using UnityEngine;
using DG.Tweening;

public class IntroductionUI : MonoBehaviour {
    [SerializeField] private GameObject skipButton;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private IntroductionController introductionController;

    public void Initialize() {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => {
            canvasGroup.gameObject.SetActive(false);
            skipButton.SetActive(true);
            introductionController.PlayOrSkip();
        });
    }
}