using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    [SerializeField] private Slider loadProgressSlider;
    [SerializeField] private GameObject loadProgressUIPanel;

    public void ChangeScene(string sceneName) {
        var opr = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(LoadSceneOpr(opr));
    }

    private IEnumerator LoadSceneOpr(AsyncOperation operation) {
        if (loadProgressUIPanel == null || loadProgressSlider == null) {
            yield return null;
        }

        loadProgressUIPanel.SetActive(true);

        while (!operation.isDone) {
            var progress = Mathf.Clamp01(operation.progress / .9f);

            loadProgressSlider.value = progress;
            yield return null;
        }

        loadProgressUIPanel.SetActive(false);
    }
}