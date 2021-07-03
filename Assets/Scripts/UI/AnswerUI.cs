using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace InvisibleNarva {
    public class AnswerUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI text;

        private string _content;
        private RectTransform _rectTransform;
        private Image _image;
        private readonly Vector2 _textPaddingSize = new Vector2(25, 25);

        public void Display(string content) {
            gameObject.SetActive(true);
            _content = content;
            text.SetDynamicText(_rectTransform, content, _textPaddingSize);
            _rectTransform.DOShakePosition(0.2f);
        }

        public void Hide() {
            text.text = "";
            _image.raycastTarget = false;
            gameObject.SetActive(false);
        }

        public void Select() {
            text.text = $"<u><b>{_content}</b></u>";
            text.ForceMeshUpdate();
        }

        public void Deselect() {
            text.text = _content;
            text.ForceMeshUpdate();
        }

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        private void Start() {
            QuestionManager.OnPlay += QuestionPlayHandler;
            QuestionManager.OnQuestionReady += QuestionReadyHandler;
        }

        private void QuestionReadyHandler() {
            _image.raycastTarget = true;
        }

        private void QuestionPlayHandler(Question q) {
            Hide();
        }

        private void OnDestroy() {
            QuestionManager.OnPlay -= QuestionPlayHandler;
            QuestionManager.OnQuestionReady -= QuestionReadyHandler;
        }
    }
}