using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
    private RectTransform _rectTransform;
    private RawImage _rawImage;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _rawImage = GetComponent<RawImage>();
    }

    private void Start() {
        CharacterManager.OnPlay += CharacterPlayHandler;
        CharacterManager.OnStop += CharacterStopHandler;
        CharacterStopHandler();
    }

    private void CharacterStopHandler() {
        _rawImage.texture = null;
        gameObject.SetActive(false);
        //_rawImage.color = Color.clear;
    }

    private void CharacterPlayHandler(Character character) {
        //_rawImage.color = Color.white;
        gameObject.SetActive(true);
        _rawImage.texture = character.RenderTexture;
        _rectTransform.anchoredPosition = character.RenderTransform.anchoredPosition;
        _rectTransform.sizeDelta = character.RenderTransform.sizeDelta;
        _rectTransform.ForceUpdateRectTransforms();
    }

    private void OnDestroy() {
        CharacterManager.OnPlay -= CharacterPlayHandler;
        CharacterManager.OnStop -= CharacterStopHandler;
    }
}
