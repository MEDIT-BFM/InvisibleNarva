using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour {
    public Quest quest;
    public Image questImage;
    public TMPro.TMP_Text UIquestInfoText;

    private void Start()
    {
        if (quest)
            Display(quest);
    }

    public virtual void Display(Quest quest)
    {
        this.quest = quest;
        questImage.sprite = quest.image;
        UIquestInfoText = quest.questInfoText;
    }

    public virtual void Completed(Sprite completedImage)
    {
        quest.image = completedImage;
    }

    public virtual void AssignInfoText()
    {
        if (GetComponent<Toggle>().isOn)
        {
            GetComponentInParent<MapUI>().UIinfoText.GetComponent<TMPro.TMP_Text>().text = UIquestInfoText.text;
        }        
    }
}
