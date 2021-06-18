using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MinimapUI : MonoBehaviour {
    public static event Action<bool> OnCompassChanged = delegate { };

    [SerializeField] private PlayerController player;
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private RectTransform minimap;
    [SerializeField] private RectTransform minimapShowButton;
    [SerializeField] private Toggle compass;
    [SerializeField] private Slider zoomSlider;

    private bool _isCompassOn;
    private Image _compassImage;
    private Transform _dynamicCompass;
    private Transform _mapCamTransform;
    private Sequence _minimapHideTween;
    private bool _minimapDisplayToggle = false;

    private const float _minimapHideTweenDuration = 0.3f;
    private readonly Vector3 _axisZ_90 = new Vector3(0, 0, 90);

    public void Display() {
        _minimapHideTween.SmoothRewind();
    }

    public void Hide() {
        _minimapHideTween.PlayForward();
    }

    private void Awake() {
        _compassImage = compass.GetComponent<Image>();
        _dynamicCompass = compass.graphic.transform;
        _mapCamTransform = minimapCamera.transform;
    }

    private void OnEnable() {
        compass.onValueChanged.AddListener(CompassChangedHandler);
        zoomSlider.onValueChanged.AddListener(ZoomValueChangedHandler);
        _mapCamTransform.position = new Vector3(player.transform.position.x, _mapCamTransform.position.y, player.transform.position.z);
    }

    private void Start() {
        Task.OnInitiated += (task) => gameObject.SetActive(false);
        Task.OnCompleted += (task) => gameObject.SetActive(true);

        _minimapHideTween = DOTween.Sequence()
                .Append(minimap.DOScale(0, _minimapHideTweenDuration * 0.75f))
                .AppendCallback(() => minimapShowButton.gameObject.SetActive(_minimapDisplayToggle = !_minimapDisplayToggle))
                .Append(minimapShowButton.DOScale(1, _minimapHideTweenDuration))
                .Join(minimapShowButton.DOPunchRotation(_axisZ_90, _minimapHideTweenDuration))
                .AppendCallback(() => minimap.gameObject.SetActive(_minimapDisplayToggle = !_minimapDisplayToggle))
                .SetAutoKill(false);

        _minimapHideTween.Pause();
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