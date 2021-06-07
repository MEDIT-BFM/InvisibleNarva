using System;

public class QuestManager : Singleton<QuestManager> {
    public event Action<string> OnMessageChanged = delegate { };
    public event Action<Quest> OnQuestUpdated = delegate { };
    public event Action<Quest> OnQuestCompleted = delegate { };

    public Quest SelectedQuest { get; private set; }

    public void HandleUpdatedQuest(Quest quest) {
        SelectedQuest = quest;
        OnMessageChanged?.Invoke(quest.QuestInfoText);
        OnQuestUpdated?.Invoke(quest);
    }

    public void EnterWorld() {
        SceneController.Instance.ChangeScene("FPS_TouchScene", 1);
    }

    private void OnEnable() {
        DontDestroyOnLoad(this);
    }
}