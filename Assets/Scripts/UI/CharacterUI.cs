using UnityEngine;

public class CharacterUI : MonoBehaviour {
    private RectTransform _rectTransform;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        CharacterManager.OnPlay += CharacterPlayHandler;
        CharacterManager.OnStop += CharacterStopHandler;
        CharacterStopHandler();
    }

    private void CharacterStopHandler() {
        gameObject.SetActive(false);
    }

    private void CharacterPlayHandler(Character character) {
        gameObject.SetActive(true);
        _rectTransform.anchoredPosition = character.RenderTransform.anchoredPosition;
        _rectTransform.sizeDelta = character.RenderTransform.sizeDelta;
        _rectTransform.ForceUpdateRectTransforms();
    }

    private void OnDestroy() {
        CharacterManager.OnPlay -= CharacterPlayHandler;
        CharacterManager.OnStop -= CharacterStopHandler;
    }
}
