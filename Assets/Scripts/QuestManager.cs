using System;
using UnityEngine;

public class QuestManager : Singleton<QuestManager> {
    public event Action<string> OnMessageChanged = delegate { };
    public event Action<Quest> OnQuestUpdated = delegate { };
    public event Action<Quest> OnQuestCompleted = delegate { };

    public Quest SelectedQuest { get; private set; }

    public void HandleChancedMessage(string message) {
        OnMessageChanged?.Invoke(message);
    }

    public void HandleUpdatedQuest(Quest quest) {
        SelectedQuest = quest;
        OnQuestUpdated?.Invoke(quest);
    }

    public void EnterWorld() {
        SceneController.Instance.ChangeScene("FPS_TouchScene", 1);
    }

    //protected override void OnDestroy() {
    //    Transform newEntity = Instantiate(entity, parentOfEntities);
    //    newEntity.Find("Completed").GetComponent<QuestManager>().entityNarrationArea = entityNarrationArea;
    //    entityNarrationArea.GetComponent<EntityTrigger>().entities = newEntity;
    //    newEntity.Find("Completed").GetComponent<QuestManager>().parentOfEntities = parentOfEntities;
    //    newEntity.Find("Completed").GetComponent<QuestManager>().entity = entity;
    //    EntityTrigger.isInteracted = false;
    //}
}