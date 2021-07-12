using UnityEngine;
using UnityEngine.Events;

namespace InvisibleNarva {
    [RequireComponent(typeof(BoxCollider))]
    public class AdditiveSceneLoader : MonoBehaviour {
        [SerializeField] private bool isLoader;
        [SerializeField] private string sceneName;
        [SerializeField] private UnityEvent OnBegin;
        [SerializeField] private UnityEvent OnComplete;

        private void OnTriggerEnter(Collider other) {
            if (other.tag != "Player") {
                return;
            }

            if (isLoader) {
                SceneController.Instance.LoadAdditiveScene(sceneName, OnLoadBegin, OnLoadComplete);
            }
            else {
                SceneController.Instance.Unload(sceneName, OnLoadBegin, OnLoadComplete);
            }
        }

        private void OnLoadBegin() {
            OnBegin?.Invoke();
        }

        private void OnLoadComplete() {
            OnComplete?.Invoke();
        }
    }
}