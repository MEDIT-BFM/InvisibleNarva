using UnityEngine;

public class MinimapQuestPointerUI : MonoBehaviour {

    [SerializeField] private Camera minimapCamera;

    private Vector3 _targetPosition;
    private RectTransform _rectTransform;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show(Vector3 target) {
        gameObject.SetActive(true);
        _targetPosition = target;
    }

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void OnEnable() {
        MinimapQuestUI.OnQuestInitiated += QuestInitiatedHandler;
    }

    private void QuestInitiatedHandler(Vector2 target) {
        Show(target);
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

    private void OnDisable() {
        MinimapQuestUI.OnQuestInitiated -= QuestInitiatedHandler;
    }
}