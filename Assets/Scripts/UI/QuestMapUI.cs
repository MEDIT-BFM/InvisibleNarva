using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class QuestMapUI : MonoBehaviour {
    [SerializeField] private Transform selectionMenu;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button enterButton;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private float menuInitiationDelay = 2f;
    [SerializeField, TextArea(1, 2)] string selectionWarningMessage;

    private QuestManager _questManager;
    private GraphicRaycaster _graphicRaycaster;

    private void Awake() {
        _graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    private void Start() {
        if (QuestManager.TryGetInstance(out QuestManager instance)) {
            _questManager = instance;
        }

        _graphicRaycaster.enabled = false;
        _questManager.OnMessageChanged += (message) => infoText.text = message;
        enterButton.onClick.AddListener(ClickEnterHandler);
        selectionMenu.localScale = Vector3.zero;

        DOTween.Sequence()
            .Append(selectionMenu.DOScale(1, 0.25f))
            .Append(selectionMenu.DOShakeScale(0.4f, strength: Vector2.one * 0.2f))
            .AppendCallback(() => {
                _graphicRaycaster.enabled = true;
            }).SetDelay(menuInitiationDelay);
    }

    private void ClickEnterHandler() {
        if (!toggleGroup.AnyTogglesOn()) {
            infoText.text = selectionWarningMessage;
        }
        else {
            _questManager.EnterWorld();
        }
    }

    private void OnDestroy() {
        enterButton.onClick.RemoveAllListeners();
    }
}