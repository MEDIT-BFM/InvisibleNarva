using UnityEngine;
using DG.Tweening;

namespace InvisibleNarva {
    public class CreditsUI : MonoBehaviour {
        [SerializeField] private RectTransform finishButton;
        [SerializeField] private CanvasGroup _creditsCanvasGroup;
        [SerializeField] private RectTransform _creditsRect;
        [SerializeField] private Speech finalSpeech;

        private float _creditsHeight;
        private const float _speedMultiplier = 0.01f;

        public void Initialize() {
            finishButton.gameObject.SetActive(false);
            _creditsCanvasGroup.gameObject.SetActive(true);
            finalSpeech.Begin();

            _creditsCanvasGroup.DOFade(1, 0.75f).OnComplete(() => {
                DOTween.Sequence()
                .Append(_creditsRect.DOAnchorPosY(0, _creditsHeight * _speedMultiplier))
                .OnComplete(() => Skip());
            });
        }

        public void Skip() {
            SceneController.Instance.ChangeScene("Entrance");
            SceneController.Instance.Unload("JaaniChurchInterior");
        }

        private void Awake() {
            _creditsHeight = _creditsRect.sizeDelta.y;
        }

        private void OnEnable() {
            TaskManager.OnGameOver += GameOverHandler;
        }

        private void GameOverHandler() {
            finishButton.gameObject.SetActive(true);
            finishButton.DOShakePosition(1, Vector2.one * 0.5f);
        }

        private void OnDisable() {
            TaskManager.OnGameOver -= GameOverHandler;
        }
    }
}