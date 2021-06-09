using System;
using UnityEngine;

public class MinimapQuestUI : MonoBehaviour {
    public static event Action<Vector2> OnQuestInitiated = delegate { };

    private bool _isCompassOn;
    private Transform _transform;
    private PlayerController _player;

    [SerializeField] private string questID;

    private void Awake() {
        _transform = transform;
    }

    private void OnEnable() {
        Task.OnInitiated += TaskInitiatedHandler;
        MinimapUI.OnCompassChanged += CompassChangedHander;
    }

    private void Start() {
        _player = TaskManager.Instance.Player;
    }

    private void TaskInitiatedHandler(Task task) {
        if (task.ID == questID) {
            OnQuestInitiated?.Invoke(_transform.position);
        }
    }

    private void CompassChangedHander(bool isOn) {
        _isCompassOn = isOn;

        if (!isOn) {
            _transform.rotation = Quaternion.Euler(_transform.eulerAngles.x, 0, _transform.eulerAngles.z);
        }
    }

    private void LateUpdate() {
        if (_isCompassOn) {
            var compassRotation = Quaternion.Euler(_transform.eulerAngles.x, _player.Transform.eulerAngles.y, _transform.eulerAngles.z);
            _transform.rotation = compassRotation;
        }
    }

    private void OnDisable() {
        Task.OnInitiated -= TaskInitiatedHandler;
        MinimapUI.OnCompassChanged -= CompassChangedHander;
    }
}