using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace InvisibleNarva {
    public class NarrationUI : MonoBehaviour {
        [SerializeField] private RectTransform content;
        [SerializeField] private TextMeshProUGUI subtitle;

        private Image _contentImage;
        private Vector2 _subtitleSize;
        private readonly Vector2 _textPaddingSize = new Vector2(60, 30);

        private void Start() {
            NarrationManager.OnPlay += NarrationPlayHandler;
            NarrationManager.OnStop += NarrationStopHander;
            gameObject.SetActive(false);
            _subtitleSize = subtitle.rectTransform.sizeDelta;
            _contentImage = content.GetComponent<Image>();
        }

        private void NarrationPlayHandler(Speech narration, bool showSubtitle) {
            gameObject.SetActive(showSubtitle);

            if (!showSubtitle) {
                return;
            }

            _contentImage.raycastTarget = narration.IsSkippable;

            var width = subtitle.GetPreferredValues(narration.Subtitle).x;

            if (width < _subtitleSize.x) {
                subtitle.rectTransform.sizeDelta = new Vector2(width, _subtitleSize.y);
            }
            else {
                subtitle.rectTransform.sizeDelta = _subtitleSize;
            }

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