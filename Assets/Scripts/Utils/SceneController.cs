using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneController : Singleton<SceneController> {
    public bool DisplayLoading = true;

    [SerializeField] private float transitionDuration = 1;
    [SerializeField] private GameObject loadProgressUIPanel;
    [SerializeField] private Slider loadProgressSlider;
    [SerializeField] private Image transitionImage;

    private void Start() {
        transitionImage.color = Color.black;
        DOTween.Sequence().Append(transitionImage.DOFade(0, transitionDuration)).OnComplete(() => transitionImage.gameObject.SetActive(false));
    }

    public void ChangeScene(string sceneName) {
        var opr = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(LoadSceneOpr(opr));
    }

    public void ChangeScene(string sceneName, float duration = 0) {
        if (duration == 0) {
            ChangeScene(sceneName);
            return;
        }

        transitionImage.gameObject.SetActive(true);
        float alpha = transitionImage.color.a == 0 ? 1 : 0;
        DOTween.Sequence().Append(transitionImage.DOFade(alpha, duration)).OnComplete(() => {
            transitionImage.gameObject.SetActive(false);
            ChangeScene(sceneName);
        });
    }

    private IEnumerator LoadSceneOpr(AsyncOperation operation) {
        if (loadProgressUIPanel == null || loadProgressSlider == null) {
            yield return null;
        }
        loadProgressUIPanel.SetActive(true);
        loadProgressSlider.gameObject.SetActive(DisplayLoading);

        while (!operation.isDone) {
            var progress = Mathf.Clamp01(operation.progress / .9f);

            loadProgressSlider.value = progress;
            yield return null;
        }

        loadProgressUIPanel.SetActive(false);
    }
}