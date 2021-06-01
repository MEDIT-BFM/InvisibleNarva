using UnityEngine;

public class QuestManager : MonoBehaviour {

    public Transform mapUI_Image;
    public Sprite completedQuestUI;
    public Transform entity;
    public Transform entityNarrationArea;

    private Transform parentOfEntities;

    private void Awake() {
        parentOfEntities = transform.parent.parent;
    }
    private void OnEnable() {
        if (mapUI_Image) {
            mapUI_Image.GetComponent<QuestUI>().quest.image = completedQuestUI;
            mapUI_Image.GetComponent<QuestUI>().Display(mapUI_Image.GetComponent<QuestUI>().quest);
        }
        Destroy(transform.parent.gameObject, 2);
    }
    private void OnDestroy() {
        Transform newEntity = Instantiate(entity, parentOfEntities);
        newEntity.Find("Completed").GetComponent<QuestManager>().entityNarrationArea = entityNarrationArea;
        entityNarrationArea.GetComponent<EntityTrigger>().entities = newEntity;
        newEntity.Find("Completed").GetComponent<QuestManager>().parentOfEntities = parentOfEntities;
        newEntity.Find("Completed").GetComponent<QuestManager>().entity = entity;
        EntityTrigger.isInteracted = false;
    }
}
