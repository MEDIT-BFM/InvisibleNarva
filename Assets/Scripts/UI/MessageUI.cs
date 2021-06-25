using UnityEngine;
using TMPro;
using DG.Tweening;

namespace InvisibleNarva {
    public class MessageUI : MonoBehaviour {
        [SerializeField] private float fadeDuration = 1.5f;
        [SerializeField] private float displayDuration = 3;
        [SerializeField] private RectTransform holder;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI message;

        private void OnEnable() {
            BuildingArea.OnEnter += BuildingEnterHandler;
            canvasGroup.alpha = 0;
        }

        private void BuildingEnterHandler(string buildingName) {
            message.text = buildingName;
          //message.SetDynamicText(holder, buildingName, Vector2.zero);
            canvasGroup.DOFade(1, fadeDuration).OnComplete(() => canvasGroup.DOFade(0, fadeDuration).SetDelay(displayDuration));
        }

        private void OnDisable() {
            BuildingArea.OnEnter -= BuildingEnterHandler;
        }
    }
}