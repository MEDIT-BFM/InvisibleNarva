using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour {
    [SerializeField] private Quest quest;
    [SerializeField] private Image questImage;
    [SerializeField] private Sprite completedQuestSprite;

    public Quest Quest { get => quest; }

    private Toggle _toggle;
    private QuestManager _questManager;

    private void Awake() {
        _toggle = GetComponent<Toggle>();
    }

    private void Start() {
        if (QuestManager.TryGetInstance(out QuestManager instance)) {
            _questManager = instance;
        }

        _questManager.OnQuestCompleted += QuestCompletedHandler;
        _toggle.onValueChanged.AddListener(UpdateInfoText);
    }

    private void QuestCompletedHandler(Quest quest) {
        questImage.sprite = completedQuestSprite;
    }

    private void UpdateInfoText(bool isOn) {
        if (isOn) {
            _questManager.HandleUpdatedQuest(Quest);
        }
    }

    private void OnDestroy() {
        _questManager.OnQuestCompleted -= QuestCompletedHandler;
        _toggle.onValueChanged.RemoveAllListeners();
    }
}