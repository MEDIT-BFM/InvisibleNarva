using System;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour {
    public static event Action<bool> OnCompassChanged = delegate { };

    [SerializeField] private PlayerController player;
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private Toggle compass;
    [SerializeField] private Slider zoomSlider;

    private bool _isCompassOn;
    private Image _compassImage;
    private Transform _dynamicCompass;
    private Transform _mapCamTransform;

    private void Awake() {
        _compassImage = compass.GetComponent<Image>();
        _dynamicCompass = compass.graphic.transform;
        _mapCamTransform = minimapCamera.transform;
    }

    private void OnEnable() {
        compass.onValueChanged.AddListener(CompassChangedHandler);
        zoomSlider.onValueChanged.AddListener(ZoomValueChangedHandler);
    }

    private void Start() {
        _mapCamTransform.position = new Vector3(player.Transform.position.x, _mapCamTransform.position.y, player.Transform.position.z);
    }

    private void LateUpdate() {
        if (_isCompassOn) {
            var rotate = Quaternion.Euler(_mapCamTransform.rotation.eulerAngles.x, player.Transform.rotation.eulerAngles.y, _mapCamTransform.rotation.eulerAngles.z);

            _mapCamTransform.rotation = rotate;
            _dynamicCompass.rotation = Quaternion.Euler(0, 0, player.Transform.eulerAngles.y);
        }

        if (player.IsMoving) {
            var position = new Vector3(player.Transform.position.x, _mapCamTransform.position.y, player.Transform.position.z);
            _mapCamTransform.position = position;
        }
    }

    private void ZoomValueChangedHandler(float value) {
        minimapCamera.orthographicSize = 60 + (value * 70f);
    }

    private void CompassChangedHandler(bool isOn) {
        OnCompassChanged?.Invoke(isOn);
        _isCompassOn = isOn;

        if (isOn) {
            _compassImage.color = Color.clear;
        }
        else {
            _compassImage.color = Color.white;
            _mapCamTransform.rotation = Quaternion.Euler(_mapCamTransform.eulerAngles.x, 0, _mapCamTransform.eulerAngles.z);
        }
    }

    private void OnDisable() {
        compass.onValueChanged.RemoveAllListeners();
        zoomSlider.onValueChanged.RemoveAllListeners();
    }
}
