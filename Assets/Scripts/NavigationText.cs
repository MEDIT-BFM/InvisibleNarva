using UnityEngine;

public class NavigationText : MonoBehaviour {

    public static TMPro.TMP_Text text;

    private void Awake()
    {
        if (text)
        {
            text = GetComponent<TMPro.TMP_Text>();
        }
    }
}
