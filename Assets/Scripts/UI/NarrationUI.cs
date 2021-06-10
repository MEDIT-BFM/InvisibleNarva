using UnityEngine;
using TMPro;

public class NarrationUI : MonoBehaviour {
    [SerializeField] private RectTransform content;
    [SerializeField] private TextMeshProUGUI subtitle;

    private readonly Vector2 _textPaddingSize = new Vector2(60, 30);

    private void Start() {
        NarrationManager.OnPlay += NarrationPlayHandler;
        NarrationManager.OnStop += NarrationStopHander;
        gameObject.SetActive(false);
    }

    private void NarrationPlayHandler(Speech narration) {
        gameObject.SetActive(true);
        subtitle.SetDynamicText(content, narration.Subtitle, _textPaddingSize);
    }

    private void NarrationStopHander() {
        subtitle.text = "";
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        NarrationManager.OnPlay -= NarrationPlayHandler;
        NarrationManager.OnStop -= NarrationStopHander;
    }
}
