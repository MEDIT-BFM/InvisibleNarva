using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MinimapIconUI : MonoBehaviour {
    [SerializeField] private Sprite completedSprite;

    private bool _isCompassOn;
    private Transform _transform;
    private PlayerController _player;
    private Image _image;

    public void Complete() {
        _image.sprite = completedSprite;
        _transform.DOShakeScale(1, Vector2.one * 0.1f);
        _image.GraphicUpdateComplete();
    }

    private void Awake() {
        _transform = transform;
    }

    private void OnEnable() {
        MinimapUI.OnCompassChanged += CompassChangedHander;
    }

    private void Start() {
        _player = TaskManager.Instance.Player;
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
        MinimapUI.OnCompassChanged -= CompassChangedHander;
    }
}