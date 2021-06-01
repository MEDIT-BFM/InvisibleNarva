using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public Map map;
    public Transform content;
    public QuestUI questUIprefab;
    public TMPro.TMP_Text UIinfoText;

    void Start () {
        if (map)
            Display(map);
    }
    
    public virtual void Display(Map map)
    {
        this.map = map;
        Refresh();
    }

    public virtual void Refresh()
    {
        foreach (Quest q in map.quests)
        {
            QuestUI ui = Instantiate(questUIprefab, content);
            ui.GetComponent<Toggle>().isOn = false;
            ui.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();

            RectTransform trans = q.location;
            ui.gameObject.GetComponent<RectTransform>().anchorMax = trans.anchorMax;
            ui.gameObject.GetComponent<RectTransform>().anchorMin = trans.anchorMin;
            ui.Display(q);
        }
    }
}
