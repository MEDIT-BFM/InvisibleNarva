using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMapUI : MonoBehaviour {
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button enterButton;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField, TextArea(1, 2)] string selectionWarningMessage;

    private QuestManager _questManager;

    private void OnEnable() {
        if (QuestManager.TryGetInstance(out QuestManager instance)) {
            _questManager = instance;
        }

        _questManager.OnMessageChanged += (message) => infoText.text = message;
        enterButton.onClick.AddListener(ClickEnterHandler);
    }

    private void ClickEnterHandler() {
        if (!toggleGroup.AnyTogglesOn()) {
            infoText.text = selectionWarningMessage;
        }
        else {
            _questManager.EnterWorld();
        }
    }

    private void OnDisable() {
        enterButton.onClick.RemoveAllListeners();
    }
}