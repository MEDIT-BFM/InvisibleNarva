using UnityEngine;
using TMPro;
using DG.Tweening;

namespace InvisibleNarva {
    public class MessageUI : MonoBehaviour {
        [SerializeField] private float fadeDuration = 1.5f;
        [SerializeField] private float displayDuration = 3;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI message;

        private Sequence _sequence;

        private void OnEnable() {

            _sequence = DOTween.Sequence()
                            .AppendCallback(() => canvasGroup.alpha = 0)
                            .Append(canvasGroup.DOFade(1, fadeDuration))
                            .Append(canvasGroup.DOFade(0, fadeDuration).SetDelay(displayDuration)).SetAutoKill(false);

            _sequence.Pause();

            BuildingArea.OnEnter += BuildingEnterHandler;
        }

        private void BuildingEnterHandler(string buildingName) {
            message.text = buildingName;

            if (_sequence.IsPlaying()) {
                _sequence.Restart();
                return;
            }

            _sequence.Restart();
        }

        private void OnDisable() {
            BuildingArea.OnEnter -= BuildingEnterHandler;
        }
    }
}