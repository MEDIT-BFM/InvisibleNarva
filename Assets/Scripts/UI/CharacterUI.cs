using UnityEngine;
using UnityEngine.UI;

namespace InvisibleNarva {
    public class CharacterUI : MonoBehaviour {
        private RectTransform _rectTransform;
        private RawImage _rawImage;
        private AspectRatioFitter _fitter;

        private void Awake() {
            _fitter = GetComponent<AspectRatioFitter>();
            _rectTransform = GetComponent<RectTransform>();
            _rawImage = GetComponent<RawImage>();
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
            var sizeDelta = character.RenderTransform.sizeDelta;
            _rawImage.raycastTarget = character.IsSkippable;
            _rectTransform.anchoredPosition = character.RenderTransform.anchoredPosition;
            _rectTransform.sizeDelta = sizeDelta;
            _fitter.aspectRatio = sizeDelta.x / sizeDelta.y;
            _rectTransform.ForceUpdateRectTransforms();
        }

        private void OnDestroy() {
            CharacterManager.OnPlay -= CharacterPlayHandler;
            CharacterManager.OnStop -= CharacterStopHandler;
        }
    }
}