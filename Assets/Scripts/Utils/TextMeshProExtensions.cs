using UnityEngine;
using TMPro;

public static class TextMeshProExtensions {
    public static TextMeshProUGUI SetDynamicText(this TextMeshProUGUI textMeshProUGUI, RectTransform holder, string content, Vector2 holderPaddingSize) {
        textMeshProUGUI.text = content;
        textMeshProUGUI.ForceMeshUpdate();

        var textSize = textMeshProUGUI.GetRenderedValues(false);
        holder.sizeDelta = textSize + holderPaddingSize;

        return textMeshProUGUI;
    }
}
