using UnityEngine;
using TMPro;

namespace InvisibleNarva {
    public class NarrationUI : MonoBehaviour {
        [SerializeField] private RectTransform content;
        [SerializeField] private TextMeshProUGUI subtitle;
        [SerializeField] private CanvasGroup canvasGroup;

        private readonly Vector2 _textPaddingSize = new Vector2(60, 30);

        private void Start() {
            NarrationManager.OnPlay += NarrationPlayHandler;
            NarrationManager.OnStop += NarrationStopHander;
            gameObject.SetActive(false);
        }

        private void NarrationPlayHandler(Speech narration, bool showSubtitle) {
            gameObject.SetActive(showSubtitle);

            if (showSubtitle) {
                subtitle.SetDynamicText(content, narration.Subtitle, _textPaddingSize);
            }
        }

        private void NarrationStopHander() {
            gameObject.SetActive(false);
        }

        private void OnDestroy() {
            NarrationManager.OnPlay -= NarrationPlayHandler;
            NarrationManager.OnStop -= NarrationStopHander;
        }
    }
}