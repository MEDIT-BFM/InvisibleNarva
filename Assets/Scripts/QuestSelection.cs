using UnityEngine;

public class QuestSelection : MonoBehaviour
{
    public Transform QuestContent;
    public SceneShifter sceneShifter;
    public Transform WarningText;
    public Transform InitialText;

    private bool isSelected = false;
    private void Start()
    {
        Invoke("PlayerInitialPosition", 1);
    }
    private void PlayerInitialPosition()
    {
        foreach (QuestUI q in QuestContent.GetComponentsInChildren<QuestUI>())
        {
            q.quest.initialPlayerPosition.GetComponent<PlayerInitialPosition>().startLocation = false;
        }
    }
    public void QuestSelected()
    {
        foreach (QuestUI q in QuestContent.GetComponentsInChildren<QuestUI>())
        {
            if (q.GetComponent<UnityEngine.UI.Toggle>().isOn)
            {
                q.quest.initialPlayerPosition.GetComponent<PlayerInitialPosition>().startLocation = true;
                sceneShifter.FadeTransition();
                InitialText.GetComponent<TMPro.TMP_Text>().text = q.quest.initialText;
                isSelected = true;
            }
        }
    }
    public void WarningMessage(string text)
    {
        if (!isSelected)
            WarningText.GetComponent<TMPro.TMP_Text>().text = text;
    }
    public void InitialTextAssigned() {

    }
}
