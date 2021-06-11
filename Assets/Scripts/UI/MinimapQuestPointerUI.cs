using UnityEngine;

public class MinimapQuestPointerUI : MonoBehaviour {
    [SerializeField] private Camera minimapCamera;

    private Vector3 _targetPosition;
    private RectTransform _rectTransform;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void OnEnable() {
        TaskManager.OnNextPointed += NextPointedHandler;
    }

    private void NextPointedHandler(Task task) {
        if (task != null) {
            Show(task.GetMinimapLocation);
        }
        else {
            Hide();
        }
    }

    private void Update() {
        if (_rectTransform.IsVisibleFrom(minimapCamera)) {
            var dir = _targetPosition - _rectTransform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            _rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else {
            Hide();
        }
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show(Vector2 target) {
        gameObject.SetActive(true);
        _targetPosition = target;
    }

    private void OnDisable() {
        TaskManager.OnNextPointed -= NextPointedHandler;
    }
}