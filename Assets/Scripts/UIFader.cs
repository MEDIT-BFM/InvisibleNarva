using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFader : MonoBehaviour {

    [Range(0.001f, 0.999f)]
    public float fadeSpeed;

    private CanvasGroup uiElement;

    private void Awake() {
        uiElement = GetComponent<CanvasGroup>();
        uiElement.alpha = 0f;
        uiElement.interactable = false;
        uiElement.blocksRaycasts = false;
        uiElement.ignoreParentGroups = false;
    }

    private void Start() {
        FadeIn();
    }

    public void FadeIn() {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, (1f - fadeSpeed)));
    }

    public void FadeOut() {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, (1f - fadeSpeed)));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1) {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true) {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }
        //Animation completed here
    }
}
