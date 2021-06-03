using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour {
    [SerializeField] private Quest quest;
    [SerializeField] private Image questImage;
    [SerializeField] private Sprite completedQuestSprite;

    public Quest Quest { get => quest; }

    private Toggle _toggle;

    public void Completed(Sprite completedImage) {
        Quest.Image = completedImage;
    }

    private void Awake() {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable() {
        QuestManager.Instance.OnQuestCompleted += QuestCompletedHandler;
        _toggle.onValueChanged.AddListener(UpdateInfoText);
    }

    private void QuestCompletedHandler(Quest quest) {
        questImage.sprite = completedQuestSprite;
    }

    private void UpdateInfoText(bool isOn) {
        if (isOn) {
            QuestManager.Instance.HandleChancedMessage(Quest.QuestInfoText);
            QuestManager.Instance.HandleUpdatedQuest(Quest);
        }
    }

    private void OnDisable() {
        QuestManager.Instance.OnQuestCompleted -= QuestCompletedHandler;
        _toggle.onValueChanged.RemoveAllListeners();
    }
}