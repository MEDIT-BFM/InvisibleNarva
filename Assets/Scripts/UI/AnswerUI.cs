using UnityEngine;
using TMPro;

public class AnswerUI : MonoBehaviour {
    [SerializeField] private int id;
    [SerializeField] private TextMeshProUGUI text;

    private float _defaultFontSize;
    private RectTransform _rectTransform;
    private const float _fontSizeMultiplier = 1.1f;
    private readonly Vector2 _textPaddingSize = new Vector2(25,25);

    public void SetText(string answer) {
        text.SetDynamicText(_rectTransform, answer, _textPaddingSize);
    }

    public void Select() {
        text.color = Color.blue;
        text.fontSize *= _fontSizeMultiplier;
        text.ForceMeshUpdate();

    }

    public void Deselect() {
        text.color = Color.black;
        text.fontSize = _defaultFontSize;
        text.ForceMeshUpdate();
    }

    private void Awake() {
        _defaultFontSize = text.fontSize;
        _rectTransform = GetComponent<RectTransform>();
    }
}