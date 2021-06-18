﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneController : Singleton<SceneController> {
    public bool DisplayLoading = true;

    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private GameObject loadProgressUIPanel;
    [SerializeField] private Slider loadProgressSlider;
    [SerializeField] private Image transitionImage;

    public void ChangeScene(string sceneName) {
        if (transitionDuration == 0) {
            var opr = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadSceneOpr(opr));
            return;
        }

        transitionImage.gameObject.SetActive(true);
        float alpha = transitionImage.color.a == 0 ? 1 : 0;
        DOTween.Sequence().Append(transitionImage.DOFade(alpha, transitionDuration)).OnComplete(() => {
            transitionImage.gameObject.SetActive(false);
            var opr = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadSceneOpr(opr));
        });
    }

    private void Start() {
        transitionImage.color = Color.black;
        DOTween.Sequence().Append(transitionImage.DOFade(0, transitionDuration)).OnComplete(() => transitionImage.gameObject.SetActive(false));
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